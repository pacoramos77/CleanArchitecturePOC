using MediatR;

namespace SharedKernel.Messaging;

public interface ICommand : IRequest { }

public interface ICommand<out TResponse> : IRequest<TResponse> { }
