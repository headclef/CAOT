namespace Domain.Wrapper
{
    public class Response<T> where T : class
    {
        #region Properties
        public bool IsSuccess { get; set; } = true;         // Indicates if the response is successful
        public T Data { get; set; } = null!;                // The data returned in the response
        public Error Error { get; set; } = new Error();     // The error information, if any
        #endregion
    }
}