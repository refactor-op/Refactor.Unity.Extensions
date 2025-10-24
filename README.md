<div align="center">
  <h1>Refactor Unity Extensions</h1>
  <p>
    <img src="https://img.shields.io/badge/Unity-2021.3+-black?logo=unity" />
    <img src="https://img.shields.io/badge/License-MIT-blue.svg" />
  </p>
</div>

---

# Unity 扩展工具包
一套全面的Unity开发扩展工具，包含数学计算、对象操作、坐标系转换和Tilemap处理等实用功能。 参考QFramework的链式调用拓展，达到又快又好的效果。

_一时链式一时爽，一直链式一直爽（）_

# 功能特性

## 🎯 核心功能
- **数学计算**: 向量运算、线性变换、三角函数、组合数学
- **对象操作**: GameObject、Component、Transform的链式操作
- **坐标系转换**: 2D坐标系在不同原点间的灵活转换
- **Tilemap处理**: 将Tilemap渲染为Texture2D
- **实例化构建器**: 流畅的实例化参数配置

## 🛠️ 主要组件

### 1. 数学计算 (Mathematics)
```csharp
// 向量运算
var direction = pointA.DirectionTo(pointB).Normalize();
var distance = pointA.Distance(pointB);
var angle = vector.ToAngle();

// 线性变换
vector.Scale2D(new Vector2(2f, 1.5f), pivot);
vector.Rotate3D(45f, Vector3.up, centerPoint);
vector.Reflect2D(lineNormal, linePoint);

// 三角函数
var sinValue = 45f.Sin();
var radians = 90f.DegreesToRadians();

// 分量重排
var swapped = vector.Swizzle().Y().X().Z(); // 交换X和Y分量

// 排列组合计算
var combinations = Combinatorics.BinomialCoef(5, 2); // C(5,2)
var permutations = Combinatorics.Permutation(5, 2); // A(5,2)
```

### 2. 对象操作 (Unity Extensions)
```csharp
// 链式配置GameObject
gameObject
    .Name("NewObject")
    .Layer("UI")
    .Tag("Player")
    .Position(1, 2, 3)
    .Show();

// 组件管理
gameObject.GetOrAddComponent<Rigidbody>();
gameObject.RemoveComponent<Collider>();

// Transform操作
transform
    .LocalPosition(2, 3, 1)
    .LocalScale(1.5f)
    .LookAt(target)
    .AsLastSibling();
```

### 3. 实例化构建器 (Instantiate Builder)
```csharp
// 流畅的实例化配置
prefab.WithBuilder()
    .WithPosition(1, 2, 3)
    .WithRotation(0, 90, 0)
    .WithParent(parentTransform)
    .WithLayer("Default")
    .WithName("Clone")
    .WithActive(true)
    .Build();
```

### 4. 坐标系转换 (Coordinate System)
```csharp
// 在不同坐标系间转换
var screenPoint = new Vector2(100, 200);
var worldPoint = screenPoint
    .FromTopLeft(screenRect)
    .ToCenter();
```

### 5. Tilemap处理
```csharp
// 将Tilemap转换为Texture2D
var texture = tilemap.ToTexture2d();
```
# 📦 安装
### 方法一：通过 Git URL 安装 (推荐)

1. 打开 Unity Package Manager
2. 点击左上角的 **+** 按钮
3. 选择 **Add package from git URL**
4. 输入以下 URL：https://github.com/refactor-op/Refactor.Unity.Extensions.git


### 方法二：通过 manifest.json 安装

在项目的 `Packages/manifest.json` 文件中添加：

```json
{
  "dependencies": {
    "cn.refactor.unity-extensions": "https://github.com/refactor-op/Refactor.Unity.Extensions.git",
    ...
  }
}
```

# 📄贡献

欢迎 PR 和 Issues！