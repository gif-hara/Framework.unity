using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using MessagePipe;

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
        
        private static TMessage Get()
        {
            return instance;
        }

        public static void Publish()
        {
            MessageBroker.GetPublisher<TMessage>()
                .Publish(Get());
        }

        public static void Publish<TKey>(TKey key)
        {
            MessageBroker.GetPublisher<TKey, TMessage>()
                .Publish(key, Get());
        }

        public static UniTask PublishAsync(CancellationToken cancellationToken = default)
        {
            return MessageBroker.GetAsyncPublisher<TMessage>()
                .PublishAsync(Get(), cancellationToken);
        }
        
        public static UniTask PublishAsync<TKey>(TKey key, CancellationToken cancellationToken = default)
        {
            return MessageBroker.GetAsyncPublisher<TKey, TMessage>()
                .PublishAsync(key, Get(), cancellationToken);
        }

        public static IDisposable Subscribe(IMessageHandler<TMessage> handler, params MessageHandlerFilter<TMessage>[] filters)
        {
            return MessageBroker.GetSubscriber<TMessage>()
                .Subscribe(handler, filters);
        }
        
        public static IDisposable Subscribe<TKey>(TKey key, IMessageHandler<TMessage> handler, params MessageHandlerFilter<TMessage>[] filters)
        {
            return MessageBroker.GetSubscriber<TKey, TMessage>()
                .Subscribe(key, handler, filters);
        }
        
        public static IDisposable SubscribeAsync(IAsyncMessageHandler<TMessage> asyncHandler, params AsyncMessageHandlerFilter<TMessage>[] filters)
        {
            return MessageBroker.GetAsyncSubscriber<TMessage>()
                .Subscribe(asyncHandler, filters);
        }
        
        public static IDisposable SubscribeAsync<TKey>(TKey key, IAsyncMessageHandler<TMessage> asyncHandler, params AsyncMessageHandlerFilter<TMessage>[] filters)
        {
            return MessageBroker.GetAsyncSubscriber<TKey, TMessage>()
                .Subscribe(key, asyncHandler, filters);
        }
    }

    public abstract class Message<TMessage, TParam1> : Message where TMessage : Message<TMessage, TParam1>, new()
    {
        private static TMessage instance = new();
        
        protected TParam1 Param1 { get; set; }

        private static TMessage Get(TParam1 param1)
        {
            instance.Param1 = param1;

            return instance;
        }
        
        public static void Publish(TParam1 param1)
        {
            MessageBroker.GetPublisher<TMessage>()
                .Publish(Get(param1));
        }
        
        public static void Publish<TKey>(TKey key, TParam1 param1)
        {
            MessageBroker.GetPublisher<TKey, TMessage>()
                .Publish(key, Get(param1));
        }
        
        public static UniTask PublishAsync(TParam1 param1, CancellationToken cancellationToken = default)
        {
            return MessageBroker.GetAsyncPublisher<TMessage>()
                .PublishAsync(Get(param1), cancellationToken);
        }
        
        public static UniTask PublishAsync<TKey>(TKey key, TParam1 param1, CancellationToken cancellationToken = default)
        {
            return MessageBroker.GetAsyncPublisher<TKey, TMessage>()
                .PublishAsync(key, Get(param1), cancellationToken);
        }
        
        public static IDisposable Subscribe(IMessageHandler<TMessage> handler, params MessageHandlerFilter<TMessage>[] filters)
        {
            return MessageBroker.GetSubscriber<TMessage>()
                .Subscribe(handler, filters);
        }
        
        public static IDisposable Subscribe<TKey>(TKey key, IMessageHandler<TMessage> handler, params MessageHandlerFilter<TMessage>[] filters)
        {
            return MessageBroker.GetSubscriber<TKey, TMessage>()
                .Subscribe(key, handler, filters);
        }
        
        public static IDisposable SubscribeAsync(IAsyncMessageHandler<TMessage> asyncHandler, params AsyncMessageHandlerFilter<TMessage>[] filters)
        {
            return MessageBroker.GetAsyncSubscriber<TMessage>()
                .Subscribe(asyncHandler, filters);
        }
        
        public static IDisposable SubscribeAsync<TKey>(TKey key, IAsyncMessageHandler<TMessage> asyncHandler, params AsyncMessageHandlerFilter<TMessage>[] filters)
        {
            return MessageBroker.GetAsyncSubscriber<TKey, TMessage>()
                .Subscribe(key, asyncHandler, filters);
        }
    }

    public abstract class Message<TMessage, TParam1, TParam2> : Message where TMessage : Message<TMessage, TParam1, TParam2>, new()
    {
        private static TMessage instance = new();
        
        protected TParam1 Param1 { get; set; }
        
        protected TParam2 Param2 { get; set; }

        private static TMessage Get(TParam1 param1, TParam2 param2)
        {
            instance.Param1 = param1;
            instance.Param2 = param2;

            return instance;
        }
        
        public static void Publish(TParam1 param1, TParam2 param2)
        {
            MessageBroker.GetPublisher<TMessage>()
                .Publish(Get(param1, param2));
        }
        
        public static void Publish<TKey>(TKey key, TParam1 param1, TParam2 param2)
        {
            MessageBroker.GetPublisher<TKey, TMessage>()
                .Publish(key, Get(param1, param2));
        }
        
        public static UniTask PublishAsync(TParam1 param1, TParam2 param2, CancellationToken cancellationToken = default)
        {
            return MessageBroker.GetAsyncPublisher<TMessage>()
                .PublishAsync(Get(param1, param2), cancellationToken);
        }
        
        public static UniTask PublishAsync<TKey>(TKey key, TParam1 param1, TParam2 param2, CancellationToken cancellationToken = default)
        {
            return MessageBroker.GetAsyncPublisher<TKey, TMessage>()
                .PublishAsync(key, Get(param1, param2), cancellationToken);
        }
        
        public static IDisposable Subscribe(IMessageHandler<TMessage> handler, params MessageHandlerFilter<TMessage>[] filters)
        {
            return MessageBroker.GetSubscriber<TMessage>()
                .Subscribe(handler, filters);
        }
        
        public static IDisposable Subscribe<TKey>(TKey key, IMessageHandler<TMessage> handler, params MessageHandlerFilter<TMessage>[] filters)
        {
            return MessageBroker.GetSubscriber<TKey, TMessage>()
                .Subscribe(key, handler, filters);
        }
        
        public static IDisposable SubscribeAsync(IAsyncMessageHandler<TMessage> asyncHandler, params AsyncMessageHandlerFilter<TMessage>[] filters)
        {
            return MessageBroker.GetAsyncSubscriber<TMessage>()
                .Subscribe(asyncHandler, filters);
        }
        
        public static IDisposable SubscribeAsync<TKey>(TKey key, IAsyncMessageHandler<TMessage> asyncHandler, params AsyncMessageHandlerFilter<TMessage>[] filters)
        {
            return MessageBroker.GetAsyncSubscriber<TKey, TMessage>()
                .Subscribe(key, asyncHandler, filters);
        }
    }

    public abstract class Message<TMessage, TParam1, TParam2, TParam3> : Message where TMessage : Message<TMessage, TParam1, TParam2, TParam3>, new()
    {
        private static TMessage instance = new();
        
        protected TParam1 Param1 { get; set; }
        
        protected TParam2 Param2 { get; set; }

        protected TParam3 Param3 { get; set; }

        private static TMessage Get(TParam1 param1, TParam2 param2, TParam3 param3)
        {
            instance.Param1 = param1;
            instance.Param2 = param2;
            instance.Param3 = param3;

            return instance;
        }
        
        public static void Publish(TParam1 param1, TParam2 param2, TParam3 param3)
        {
            MessageBroker.GetPublisher<TMessage>()
                .Publish(Get(param1, param2, param3));
        }
        
        public static void Publish<TKey>(TKey key, TParam1 param1, TParam2 param2, TParam3 param3)
        {
            MessageBroker.GetPublisher<TKey, TMessage>()
                .Publish(key, Get(param1, param2, param3));
        }
        
        public static UniTask PublishAsync(TParam1 param1, TParam2 param2, TParam3 param3, CancellationToken cancellationToken = default)
        {
            return MessageBroker.GetAsyncPublisher<TMessage>()
                .PublishAsync(Get(param1, param2, param3), cancellationToken);
        }
        
        public static UniTask PublishAsync<TKey>(TKey key, TParam1 param1, TParam2 param2, TParam3 param3, CancellationToken cancellationToken = default)
        {
            return MessageBroker.GetAsyncPublisher<TKey, TMessage>()
                .PublishAsync(key, Get(param1, param2, param3), cancellationToken);
        }
        
        public static IDisposable Subscribe(IMessageHandler<TMessage> handler, params MessageHandlerFilter<TMessage>[] filters)
        {
            return MessageBroker.GetSubscriber<TMessage>()
                .Subscribe(handler, filters);
        }
        
        public static IDisposable Subscribe<TKey>(TKey key, IMessageHandler<TMessage> handler, params MessageHandlerFilter<TMessage>[] filters)
        {
            return MessageBroker.GetSubscriber<TKey, TMessage>()
                .Subscribe(key, handler, filters);
        }
        
        public static IDisposable SubscribeAsync(IAsyncMessageHandler<TMessage> asyncHandler, params AsyncMessageHandlerFilter<TMessage>[] filters)
        {
            return MessageBroker.GetAsyncSubscriber<TMessage>()
                .Subscribe(asyncHandler, filters);
        }
        
        public static IDisposable SubscribeAsync<TKey>(TKey key, IAsyncMessageHandler<TMessage> asyncHandler, params AsyncMessageHandlerFilter<TMessage>[] filters)
        {
            return MessageBroker.GetAsyncSubscriber<TKey, TMessage>()
                .Subscribe(key, asyncHandler, filters);
        }
    }

    public abstract class Message<TMessage, TParam1, TParam2, TParam3, TParam4> : Message where TMessage : Message<TMessage, TParam1, TParam2, TParam3, TParam4>, new()
    {
        private static TMessage instance = new();
        
        protected TParam1 Param1 { get; set; }
        
        protected TParam2 Param2 { get; set; }

        protected TParam3 Param3 { get; set; }

        protected TParam4 Param4 { get; set; }

        private static TMessage Get(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4)
        {
            instance.Param1 = param1;
            instance.Param2 = param2;
            instance.Param3 = param3;
            instance.Param4 = param4;

            return instance;
        }
        
        public static void Publish(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4)
        {
            MessageBroker.GetPublisher<TMessage>()
                .Publish(Get(param1, param2, param3, param4));
        }
        
        public static void Publish<TKey>(TKey key, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4)
        {
            MessageBroker.GetPublisher<TKey, TMessage>()
                .Publish(key, Get(param1, param2, param3, param4));
        }
        
        public static UniTask PublishAsync(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, CancellationToken cancellationToken = default)
        {
            return MessageBroker.GetAsyncPublisher<TMessage>()
                .PublishAsync(Get(param1, param2, param3, param4), cancellationToken);
        }
        
        public static UniTask PublishAsync<TKey>(TKey key, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, CancellationToken cancellationToken = default)
        {
            return MessageBroker.GetAsyncPublisher<TKey, TMessage>()
                .PublishAsync(key, Get(param1, param2, param3, param4), cancellationToken);
        }
        
        public static IDisposable Subscribe(IMessageHandler<TMessage> handler, params MessageHandlerFilter<TMessage>[] filters)
        {
            return MessageBroker.GetSubscriber<TMessage>()
                .Subscribe(handler, filters);
        }
        
        public static IDisposable Subscribe<TKey>(TKey key, IMessageHandler<TMessage> handler, params MessageHandlerFilter<TMessage>[] filters)
        {
            return MessageBroker.GetSubscriber<TKey, TMessage>()
                .Subscribe(key, handler, filters);
        }
        
        public static IDisposable SubscribeAsync(IAsyncMessageHandler<TMessage> asyncHandler, params AsyncMessageHandlerFilter<TMessage>[] filters)
        {
            return MessageBroker.GetAsyncSubscriber<TMessage>()
                .Subscribe(asyncHandler, filters);
        }
        
        public static IDisposable SubscribeAsync<TKey>(TKey key, IAsyncMessageHandler<TMessage> asyncHandler, params AsyncMessageHandlerFilter<TMessage>[] filters)
        {
            return MessageBroker.GetAsyncSubscriber<TKey, TMessage>()
                .Subscribe(key, asyncHandler, filters);
        }
    }
}
