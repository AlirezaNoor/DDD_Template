using DDD.Domain.Common;
using MediatR;

namespace DDD.Application.Common;

public interface IQuery<TResponse> : IRequest<Result<TResponse>> { }