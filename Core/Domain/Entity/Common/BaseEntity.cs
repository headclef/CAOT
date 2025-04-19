using System.ComponentModel.DataAnnotations;

namespace Domain.Entity.Common
{
    public class BaseEntity
    {
        #region Properties
        [Key]
        public int Id { get; set; }                 // Primary Key
        public bool IsActive { get; set; }          // Indicates if the entity is active
        public bool IsDeleted { get; set; }         // Indicates if the entity is deleted
        public DateTime InsertDate { get; set; }    // Date of entity creation
        public DateTime? UpdateDate { get; set; }   // Date of last update
        public DateTime? DeleteDate { get; set; }   // Date of deletion
        #endregion
    }
}