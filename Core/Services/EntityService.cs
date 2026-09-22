using CatchLightning.Core.Abstractions;
using CatchLightning.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatchLightning.Core.Services
{
    public abstract class EntityService<TEntity, TEnumErrors> : IGetterById<TEntity>
        where TEntity : class, IEntity
        where TEnumErrors : Enum
    {
        protected GenericRepository<TEntity> repository;
        protected IValidator<TEntity, TEnumErrors> validator;

        /// <param name="repository">contains the validation logic for entities</param>
        /// <param name="validator">Validation chain can be either a single validator or a composite of multiple validators. </param>
        public EntityService(GenericRepository<TEntity> repository, IValidator<TEntity, TEnumErrors> validator)
        {
            this.repository = repository;
            this.validator = validator;
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            return await repository.GetByIDAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAsync(int count, int skip = 0)
        {
            return await repository.GetPartFromAsync(count, skip);
        }

        public async Task<TEntity?> GetByNameAsync(string name)
        {
            return await repository.GetByNameAsync(name);
        }

        /// <summary>
        /// Add a new entity in the repository after validating it. <br/>
        /// If the validation fails, it returns an <see cref="OperationResult{TEnumErrors}"/> with the error.
        /// </summary>
        public async Task<OperationResult<TEnumErrors>> AddAsync(TEntity entity)
        {
            TEnumErrors? result = await validator.Validate(entity);
            if (result != null)
                return OperationResult<TEnumErrors>.Failure(result);

            await repository.AddAsync(entity);
            await repository.SaveChangesAsync();
            return OperationResult<TEnumErrors>.Success();
        }

        /// <summary>
        /// Update an existing entity in the repository after validating it. <br/>
        /// If the validation fails, it returns an <see cref="OperationResult{TEnumErrors}"/> with the error.
        /// </summary>
        /// <exception cref="InvalidOperationException">Entity is not exist</exception>
        public async Task<OperationResult<TEnumErrors>> UpdateAsync(
            int id, 
            Action<TEntity> updateFields)
        {
            var entity = await GetByIdAsync(id);
            if (entity is null)
                throw new InvalidOperationException($"{typeof(TEntity)} with id = {id} was not found.");

            TEnumErrors? result = await validator.Validate(entity);
            if (result != null)
                return OperationResult<TEnumErrors>.Failure(result);

            updateFields(entity);

            await repository.SaveChangesAsync();
            return OperationResult<TEnumErrors>.Success();
        }

        public virtual async Task Delete(TEntity entity)
        {
            repository.Delete(entity);
            await repository.SaveChangesAsync();
        }
    }
}
