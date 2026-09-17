using System;

namespace CatchLightning.Core.Abstractions.Presentation
{
    public interface IMediator
    {
        IDisposable Subscribe<T>(Action<T> handler);
        void Publish<T>(T message);
        IDisposable Subscribe<TToken, TMessage>(TToken token, Action<TMessage> handler) where TToken : notnull;
        void Publish<TToken, TMessage>(TToken token, TMessage message) where TToken : notnull;
    }
}
