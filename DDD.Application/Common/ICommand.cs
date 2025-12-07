using DDD.Domain.Common;
using MediatR;

namespace DDD.Application.Common;

public interface ICommand : IRequest<Result> { }

public interface ICommand<TResponse> : IRequest<Result<TResponse>> { }