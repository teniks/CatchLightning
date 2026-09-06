using CatchLightning.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatchLightning.Core.Services.Validators
{
    internal class CompositeValidator<TEntity, TEnumErrors> : IValidator<TEntity, TEnumErrors>
        where TEntity : class, IHasName
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
                return await validator.Validate(entity);
            }

            return default(TEnumErrors?);
        }
    }
}
