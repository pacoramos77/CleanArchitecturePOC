using MediatR;

namespace SharedKernel.Messaging;

public interface IQuery<out TResponse> : IRequest<TResponse> { }
