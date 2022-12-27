using MediatR;

namespace Application.Abstractions.Messaging;

public interface IQueryHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IQuery<TResponse> { }
