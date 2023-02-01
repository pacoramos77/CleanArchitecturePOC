using MediatR;

namespace SharedKernel.Domain;
public interface IDomainEvent : INotification
{
    Guid Id { get; set; }
}
