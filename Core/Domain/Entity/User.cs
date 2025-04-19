using Domain.Entity.Common;

namespace Domain.Entity
{
    public class User : BaseEntity
    {
        #region Properties
        public string? Username { get; set; } = string.Empty;   // Unique username for the user
        public string? FirstName { get; set; } = string.Empty;  // User's first name
        public string? LastName { get; set; } = string.Empty;   // User's last name
        public string? Email { get; set; } = string.Empty;      // Unique email address for the user
        public string? Password { get; set; } = string.Empty;   // User's password
        #endregion
    }
}