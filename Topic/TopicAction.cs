namespace Synapse.Shared.Topic;

public enum TopicAction
{
    Status,
    Update
}

public static class TopicActionExtensions
{
    public static string ToTopic(this TopicAction action)
    {
        return action switch
        {
            TopicAction.Status => "status",
            TopicAction.Update => "update",
            _ => throw new ArgumentOutOfRangeException(nameof(action), action, null)
        };
    }
    
    private static readonly Dictionary<string, TopicAction> TopicMap =
        Enum.GetValues<TopicAction>()
            .ToDictionary(
                e => e.ToString(),        
                e => e,
                StringComparer.OrdinalIgnoreCase 
            );
    
    public static TopicAction FromTopic(this TopicAction action, string value)
    {
        return TopicMap.TryGetValue(value, out var result)
            ? result
            : throw new ArgumentException($"Unknown topic action: {action}", nameof(action));
    }
}