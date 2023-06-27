using System;
using System.Collections.Generic;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

namespace HK.Framework
{
    /// <summary>
    /// <see cref="IWeight"/>に関する拡張関数
    /// </summary>
    public static class IWeightExtensions
    {
        /// <summary>
        /// 抽選を行う
        /// </summary>
        public static T Lottery<T>(this IList<T> self) where T : IWeight
        {
            return Lottery(self, max => Random.Range(0, max));
        }

        /// <summary>
        /// 抽選を行う
        /// </summary>
        public static int LotteryIndex<T>(this IList<T> self) where T : IWeight
        {
            return LotteryIndex(self, max => Random.Range(0, max));
        }

        /// <summary>
        /// 抽選を行う
        /// </summary>
        /// <remarks>
        /// 乱数を独自で実装したい場合に利用します
        /// </remarks>
        public static T Lottery<T>(this IList<T> self, Func<int, int> randomSelector) where T : IWeight
        {
            var max = 0;
            foreach (var i in self)
            {
                max += i.Weight;
            }

            var current = 0;
            var random = randomSelector(max);
            foreach (var i in self)
            {
                if (random >= current && random < current + i.Weight)
                {
                    return i;
                }

                current += i.Weight;
            }

            Assert.IsTrue(false, "未定義の動作です");
            return default(T);
        }
        /// <summary>
        /// 抽選を行う
        /// </summary>
        /// <remarks>
        /// 乱数を独自で実装したい場合に利用します
        /// </remarks>
        public static int LotteryIndex<T>(this IList<T> self, Func<int, int> randomSelector) where T : IWeight
        {
            var max = 0;
            foreach (var i in self)
            {
                max += i.Weight;
            }

            var current = 0;
            var random = randomSelector(max);
            for (var i = 0; i < self.Count; i++)
            {
                var element = self[i];
                if (random >= current && random < current + element.Weight)
                {
                    return i;
                }

                current += element.Weight;
            }

            Assert.IsTrue(false, "未定義の動作です");
            return -1;
        }
    }
}
