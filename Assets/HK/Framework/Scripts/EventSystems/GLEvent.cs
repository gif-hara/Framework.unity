using UnityEngine;
using UniRx;

namespace HK.GL.Events
{
    /// <summary>
    /// GLで扱うイベントの基底クラス.
    /// </summary>
    public abstract class GLEvent
    {
        public static IMessageBroker GlobalBroker { get { return MessageBroker.Default; } }
    }

    public abstract class GLEvent<E> : GLEvent
        where E : GLEvent<E>, new()
    {
        public static E Get()
        {
            var result = new E();

            return result;
        }
    }

    public abstract class GLEvent<E, P1> : GLEvent
        where E : GLEvent<E, P1>, new()
    {
        protected P1 param1;

        public static E Get(P1 param1)
        {
            var result = new E();
            result.param1 = param1;

            return result;
        }
    }

    public abstract class GLEvent<E, P1, P2> : GLEvent
        where E : GLEvent<E, P1, P2>, new()
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

    public abstract class GLEvent<E, P1, P2, P3> : GLEvent
        where E : GLEvent<E, P1, P2, P3>, new()
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

    public abstract class GLEvent<E, P1, P2, P3, P4> : GLEvent
        where E : GLEvent<E, P1, P2, P3, P4>, new()
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
