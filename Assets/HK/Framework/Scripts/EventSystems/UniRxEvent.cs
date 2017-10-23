using UniRx;

namespace HK.Framework.EventSystems
{
    /// <summary>
    /// UniRxイベントの基底クラス.
    /// </summary>
    public abstract class UniRxEvent
    {
        public static IMessageBroker GlobalBroker { get { return MessageBroker.Default; } }
    }

    public abstract class UniRxEvent<E> : UniRxEvent
        where E : UniRxEvent<E>, new()
    {
        /// <summary>
        /// イベントを取得します
        /// </summary>
        public static E Get()
        {
            var result = new E();

            return result;
        }
        
        protected static E cache = new E();

        /// <summary>
        /// キャッシュイベントを取得します
        /// </summary>
        /// <remarks>
        /// キャッシュを利用しているため<c>new</c>はしませんがスレッドセーフではありません
        /// </remarks>
        public static E GetCache()
        {
            return cache;
        }
    }

    public abstract class UniRxEvent<E, P1> : UniRxEvent
        where E : UniRxEvent<E, P1>, new()
    {
        protected P1 param1;

        /// <summary>
        /// イベントを取得します
        /// </summary>
        public static E Get(P1 param1)
        {
            var result = new E();
            result.param1 = param1;

            return result;
        }
        
        protected static E cache = new E();

        /// <summary>
        /// キャッシュイベントを取得します
        /// </summary>
        /// <remarks>
        /// キャッシュを利用しているため<c>new</c>はしませんがスレッドセーフではありません
        /// </remarks>
        public static E GetCache(P1 param1)
        {
            cache.param1 = param1;
            return cache;
        }
    }

    public abstract class UniRxEvent<E, P1, P2> : UniRxEvent
        where E : UniRxEvent<E, P1, P2>, new()
    {
        protected P1 param1;

        protected P2 param2;

        /// <summary>
        /// イベントを取得します
        /// </summary>
        public static E Get(P1 param1, P2 param2)
        {
            var result = new E();
            result.param1 = param1;
            result.param2 = param2;

            return result;
        }
        
        protected static E cache = new E();

        /// <summary>
        /// キャッシュイベントを取得します
        /// </summary>
        /// <remarks>
        /// キャッシュを利用しているため<c>new</c>はしませんがスレッドセーフではありません
        /// </remarks>
        public static E GetCache(P1 param1, P2 param2)
        {
            cache.param1 = param1;
            cache.param2 = param2;
            return cache;
        }
    }

    public abstract class UniRxEvent<E, P1, P2, P3> : UniRxEvent
        where E : UniRxEvent<E, P1, P2, P3>, new()
    {
        protected P1 param1;

        protected P2 param2;

        protected P3 param3;

        /// <summary>
        /// イベントを取得します
        /// </summary>
        public static E Get(P1 param1, P2 param2, P3 param3)
        {
            var result = new E();
            result.param1 = param1;
            result.param2 = param2;
            result.param3 = param3;

            return result;
        }
        
        protected static E cache = new E();

        /// <summary>
        /// キャッシュイベントを取得します
        /// </summary>
        /// <remarks>
        /// キャッシュを利用しているため<c>new</c>はしませんがスレッドセーフではありません
        /// </remarks>
        public static E GetCache(P1 param1, P2 param2, P3 param3)
        {
            cache.param1 = param1;
            cache.param2 = param2;
            cache.param3 = param3;
            return cache;
        }
    }

    public abstract class UniRxEvent<E, P1, P2, P3, P4> : UniRxEvent
        where E : UniRxEvent<E, P1, P2, P3, P4>, new()
    {
        protected P1 param1;

        protected P2 param2;

        protected P3 param3;

        protected P4 param4;

        /// <summary>
        /// イベントを取得します
        /// </summary>
        public static E Get(P1 param1, P2 param2, P3 param3, P4 param4)
        {
            var result = new E();
            result.param1 = param1;
            result.param2 = param2;
            result.param3 = param3;
            result.param4 = param4;

            return result;
        }
        
        protected static E cache = new E();

        /// <summary>
        /// キャッシュイベントを取得します
        /// </summary>
        /// <remarks>
        /// キャッシュを利用しているため<c>new</c>はしませんがスレッドセーフではありません
        /// </remarks>
        public static E GetCache(P1 param1, P2 param2, P3 param3, P4 param4)
        {
            cache.param1 = param1;
            cache.param2 = param2;
            cache.param3 = param3;
            cache.param4 = param4;
            return cache;
        }
    }
}
