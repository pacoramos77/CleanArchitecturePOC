using Core.ToDoListAggregate.Events;
using Core.Util;

using SharedKernel.Domain;

namespace Core.Tests.SharedKernel;

public class DomainEventsSerializerTest : TestBaseFixture
{
    public DomainEventsSerializerTest() : base(DateTime.Parse("2023-01-01T17:30:00Z").ToUniversalTime()) { }

    [Fact]
    public void Should_Serialize_DomainEvent()
    {
        var obj = new ToDoListCreatedEvent { Name = "to do" };

        var json = DomainEventsSerializer.Serialize(obj);

        const string jsonExpected = $$"""
            {"__type":"ToDoListCreatedEvent","Name":"to do","DateOccurred":"2023-01-01T17:30:00Z"}
            """;
        json.Should().Be(jsonExpected);
    }

    [Fact]
    public void Should_Serialize_DomainEventList()
    {
        var list = new List<DomainEventBase>() { new ToDoListCreatedEvent { Name = "to do" } };

        var json = DomainEventsSerializer.SerializeList(list);

        const string jsonExpected = $$"""
            [{"__type":"ToDoListCreatedEvent","Name":"to do","DateOccurred":"2023-01-01T17:30:00Z"}]
            """;
        json.Should().Be(jsonExpected);
    }

    [Fact]
    public void Should_Deserialize_DomainEvent()
    {
        const string json = $$"""
            {"__type":"ToDoListCreatedEvent","Name":"to do","DateOccurred":"2023-01-01T17:30:00Z"}
            """;

        var obj = DomainEventsSerializer.Deserialize<ToDoListCreatedEvent>(json)!;

        obj.Name.Should().Be("to do");
        obj.DateOccurred.Should().Be(DateTimeMock);
    }

    [Fact]
    public void Should_Deserialize_DomainEventList()
    {
        const string json = $$"""
            [{"__type":"ToDoListCreatedEvent","Name":"to do","DateOccurred":"2023-01-01T17:30:00Z"}]
            """;

        var list = DomainEventsSerializer.DeserializeList(json)!;
        list.Should().NotBeNull();

        var obj = (list.First() as ToDoListCreatedEvent)!;
        obj.Should().NotBeNull();
        obj.Name.Should().Be("to do");
        obj.DateOccurred.Should().Be(DateTimeMock);
    }
}
