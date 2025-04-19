namespace Domain.Wrapper
{
    public class PagedModelResponse<T> : Response<T> where T : class
    {
        #region Properties
        public int PageNumber { get; set; } = 1;    // Current page number
        public int PageSize { get; set; } = 10;     // Size of each page
        public int TotalPages { get; set; } = 0;    // Total number of pages
        public int TotalItems { get; set; } = 0;    // Total number of items
        #endregion
        #region Methods
        /// <summary>
        /// This method is used to return a success response with data
        /// </summary>
        /// <param name="data"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalPages"></param>
        /// <param name="totalItems"></param>
        /// <returns></returns>
        public PagedModelResponse<T> Success(T data, int pageNumber, int pageSize, int totalPages, int totalItems)
            => new PagedModelResponse<T>
            {
                IsSuccess = true,
                Data = data,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalItems = totalItems
            };

        /// <summary>
        /// This method is used to return a success response with data
        /// </summary>
        /// <returns></returns>
        public PagedModelResponse<T> Success() => new PagedModelResponse<T> { IsSuccess = true };

        /// <summary>
        /// This method is used to return a failed response with error
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public PagedModelResponse<T> Fail(Error error) => new PagedModelResponse<T> { IsSuccess = false, Error = error };

        /// <summary>
        /// This method is used to return a failed response with error message
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public PagedModelResponse<T> Fail(string errorMessage) => new PagedModelResponse<T> { IsSuccess = false, Error = new Error(errorMessage) };
        #endregion
    }
}