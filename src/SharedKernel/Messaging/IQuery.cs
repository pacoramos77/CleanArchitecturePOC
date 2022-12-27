using MediatR;

namespace SharedKernel.Messaging;

public interface IQuery<TResponse> : IRequest<TResponse> { }
