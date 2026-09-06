using CatchLightning.Core.Infrastructure;
using CatchLightning.Core.Models;
using System;
using System.Threading.Tasks;

namespace CatchLightning.Core.Services.Validators
{
    internal class UniqueNameValidator<TEntity, TEnumErrors> : IValidator<TEntity, TEnumErrors>
        where TEntity : class, IHasName
        where TEnumErrors : Enum
    {
        private IFindByName<TEntity> _finder;
        private TEnumErrors _errorDuplicate;

        /// <summary>
        /// <see cref="IFindByName{TEntity}"/> to check if the name is unique and return <see cref="TEnumErrors?"/>, 
        /// and <b>next</b> an optional next validator in the chain.
        /// </summary>
        /// <param name="finder"></param>
        /// <param name="errorDuplicate"></param>
        /// <param name="next"></param>
        public UniqueNameValidator(IFindByName<TEntity> finder, TEnumErrors errorDuplicate)
        {
            _finder = finder;
            _errorDuplicate = errorDuplicate;
        }

        /// <summary>
        /// If the field is not unique it returns an error message.<br/> 
        /// Else it returns default value of <see cref="TEnumErrors?"/>
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public async Task<TEnumErrors?> Validate(TEntity entity)
        {
            if (await _finder.GetByNameAsync(entity.Name) != null)
                return _errorDuplicate;

            return default(TEnumErrors?);
        }
    }
}
