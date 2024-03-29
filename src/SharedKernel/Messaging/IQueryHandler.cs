using MediatR;

namespace SharedKernel.Messaging;

public interface IQueryHandler<in TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IQuery<TResponse> { }
