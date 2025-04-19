using Application.Dto.Common;

namespace Application.Dto
{
    public class BlockedEmailDto : BaseDto
    {
        #region Properties
        public string Email { get; set; } = string.Empty;   // Email address to be blocked
        public int RetryCount { get; set; } = 1;            // Number of retry attempts
        #endregion
    }
}