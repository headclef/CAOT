namespace Domain.Wrapper
{
    public class ModelResponse<T> : Response<T> where T : class
    {
        #region Methods
        /// <summary>
        /// This method is used to return a success response with data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ModelResponse<T> Success(T data) => new ModelResponse<T> { Data = data, IsSuccess = true };

        /// <summary>
        /// This method is used to return a success response without data
        /// </summary>
        /// <returns></returns>
        public ModelResponse<T> Success() => new ModelResponse<T> { IsSuccess = true };

        /// <summary>
        /// This method is used to return a failed response with error
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public ModelResponse<T> Fail(Error error) => new ModelResponse<T> { Error = error, IsSuccess = false };

        /// <summary>
        /// This method is used to return a failed response with error message
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public ModelResponse<T> Fail(string errorMessage) => new ModelResponse<T> { Error = new Error(errorMessage), IsSuccess = false };
        #endregion
    }
}