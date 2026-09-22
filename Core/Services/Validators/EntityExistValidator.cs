using CatchLightning.Core.Abstractions;
using System.Threading.Tasks;

namespace CatchLightning.Core.Services.Validators
{
    public class EntityExistValidator<TEntity, TEntityErrors>
        : IValidator<TEntity, TEntityErrors>
        where TEntity : IEntity
    {
        private IGetterById<TEntity> _getterById;
        private TEntityErrors _errorDuplicate;


        public EntityExistValidator(IGetterById<TEntity> getterById, TEntityErrors errorDuplicate)
        {
            _getterById = getterById;
            _errorDuplicate = errorDuplicate;
        }

        public async Task<TEntityErrors?> Validate(TEntity entity)
        {
            var result = await _getterById.GetByIdAsync(entity.Id);

            if (result is null)
                return _errorDuplicate;
            return default(TEntityErrors?);
        }
    }
}
