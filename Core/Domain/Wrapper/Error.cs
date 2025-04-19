namespace Domain.Wrapper
{
    public class Error
    {
        #region Properties
        public List<string>? Errors { get; set; } = new List<string>(); // List of errors
        public string? Message { get; set; } = string.Empty;            // Message
        #endregion
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public Error()
        {
            // Default constructor
        }

        /// <summary>
        /// Constructor with message
        /// </summary>
        /// <param name="message"></param>
        public Error(string message)
        {
            Message = message;
            Errors = ListError(message);
        }

        /// <summary>
        /// Constructor with list of errors
        /// </summary>
        /// <param name="errors"></param>
        public Error(List<string> errors)
        {
            Errors = errors;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Add error to list
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private List<string>? ListError(string message)
        {
            // Make a new list
            var list = new List<string>();
            // Assign first element
            list.Add(message);
            // Return
            return list;
        }
        #endregion
    }
}