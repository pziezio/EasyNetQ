using System.Collections.Concurrent;

namespace EasyNetQ.Benchmarks.MessageFactory;

/// <summary>
/// Creates a generic <see cref="IMessage{T}"/> and returns it casted as <see cref="IMessage"/>
/// so it can be used in scenarios where we only have a runtime <see cref="Type"/> available.
/// </summary>
public static class ActivatorMessageFactory
{
    private static readonly ConcurrentDictionary<Type, Type> GenericMessageTypesMap = new();

    public static IMessage CreateInstance(Type messageType, object? body, MessageProperties properties)
    {
        var genericType = GenericMessageTypesMap.GetOrAdd(messageType, t => typeof(Message<>).MakeGenericType(t));
        return (IMessage)Activator.CreateInstance(genericType, body, properties)!;
    }
}
