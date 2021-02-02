using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Natech.IpGeoFinder.Api.BackgroundServices
{
    public class BatchProcessingChannel
    {
        private const int MaxMessagesInChannel = 100;

        private readonly Channel<Guid> _channel;

        public BatchProcessingChannel()
        {
            var options = new BoundedChannelOptions(MaxMessagesInChannel)
            {
                SingleWriter = false,
                SingleReader = true
            };

            _channel = Channel.CreateBounded<Guid>(options);
        }

        public async Task<bool> AddBatchAsync(Guid batchid, CancellationToken ct = default)
        {
            while (await _channel.Writer.WaitToWriteAsync(ct) && !ct.IsCancellationRequested)
            {
                if (_channel.Writer.TryWrite(batchid))
                {
                    //Channel Message Written
                    return true;
                }
            }

            return false;
        }

        public IAsyncEnumerable<Guid> ReadAllAsync(CancellationToken ct = default) =>
            _channel.Reader.ReadAllAsync(ct);

        public bool TryCompleteWriter(Exception ex = null) => _channel.Writer.TryComplete(ex);


    }
}
