using CatchLightning.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatchLightning.Core.Services.Validators
{
    public class CompositeValidator<TEntity, TEnumErrors> : IValidator<TEntity, TEnumErrors>
        where TEntity : class, IEntity
        where TEnumErrors : Enum
    {
        private readonly IEnumerable<IValidator<TEntity, TEnumErrors>> _validators;


        public CompositeValidator(IEnumerable<IValidator<TEntity, TEnumErrors>> validators)
        {
            _validators = validators;
        }

        public async Task<TEnumErrors?> Validate(TEntity entity)
        {
            foreach (var validator in _validators)
            {
                var result = await validator.Validate(entity);

                if (result is null)
                    continue;
                else
                    return result;
            }

            return default(TEnumErrors?);
        }
    }
}
