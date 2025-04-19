using Domain.Repository.Abstract.Common;
using Domain.Wrapper;
using Domain.Entity.Common;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System.Linq.Expressions;

namespace Persistence.Respository.Concrete.Common
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        #region Properties
        private readonly BaseDbContext _dbContext;  // DbContext instance
        #endregion
        #region Constructors
        /// <summary>
        /// Constructor for BaseRepository that takes a DbContext as a parameter.
        /// </summary>
        /// <param name="dbContext"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public BaseRepository(BaseDbContext dbContext)
        {
            // Initialize the DbContext
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        #endregion
        #region Methods
        public async Task<ModelResponse<T>> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            // Check if entity is null
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            // Check cancellation token status
            cancellationToken.ThrowIfCancellationRequested();

            // Add entity to the database
            await _dbContext.Set<T>().AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            // Return success response
            return new ModelResponse<T>().Success(entity);
        }

        public async Task<ModelResponse<T>> UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            // Check if entity is null
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            // Check cancellation token status
            cancellationToken.ThrowIfCancellationRequested();

            // Update entity in the database
            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            // Return success response
            return new ModelResponse<T>().Success(entity);
        }

        public async Task<ModelResponse<T>> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            // Check if id is less than 1
            if (id < 1)
                throw new ArgumentOutOfRangeException(nameof(id));

            // Check cancellation token status
            cancellationToken.ThrowIfCancellationRequested();

            // Get entity by id
            var entity = await _dbContext.Set<T>().FindAsync(new object[] { id }, cancellationToken);

            // Check if entity is null
            if (entity is null)
                return new ModelResponse<T>().Fail("Entity not found");

            // Delete entity from the database
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            // Return success response
            return new ModelResponse<T>().Success(entity);
        }

        public async Task<ModelResponse<T>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            // Check if id is less than 1
            if (id < 1)
                throw new ArgumentOutOfRangeException(nameof(id));

            // Check cancellation token status
            cancellationToken.ThrowIfCancellationRequested();

            // Get entity by id
            var entity = await _dbContext.Set<T>().FindAsync(new object[] { id }, cancellationToken);

            // Check if entity is null
            if (entity is null)
                return new ModelResponse<T>().Fail("Entity not found");

            // Return success response
            return new ModelResponse<T>().Success(entity);
        }

        public async Task<ModelResponse<IEnumerable<T>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            // Check cancellation token status
            cancellationToken.ThrowIfCancellationRequested();

            // Count all entities
            var totalItems = await _dbContext.Set<T>().CountAsync(cancellationToken);

            // Get all entities
            var entities = await _dbContext.Set<T>()
                .ToListAsync(cancellationToken);

            // Check if entities is null or empty
            if (entities is null)
                return new ModelResponse<IEnumerable<T>>().Fail("An error occured");

            // Return success response
            return new ModelResponse<IEnumerable<T>>().Success(entities);
        }

        public async Task<PagedModelResponse<IEnumerable<T>>> GetAllByParametersAsync(
            Expression<Func<T, bool>>? expression,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IQueryable<T>>? filter = null,
            int? pageNumber = null,
            int? pageSize = null,
            bool enableTracking = false,
            bool ignoreQueryFilters = false,
            bool enableSplitQuery = false,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            // Get all entities by parameters
            IQueryable<T> query = _dbContext.Set<T>();
            if (!enableTracking)
                query = query.AsNoTracking();
            if (ignoreQueryFilters)
                query = query.IgnoreQueryFilters();
            if (enableSplitQuery)
                query = query.AsSplitQuery();
            if (expression is not null)
                query = query.Where(expression);
            if (filter is not null)
                query = filter(query);
            if (orderBy is not null)
                query = orderBy(query);

            // Set default values
            pageNumber ??= 1;
            pageSize ??= 10;

            // Count all entities
            var totalItemsTask = query.CountAsync(cancellationToken);

            // Get all entities by parameters
            var entitiesTask = query
                .Skip((pageNumber.Value - 1) * pageSize.Value)
                .Take(pageSize.Value)
                .ToListAsync(cancellationToken);

            // Wait for the tasks to complete
            await Task.WhenAll(entitiesTask, totalItemsTask);

            // Get total items
            var totalItems = totalItemsTask.Result;

            // Get entities
            var entities = entitiesTask.Result;

            // Check if entities is null or empty
            if (entities is null)
                return new PagedModelResponse<IEnumerable<T>>().Fail("Entities not found");

            // Return success response
            return new PagedModelResponse<IEnumerable<T>>().Success(
                entities, pageNumber.Value, pageSize.Value, (int)Math.Ceiling(totalItems / (double)pageSize.Value), totalItems
            );
        }

        public async Task<ModelResponse<T>> TruncateAsync(CancellationToken cancellationToken)
        {
            // Check cancellation token status
            cancellationToken.ThrowIfCancellationRequested();

            // Check if there is any data in the table
            if (!_dbContext.Set<T>().Any())
                return new ModelResponse<T>().Fail("Table is empty");

            // Truncate the table
            _dbContext.Set<T>().RemoveRange(_dbContext.Set<T>());
            await _dbContext.SaveChangesAsync();

            // Return success response
            return new ModelResponse<T>().Success();
        }
        #endregion
    }
}