using MediatR;

namespace SharedKernel.Domain;

public interface IDomainEventHandler<in TNotification> : INotificationHandler<TNotification>
    where TNotification : INotification { }
