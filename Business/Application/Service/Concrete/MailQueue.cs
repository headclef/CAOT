using Application.Model;
using Application.Service.Abstract;
using System.Collections.Concurrent;

namespace Application.Service.Concrete
{
    public class MailQueue : IMailQueue
    {
        #region Properties
        private readonly ConcurrentQueue<QueuedMailModel> _queue = new();   // Thread-safe queue for storing emails
        private readonly SemaphoreSlim _signal = new(0);                    // SemaphoreSlim to signal when an email is available in the queue
        #endregion
        #region Methods
        public void Enqueue(QueuedMailModel email)
        {
            // Enqueue the email
            _queue.Enqueue(email);

            // Release a signal
            _signal.Release();
        }

        public async Task<QueuedMailModel?> DequeueAsync(CancellationToken cancellationToken)
        {
            // Wait for a signal to be released
            await _signal.WaitAsync(cancellationToken);

            // Try to dequeue an email
            _queue.TryDequeue(out var email);

            // Return the email
            return email;
        }
        #endregion
    }
}