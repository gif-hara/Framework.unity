using System;
using MessagePipe;

namespace HK.Framework.MessageSystems
{
    /// <summary>
    /// メッセージの仲介を行うクラス
    /// </summary>
    public static class MessageBroker
    {
        public static void Setup(Action<BuiltinContainerBuilder> builderSetupAction)
        {
            var builder = new BuiltinContainerBuilder();
            builder.AddMessagePipe();
            builderSetupAction?.Invoke(builder);
            var provider = builder.BuildServiceProvider();
            GlobalMessagePipe.SetProvider(provider);
        }

        public static IPublisher<T> GetPublisher<T>()
        {
            return GlobalMessagePipe.GetPublisher<T>();
        }

        public static IPublisher<TKey, TValue> GetPublisher<TKey, TValue>()
        {
            return GlobalMessagePipe.GetPublisher<TKey, TValue>();
        }

        public static ISubscriber<T> GetSubscriber<T>()
        {
            return GlobalMessagePipe.GetSubscriber<T>();
        }

        public static ISubscriber<TKey, TValue> GetSubscriber<TKey, TValue>()
        {
            return GlobalMessagePipe.GetSubscriber<TKey, TValue>();
        }

        public static IAsyncPublisher<T> GetAsyncPublisher<T>()
        {
            return GlobalMessagePipe.GetAsyncPublisher<T>();
        }

        public static IAsyncPublisher<TKey, TMessage> GetAsyncPublisher<TKey, TMessage>()
        {
            return GlobalMessagePipe.GetAsyncPublisher<TKey, TMessage>();
        }

        public static IAsyncSubscriber<T> GetAsyncSubscriber<T>()
        {
            return GlobalMessagePipe.GetAsyncSubscriber<T>();
        }

        public static IAsyncSubscriber<TKey, TMessage> GetAsyncSubscriber<TKey, TMessage>()
        {
            return GlobalMessagePipe.GetAsyncSubscriber<TKey, TMessage>();
        }
    }
}
