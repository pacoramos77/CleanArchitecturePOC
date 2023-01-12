using Core.ToDoListAggregate.Events;
using Core.Util;

using SharedKernel.DateTimeContext;
using SharedKernel.Domain;

namespace Core.Tests.SharedKernel;

public class DomainEventsSerializerTest
{
    private readonly DateTime _fakeNow = DateTime.Parse("2023-01-01T17:30:00Z").ToUniversalTime();
    private readonly DateTimeProviderContext _dateTimeProviderContext;

    public DomainEventsSerializerTest()
    {
        _dateTimeProviderContext = new DateTimeProviderContext(_fakeNow);
    }

    internal void Dispose()
    {
        _dateTimeProviderContext.Dispose();
    }

    [Fact]
    public void Should_Serialize_ADomainEvent()
    {
        var obj = new ToDoListCreatedEvent { Name = "to do" };

        var json = DomainEventsSerializer.Serialize(obj);

        json.Should().Be(
            $$"""
            {"__type":"Core.ToDoListAggregate.Events.ToDoListCreatedEvent","Name":"to do","DateOccurred":"2023-01-01T17:30:00Z"}
            """
        );
    }

    [Fact]
    public void Should_Serialize_ADomainEventList()
    {
        var list = new List<DomainEventBase>() { new ToDoListCreatedEvent { Name = "to do" } };

        var json = DomainEventsSerializer.SerializeList(list);

        json.Should().Be(
            $$"""
            [{"__type":"Core.ToDoListAggregate.Events.ToDoListCreatedEvent","Name":"to do","DateOccurred":"2023-01-01T17:30:00Z"}]
            """
        );
    }

    [Fact]
    public void Should_Deserialize_ADomainEvent()
    {
        const string json =
            $$"""
            {"__type":"Core.ToDoListAggregate.Events.ToDoListCreatedEvent","Name":"to do","DateOccurred":"2023-01-01T17:30:00Z"}
            """;

        var obj = DomainEventsSerializer.Deserialize<ToDoListCreatedEvent>(json)!;

        obj.Name.Should().Be("to do");
        obj.DateOccurred.Should().Be(_fakeNow);
    }

    [Fact]
    public void Should_Deserialize_ADomainEventList()
    {
        const string json =
            $$"""
            [{"__type":"Core.ToDoListAggregate.Events.ToDoListCreatedEvent","Name":"to do","DateOccurred":"2023-01-01T17:30:00Z"}]
            """;

        var list = DomainEventsSerializer.DeserializeList(json)!;
        list.Should().NotBeNull();

        var obj = (list.First() as ToDoListCreatedEvent)!;
        obj.Should().NotBeNull();
        obj.Name.Should().Be("to do");
        obj.DateOccurred.Should().Be(_fakeNow);
    }
}
