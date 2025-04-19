namespace Application.Dto.Common
{
    public class BaseDto
    {
        #region Properties
        public int Id { get; set; }                 // Primary Key
        public bool IsActive { get; set; }          // Indicates if the record is active
        public bool IsDeleted { get; set; }         // Indicates if the record is deleted
        public DateTime InsertDate { get; set; }    // Date when the record was created
        public DateTime? UpdateDate { get; set; }   // Date when the record was last updated
        public DateTime? DeleteDate { get; set; }   // Date when the record was deleted
        #endregion
    }
}