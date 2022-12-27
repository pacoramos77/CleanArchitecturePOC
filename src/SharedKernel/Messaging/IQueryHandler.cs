using MediatR;

namespace SharedKernel.Messaging;

public interface IQueryHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IQuery<TResponse> { }
