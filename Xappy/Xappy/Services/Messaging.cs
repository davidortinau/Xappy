using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Xappy
{
    public class Messaging
    {
        #region Singleton
        private Messaging() { }
        private static Lazy<Messaging> _instance = new Lazy<Messaging>(() => new Messaging());
        public static Messaging Instance => _instance.Value;
        #endregion

        private readonly ChannelSubscriptions Channels = new ChannelSubscriptions();

        public void Subscribe(string channel, string subscriberKey, Action response)
        {
            var channelKey = new ChannelKey(channel, null);
            List<Subscription> subs = GetSubscriptions(channelKey);
            subs.RemoveAll(s => s.SusbcriberKey == subscriberKey);
            subs.Add(new Subscription(subscriberKey, response));
            Channels[channelKey] = subs;
        }

        public void Subscribe<T>(string channel, string subscriberKey, Action<T> response)
        {
            var channelKey = new ChannelKey(channel, typeof(T));
            List<Subscription> subs = GetSubscriptions(channelKey);
            subs.RemoveAll(s => s.SusbcriberKey == subscriberKey);
            subs.Add(new Subscription(subscriberKey, response));
            Channels[channelKey] = subs;
        }

        public void Unsubscribe(string channel, string subscriberKey)
        {
            var channelKey = new ChannelKey(channel, null);
            List<Subscription> subs = GetSubscriptions(channelKey);
            subs.RemoveAll(s => s.SusbcriberKey == subscriberKey);
        }

        public void Unsubscribe<T>(string channel, string subscriberKey)
        {
            var channelKey = new ChannelKey(channel, typeof(T));
            List<Subscription> subs = GetSubscriptions(channelKey);
            subs.RemoveAll(s => s.SusbcriberKey == subscriberKey);
        }

        public void Publish(string channel)
        {
            var channelKey = new ChannelKey(channel, null);
            List<Subscription> subs = GetSubscriptions(channelKey);
            foreach (var sub in subs)
            {
                if (sub.Reponse.IsAlive)
                {
                    Action subResponse = sub.Reponse.Target as Action;
                    subResponse?.Invoke();
                }
            }
        }

        public void Publish<T>(string channel, T parameter)
        {
            var channelKey = new ChannelKey(channel, typeof(T));
            List<Subscription> subs = GetSubscriptions(channelKey);
            foreach (var sub in subs)
            {
                if (sub.Reponse.IsAlive)
                {
                    Action<T> subResponse = sub.Reponse.Target as Action<T>;
                    subResponse?.Invoke(parameter);
                }
            }
        }

        public void ClearSubscriptions(string channel)
            => Channels.TryRemove(new ChannelKey(channel, null), out List<Subscription> value);

        public void ClearSubscriptions<T>(string channel)
            => Channels.TryRemove(new ChannelKey(channel, typeof(T)), out List<Subscription> value);

        private List<Subscription> GetSubscriptions(ChannelKey key)
            => Channels.ContainsKey(key)
                ? Channels[key] ?? new List<Subscription>()
                : new List<Subscription>();

        private class ChannelKey : Tuple<string, Type>
        {
            public ChannelKey(string channel, Type paramaterType) : base(channel, paramaterType) { }
            public string Channel => Item1;
            public Type ParameterType => Item2;
        }

        private class Subscription : Tuple<string, WeakReference>
        {
            public Subscription(string subscriberKey, object response)
                : base(subscriberKey, new WeakReference(response)) { }
            public string SusbcriberKey => Item1;
            public WeakReference Reponse => Item2;
        }

        private class ChannelSubscriptions : ConcurrentDictionary<ChannelKey, List<Subscription>> { }
    }
}
