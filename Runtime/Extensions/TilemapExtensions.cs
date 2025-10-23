using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Refactor.Extensions
{
    public static class TilemapExtensions
    {
        /// <summary>
        ///     将 Tilemap 渲染为 Texture2D
        /// </summary>
        /// <param name="tilemap">要渲染的源 Tilemap</param>
        /// <returns>一张包含 Tilemap 内容的 Texture2D</returns>
        public static Texture2D ToTexture2d(this Tilemap tilemap)
        {
            if (!tilemap) return null;

            // 在 Unity 中，每个 Tilemap 都有一个 cellBounds 属性，它是一个 BoundsInt 结构体，定义了一个三维的矩形区域，
            // 这个区域代表了 Tilemap 中可能存在瓦片的所有单元格的范围
            // 当通过编辑器或者代码在 Tilemap 中设置或删除瓦片时，这个 cellBounds 可能会变得比实际有瓦片的区域大得多
            // 比如，在一个很大的区域内刷过瓦片，后来又擦除了一部分，特别是边缘的瓦片
            // 这时，cellBounds 仍然会记录你曾经操作过的最大范围，而不是当前真正在使用的范围
            // 调用 tilemap.CompressBounds() 方法后，Unity 会重新计算这个边界，
            // 找到所有非空单元格的最小和最大坐标，然后将 cellBounds 更新为刚好能包裹住这些单元格的最小矩形区域
            tilemap.CompressBounds();
            var cellBounds = tilemap.cellBounds;
            if (cellBounds.size.x == 0 || cellBounds.size.y == 0) return null;

            // 为后续统一设置为可读提供一个容器
            // 因为不设置为可读，操作 Tilemap 会报错（源于 Jigsaw Editor）
            // 所以这里将所有用到的 Texture2D 设置为可读
            // 至于为什么不一个一个设置为可读，而是统一设置，是因为 SetTexturesReadable 中
            // `textureImporter.SaveAndReimport()` 会触发一次重新导入，如果每次都重新导入，Unity 可能会卡死 
            var texturesToEnsureReadable = new HashSet<Texture2D>();
            // 1. 测量尺寸
            // 遍历一次所有瓦片，确定最终纹理所需要的最大单元格尺寸
            // Tilemap 中可能存在不同尺寸的 Tile，所以需要遍历所有瓦片，找到最大的尺寸
            var maxTileWidth  = 0;
            var maxTileHeight = 0;
            foreach (var position in cellBounds.allPositionsWithin)
            {
                var sprite = tilemap.GetSprite(position);
                if (!sprite) continue;

                texturesToEnsureReadable.Add(sprite.texture);

                var textureRect                                       = sprite.textureRect;
                if (textureRect.width > maxTileWidth) maxTileWidth    = (int)textureRect.width;
                if (textureRect.height > maxTileHeight) maxTileHeight = (int)textureRect.height;
            }

            // 如果遍历后发现没有带 Sprite 的瓦片，则返回 null
            if (maxTileWidth == 0 || maxTileHeight == 0) return null;

#if UNITY_EDITOR
            SetTexturesReadable(texturesToEnsureReadable, true);
#endif
            Texture2D result = null;
            try
            {
                // 纹理和像素缓冲区初始化
                var textureWidth  = cellBounds.size.x * maxTileWidth;
                var textureHeight = cellBounds.size.y * maxTileHeight;

                result = new Texture2D(textureWidth, textureHeight, TextureFormat.RGBA32, false)
                {
                    filterMode = FilterMode.Point
                };

                // 循环开始前，初始化缓冲区，避免 Gpu <-> Cpu 的数据传输开销
                var pixels = new Color32[textureWidth * textureHeight];
                for (var i = 0; i < pixels.Length; i++) // 并填充为透明色
                    pixels[i] = Color.clear;
                var spriteSheetCache = new Dictionary<Texture2D, Color32[]>();

                // 2. 渲染像素
                // 再次遍历所有瓦片，执行实际的像素复制操作
                foreach (var pos in cellBounds.allPositionsWithin)
                {
                    var sprite = tilemap.GetSprite(pos);
                    if (!sprite) continue;

                    // 从缓存或源纹理获取像素数据
                    var sourceTexture = sprite.texture;
                    if (!spriteSheetCache.TryGetValue(sourceTexture, out var allSourcePixels))
                    {
                        // sourceTexture 的 isReadable 为 false，调用本方法之前确保 texture.isReadable 为 true
                        allSourcePixels = sourceTexture.GetPixels32();
                        spriteSheetCache.Add(sourceTexture, allSourcePixels);
                    }

                    var tileColor    = (Color32)tilemap.GetColor(pos);
                    var textureRect  = sprite.textureRect;
                    var spriteWidth  = (int)textureRect.width;
                    var spriteHeight = (int)textureRect.height;

                    // 居中对齐不同尺寸的瓦片
                    var offsetX    = (maxTileWidth - spriteWidth) / 2;
                    var offsetY    = (maxTileHeight - spriteHeight) / 2;
                    var destStartX = (pos.x - cellBounds.xMin) * maxTileWidth + offsetX;
                    var destStartY = (pos.y - cellBounds.yMin) * maxTileHeight + offsetY;

                    var sourceStartX       = (int)textureRect.x;
                    var sourceStartY       = (int)textureRect.y;
                    var sourceTextureWidth = sourceTexture.width;

                    // 获取变换矩阵以处理翻转的 Tile
                    var transformMatrix       = tilemap.GetTransformMatrix(pos);
                    var isFlippedHorizontally = transformMatrix.m00 < 0;
                    var isFlippedVertically   = transformMatrix.m11 < 0;

                    for (var y = 0; y < spriteHeight; y++)
                    for (var x = 0; x < spriteWidth; x++)
                    {
                        var sourceX = sourceStartX + (isFlippedHorizontally ? spriteWidth - 1 - x : x);
                        var sourceY = sourceStartY + (isFlippedVertically ? spriteHeight - 1 - y : y);

                        var sourceIndex = sourceY * sourceTextureWidth + sourceX;
                        var destIndex   = (destStartY + y) * textureWidth + destStartX + x;

                        if (destIndex >= 0 && destIndex < pixels.Length)
                        {
                            var sourcePixel = allSourcePixels[sourceIndex];
                            var finalColor = new Color32(
                                (byte)(sourcePixel.r * tileColor.r / 255),
                                (byte)(sourcePixel.g * tileColor.g / 255),
                                (byte)(sourcePixel.b * tileColor.b / 255),
                                (byte)(sourcePixel.a * tileColor.a / 255)
                            );
                            pixels[destIndex] = finalColor;
                        }
                    }
                }

                result.SetPixels32(pixels); // 最终一次性渲染所有像素，避免 Gpu <-> Cpu 的数据传输开销
                result.Apply();             // Call Gpu
            }
            finally
            {
#if UNITY_EDITOR
                SetTexturesReadable(texturesToEnsureReadable, false); // 保证无论发生什么，都会恢复纹理设置
#endif
            }

            return result;
        }

#if UNITY_EDITOR
        private static void SetTexturesReadable(IEnumerable<Texture2D> textures, bool readable)
        {
            foreach (var texture in new HashSet<Texture2D>(textures)) // 使用 HashSet 保证每个纹理只处理一次
            {
                if (!texture) continue;
                var path = AssetDatabase.GetAssetPath(texture);
                if (string.IsNullOrEmpty(path)) continue;

                var textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;
                if (textureImporter && textureImporter.isReadable != readable)
                {
                    textureImporter.isReadable = readable;
                    textureImporter.SaveAndReimport();
                }
            }
        }
#endif
    }
}