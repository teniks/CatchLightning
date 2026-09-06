using System;

namespace CatchLightning.Core.Services
{
    internal class OperationResult<TEnumErrors> where TEnumErrors : Enum
    {
        public bool IsSuccess { get; private set; }
        public TEnumErrors? Error { get; private set; }

        public static OperationResult<TEnumErrors> Success() =>
            new OperationResult<TEnumErrors> { IsSuccess = true };

        public static OperationResult<TEnumErrors> Failure(TEnumErrors error) =>
            new OperationResult<TEnumErrors> { IsSuccess = false, Error = error };
    }
}
