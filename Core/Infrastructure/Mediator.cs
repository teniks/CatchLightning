using CatchLightning.Core.Abstractions.Presentation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace CatchLightning.Core.Infrastructure
{
    public class Mediator : IMediator
    {
        /// Able to direct event to getter
        private readonly record struct Key(Type messageType, object? token = null);
        private readonly Dictionary<Key, List<Subscription>> _subs = new();
        private readonly Lock gate = new Lock();


        /// <summary>
        /// A <paramref name="handler"/> for the event struct type
        /// </summary>
        /// <typeparam name="T">Event struct type</typeparam>
        /// <param name="handler">The reaction on event struct type</param>
        /// <returns>A <see cref="Unsubscription"/> for dispose link</returns>
        public IDisposable Subscribe<T>(Action<T> handler)
        {
            return Add(new Key(typeof(T)), handler);
        }

        /// <summary>
        /// A <paramref name="message"/> is the struct type for invoke event
        /// </summary>
        /// <typeparam name="T">Event struct type</typeparam>
        public void Publish<T>(T message)
        {
            Dispatch(new Key(typeof(T)), message);
        }

        /// <summary>
        /// A <paramref name="handler"/> for the event struct type
        /// </summary>
        /// <typeparam name="TMessage">Event struct type</typeparam>
        /// <typeparam name="TToken">Strurct type for detects getter</typeparam>
        /// <param name="handler">The reaction on event struct type</param>
        /// <returns>A <see cref="Unsubscription"/> for dispose link</returns>
        public IDisposable Subscribe<TToken, TMessage>(TToken token, Action<TMessage> handler) where TToken : notnull
        {
            return Add(new Key(typeof(TMessage), token), handler);
        }

        /// <summary>
        /// A <paramref name="message"/> is the struct type for invoke event
        /// </summary>
        /// <typeparam name="TMessage">Event struct type</typeparam>
        /// <typeparam name="TToken">Strurct type for detects getter</typeparam>
        public void Publish<TToken, TMessage>(TToken token, TMessage message) where TToken : notnull
        {
            Dispatch(new Key(typeof(TMessage), token), message);
        }

        private IDisposable Add<T>(Key key, Action<T> handler)
        {
            lock (gate)
            {
                if (!_subs.TryGetValue(key, out var list))
                    _subs[key] = list = new List<Subscription>();

                var sub = new Subscription(handler);
                list.Add(sub);

                // Delegate implement Unsubscription by remove handler or key
                return new Unsubscription(() =>
                {
                    lock (gate)
                    {
                        list.Remove(sub);
                        if (list.Count == 0) _subs.Remove(key);
                    }
                });
            }
        }

        private void Dispatch<T>(Key key, T message)
        {
            Subscription[] snapshot;
            lock (gate)
            {
                if (!_subs.TryGetValue(key, out var list))
                    return;
                snapshot = list.ToArray();
            }

            foreach (var sub in snapshot)
                sub.Invoke(message);
        }

        private sealed class Subscription
        {
            private readonly Delegate _handler;
            public Subscription(Delegate handler) => _handler = handler;
            public void Invoke<T>(T message) => ((Action<T>)_handler)(message);
        }

        public sealed class Unsubscription : IDisposable
        {
            private readonly Action _dispose;
            private bool _disposed;
            public Unsubscription(Action dispose) => _dispose = dispose;
            public void Dispose()
            {
                if (_disposed) return;
                _disposed = true;
                _dispose();
            }
        }
    }
}
