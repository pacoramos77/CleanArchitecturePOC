using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

using Core.ToDoListAggregate.Events;

using SharedKernel.Domain;

namespace Core.Util;

public static class DomainEventsSerializer
{
    private const string TypeDiscriminatorPropertyName = "__type";

    public static JsonSerializerOptions Options
    {
        get
        {
            return new()
            {
                TypeInfoResolver = new DefaultJsonTypeInfoResolver
                {
                    Modifiers =
                    {
                        static typeInfo =>
                        {
                            if (typeInfo.Type == typeof(DomainEventBase))
                            {
                                typeInfo.PolymorphismOptions = new()
                                {
                                    TypeDiscriminatorPropertyName = TypeDiscriminatorPropertyName,
                                    DerivedTypes =
                                    {
                                        new JsonDerivedType(
                                            typeof(ToDoListCreatedEvent),
                                            typeof(ToDoListCreatedEvent).Name!
                                        ),

                                        // new JsonDerivedType(typeof(OtherEvent), typeof(OtherEvent).Name!),
                                    },
                                };
                            }
                        },
                    },
                },
            };
        }
    }

    public static string Serialize(DomainEventBase e) => SerializeWithTypeResolver(e);

    public static string SerializeList(IEnumerable<DomainEventBase> e) =>
        SerializeWithTypeResolver(e);

    public static DomainEventBase? Deserialize(string json) =>
        DeserializeWithTypeResolver<DomainEventBase>(json);

    public static TDerivedType? Deserialize<TDerivedType>(string json)
        where TDerivedType : DomainEventBase =>
        DeserializeWithTypeResolver<DomainEventBase>(json) as TDerivedType;

    public static IEnumerable<DomainEventBase>? DeserializeList(string json) =>
        DeserializeWithTypeResolver<IEnumerable<DomainEventBase>>(json);

    private static string SerializeWithTypeResolver<TValue>(TValue obj) =>
        JsonSerializer.Serialize(obj, Options);

    private static TValue? DeserializeWithTypeResolver<TValue>(string json) =>
        JsonSerializer.Deserialize<TValue>(json, Options);
}
