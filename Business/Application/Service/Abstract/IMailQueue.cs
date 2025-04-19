using Application.Model;

namespace Application.Service.Abstract
{
    public interface IMailQueue
    {
        #region Signatures
        /// <summary>
        /// Enqueues an email to be sent.
        /// </summary>
        /// <param name="email"></param>
        void Enqueue(QueuedMailModel email);

        /// <summary>
        /// Dequeues an email to be sent.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<QueuedMailModel?> DequeueAsync(CancellationToken cancellationToken);
        #endregion
    }
}