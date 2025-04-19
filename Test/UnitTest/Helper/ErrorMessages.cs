namespace UnitTest.Helper
{
    public static class ErrorMessages
    {
        public static class User
        {
            public const string IdIsRequired = "Id is required";                        // Indicates that the user ID is required for the operation.
            public const string IdIsInvalid = "Id is invalid";                          // Indicates that the provided user ID is not valid.
            public const string IdIsNotRequired = "Id is not required";                 // Indicates that the user ID is not required for the operation.
            public const string UserUsernameIsRequired = "Username is required";        // Indicates that the username is required for the operation.
            public const string UserFirstNameIsRequired = "First name is required";     // Indicates that the first name is required for the operation.
            public const string UserLastNameIsRequired = "Last name is required";       // Indicates that the last name is required for the operation.
            public const string EmailIsRequired = "Email is required";                  // Indicates that the email address is required for the operation.
            public const string EmailIsInvalid = "Email is invalid";                    // Indicates that the provided email address is not valid.
            public const string UserNotFound = "User not found";                        // Indicates that the user was not found in the system.
            public const string NoUserFound = "No users found";                         // Indicates that no users were found in the system.
            public const string UserAlreadyExists = "User already exists";              // Indicates that the user already exists in the system.
            public const string PasswordIsRequired = "Password is required";            // Indicates that the password is required for the operation.
            public const string UserIsNotActive = "User is not active";                 // Indicates that the user is not active and cannot perform the operation.
            public const string UserInsertDateIsRequired = "Insert date is required";   // Indicates that the insert date is required for the operation.
            public const string UserUpdateDateIsRequired = "Update date is required";   // Indicates that the update date is required for the operation.
        }
    }
}