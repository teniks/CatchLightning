using System;

namespace CatchLightning.Core.Services
{
    public class OperationResult<TEnumErrors> : OperationResult
        where TEnumErrors : Enum
    {
        public TEnumErrors? Error { get; set; }
        public override Enum? EnumError => Error;

        public static OperationResult<TEnumErrors> Success(string? message = null) =>
            new OperationResult<TEnumErrors> { IsSuccess = true, Message = message };

        public static OperationResult<TEnumErrors> Failure(TEnumErrors error, string? message = null) =>
            new OperationResult<TEnumErrors> { IsSuccess = false, Error = error, Message = message };
    }

    public abstract class OperationResult
    {
        public bool IsSuccess { get; protected set; }
        public string? Message { get; protected set; }
        public virtual Enum? EnumError { get; protected init; }

        public static OperationResult Success(string? message = null) =>
            new OperationResult<DummyEnum> { IsSuccess = true, Message = message };

        public static OperationResult Failure(Enum error, string? message = null) =>
            new OperationResult<DummyEnum> { IsSuccess = false, Message = message };

        private enum DummyEnum { } 
    }
}
