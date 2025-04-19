using Domain.Parameter.Abstract;

namespace Domain.Parameter.Concrete
{
    public class ListRequestParameter : IListRequestParameter
    {
        #region Properties
        public int? PageNumber { get; set; } = 1;           // The current page number
        public int? PageSize { get; set; } = 10;            // The number of items per page
        public string? Search { get; set; } = null;         // The search term
        public string? SortBy { get; set; } = null;         // The field to sort by
        public string? SortOrder { get; set; } = null;      // The order to sort (ascending/descending)
        public DateTime? StartDate { get; set; } = null;    // The start date for filtering
        public DateTime? EndDate { get; set; } = null;      // The end date for filtering
        #endregion
    }
}