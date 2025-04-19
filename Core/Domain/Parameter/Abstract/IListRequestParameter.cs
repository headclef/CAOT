namespace Domain.Parameter.Abstract
{
    public interface IListRequestParameter
    {
        #region Properties
        public int? PageNumber { get; set; }        // The current page number
        public int? PageSize { get; set; }          // The number of items per page
        public string? Search { get; set; }         // The search term
        public string? SortBy { get; set; }         // The field to sort by
        public string? SortOrder { get; set; }      // The order to sort (ascending/descending)
        public DateTime? StartDate { get; set; }    // The start date for filtering
        public DateTime? EndDate { get; set; }      // The end date for filtering
        #endregion
    }
}