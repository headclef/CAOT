using Domain.Entity.Common;
using Domain.Wrapper;
using System.Linq.Expressions;

namespace Domain.Repository.Abstract.Common
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        #region Signatures
        /// <summary>
        /// This method is used to add an entity to the database
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ModelResponse<T>> AddAsync(T entity, CancellationToken cancellationToken);

        /// <summary>
        /// This method is used to update an entity in the database
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ModelResponse<T>> UpdateAsync(T entity, CancellationToken cancellationToken);

        /// <summary>
        /// This method is used to delete an entity from the database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ModelResponse<T>> DeleteAsync(int id, CancellationToken cancellationToken);

        /// <summary>
        /// This method is used to get an entity by its id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ModelResponse<T>> GetByIdAsync(int id, CancellationToken cancellationToken);

        /// <summary>
        /// This method is used to get all entities from the database
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ModelResponse<IEnumerable<T>>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// This method is used to get all entities from the database
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="orderBy"></param>
        /// <param name="filter"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="enableTracking"></param>
        /// <param name="ignoreQueryFilters"></param>
        /// <param name="enableSplitQuery"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<PagedModelResponse<IEnumerable<T>>> GetAllByParametersAsync(
            Expression<Func<T, bool>> expression,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
            Func<IQueryable<T>, IQueryable<T>>? filter,
            int? pageNumber,
            int? pageSize,
            bool enableTracking,
            bool ignoreQueryFilters,
            bool enableSplitQuery,
            CancellationToken cancellationToken
        );

        /// <summary>
        /// This method is used to get all entities from the database
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ModelResponse<T>> TruncateAsync(CancellationToken cancellationToken);
        #endregion
    }
}