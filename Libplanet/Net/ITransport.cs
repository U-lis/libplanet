using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Libplanet.Net.Messages;

namespace Libplanet.Net
{
    public interface ITransport : IDisposable
    {
        Peer AsPeer { get; }

        IEnumerable<BoundPeer> Peers { get; }

        DateTimeOffset? LastMessageTimestamp { get; }

        Task StartAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task RunAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task StopAsync(
            TimeSpan waitFor,
            CancellationToken cancellationToken = default(CancellationToken));

        void AddEventHandler(EventHandler<Message> eventHandler);

        void RemoveEventHandler(EventHandler<Message> eventHandler);

        void RemoveAllEventHandlers();

        Task BootstrapAsync(
            IEnumerable<BoundPeer> bootstrapPeers,
            TimeSpan? pingSeedTimeout,
            TimeSpan? findNeighborsTimeout,
            int depth,
            CancellationToken cancellationToken);

        Task<Message> SendMessageWithReplyAsync(
            BoundPeer peer,
            Message message,
            TimeSpan? timeout,
            CancellationToken cancellationToken);

        Task<IEnumerable<Message>> SendMessageWithReplyAsync(
            BoundPeer peer,
            Message message,
            TimeSpan? timeout,
            int expectedResponses,
            CancellationToken cancellationToken = default(CancellationToken));

        void BroadcastMessage(Address? except, Message message);

        void ReplyMessage(Message message);
    }
}
