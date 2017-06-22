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
        public static E Get()
        {
            var result = new E();

            return result;
        }
    }

    public abstract class UniRxEvent<E, P1> : UniRxEvent
        where E : UniRxEvent<E, P1>, new()
    {
        protected P1 param1;

        public static E Get(P1 param1)
        {
            var result = new E();
            result.param1 = param1;

            return result;
        }
    }

    public abstract class UniRxEvent<E, P1, P2> : UniRxEvent
        where E : UniRxEvent<E, P1, P2>, new()
    {
        protected P1 param1;

        protected P2 param2;

        public static E Get(P1 param1, P2 param2)
        {
            var result = new E();
            result.param1 = param1;
            result.param2 = param2;

            return result;
        }
    }

    public abstract class UniRxEvent<E, P1, P2, P3> : UniRxEvent
        where E : UniRxEvent<E, P1, P2, P3>, new()
    {
        protected P1 param1;

        protected P2 param2;

        protected P3 param3;

        public static E Get(P1 param1, P2 param2, P3 param3)
        {
            var result = new E();
            result.param1 = param1;
            result.param2 = param2;
            result.param3 = param3;

            return result;
        }
    }

    public abstract class UniRxEvent<E, P1, P2, P3, P4> : UniRxEvent
        where E : UniRxEvent<E, P1, P2, P3, P4>, new()
    {
        protected P1 param1;

        protected P2 param2;

        protected P3 param3;

        protected P4 param4;

        public static E Get(P1 param1, P2 param2, P3 param3, P4 param4)
        {
            var result = new E();
            result.param1 = param1;
            result.param2 = param2;
            result.param3 = param3;
            result.param4 = param4;

            return result;
        }
    }
}
