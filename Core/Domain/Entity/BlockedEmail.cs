using Domain.Entity.Common;

namespace Domain.Entity
{
    public class BlockedEmail : BaseEntity
    {
        #region Properties
        public string Email { get; set; } = string.Empty;   // Email address to be blocked
        public int RetryCount { get; set; } = 1;            // Number of retries
        #endregion
    }
}