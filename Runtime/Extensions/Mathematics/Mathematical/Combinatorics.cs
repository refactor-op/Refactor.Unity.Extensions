using UnityEngine;

namespace Refactor.Unity.Extensions.Mathematics.Mathematical
{
    public static class Combinatorics
    {
        private static readonly ulong[] Factorials =
        {
            1, 1, 2, 6, 24, 120, 720, 5040, 40320,
            362880, 3628800, 39916800, 479001600, 6227020800,
            87178291200, 1307674368000, 20922789888000, 355687428096000,
            6402373705728000, 121645100408832000, 2432902008176640000
        };

        /// <summary>
        /// 阶乘
        /// </summary>
        /// <param name="val">uint</param>
        public static ulong Factorial(uint val)
        {
            if (val >= Factorials.Length)
            {
                Debug.LogError($"阶乘只支持 0-{Factorials.Length - 1},当前数字{val}");
                return 0;
            }

            return Factorials[val];
        }
        
        /// <summary>
        /// C(n,k)
        /// </summary>
        public static ulong BinomialCoef(uint n, uint k)
        {
            ulong r = 1;
            if (k > n) return 0;

            for (ulong d = 1; d <= k; d++)
            {
                r *= n--;
                r /= d;
            }

            return r;
        }
        
        /// <summary>
        /// A(n,k)
        /// </summary>
        public static ulong Permutation(int n, int k)
        {
            if (n < 0 || k < 0 || k > n)
            {
                Debug.LogError("参数无效：0 ≤ k ≤ n");
                return 0;
            }
    
            if (n > 20)
            {
                Debug.LogError($"阶乘只支持 0-{Factorials.Length - 1}");
                return 0;
            }
    
            // 使用预计算的阶乘表
            return Factorials[n] / Factorials[n - k];
        }
    }
}