namespace HK.Framework.MessageSystems
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class Message
    {
    }

    public abstract class Message<TMessage> : Message where TMessage : Message<TMessage>, new()
    {
        private static TMessage instance = new();
        
        public static TMessage Get()
        {
            return instance;
        }
    }

    public abstract class Message<TMessage, TParam1> : Message where TMessage : Message<TMessage, TParam1>, new()
    {
        private static TMessage instance = new();
        
        protected TParam1 Param1 { get; set; }

        public static TMessage Get(TParam1 param1)
        {
            instance.Param1 = param1;

            return instance;
        }
    }

    public abstract class Message<TMessage, TParam1, TParam2> : Message where TMessage : Message<TMessage, TParam1, TParam2>, new()
    {
        private static TMessage instance = new();
        
        protected TParam1 Param1 { get; set; }
        
        protected TParam2 Param2 { get; set; }

        public static TMessage Get(TParam1 param1, TParam2 param2)
        {
            instance.Param1 = param1;
            instance.Param2 = param2;

            return instance;
        }
    }

    public abstract class Message<TMessage, TParam1, TParam2, TParam3> : Message where TMessage : Message<TMessage, TParam1, TParam2, TParam3>, new()
    {
        private static TMessage instance = new();
        
        protected TParam1 Param1 { get; set; }
        
        protected TParam2 Param2 { get; set; }

        protected TParam3 Param3 { get; set; }

        public static TMessage Get(TParam1 param1, TParam2 param2, TParam3 param3)
        {
            instance.Param1 = param1;
            instance.Param2 = param2;
            instance.Param3 = param3;

            return instance;
        }
    }

    public abstract class Message<TMessage, TParam1, TParam2, TParam3, TParam4> : Message where TMessage : Message<TMessage, TParam1, TParam2, TParam3, TParam4>, new()
    {
        private static TMessage instance = new();
        
        protected TParam1 Param1 { get; set; }
        
        protected TParam2 Param2 { get; set; }

        protected TParam3 Param3 { get; set; }

        protected TParam4 Param4 { get; set; }

        public static TMessage Get(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4)
        {
            instance.Param1 = param1;
            instance.Param2 = param2;
            instance.Param3 = param3;
            instance.Param4 = param4;

            return instance;
        }
    }
}
