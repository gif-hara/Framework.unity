using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace HK.Framework.EventSystems
{
    /// <summary>
    /// メッセージを仲介するクラス
    /// </summary>
    public static class Broker
    {
        /// <summary>
        /// アプリケーション全体へ通知する<see cref="IMessageBroker"/>
        /// </summary>
        public static readonly IMessageBroker Global = MessageBroker.Default;
        
        /// <summary>
        /// <see cref="GameObject"/>に紐づく<see cref="IMessageBroker"/>
        /// </summary>
        public static readonly Dictionary<GameObject, IMessageBroker> GameObjects = new Dictionary<GameObject, IMessageBroker>();
    }
}
