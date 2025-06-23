using Synapse.Shared.Types;

namespace Synapse.Shared.Topic;

public partial class TopicInfo
{
    public int MacrozoneId { get; set; }
    public DeviceType DeviceType { get; set; }
    public int DeviceRole { get; set; }     
    public int? ZoneId { get; set; } = null;
    
    public int? SubzoneId { get; set; } = null;
    public int? DeviceId { get; set; } = null;
    
    public TopicAction Action { get; set; } 

}



public partial class TopicInfo
{
    /// <summary>
    /// Parses a topic string into a TopicInfo object.
    /// Expected format:
    /// macrozone/{id}/
    /// [zone/{zoneid}/][subzone/{subzoneid}/][id/{deviceid}/]
    /// {deviceType}/{deviceRole}/
    /// update|status/
    /// </summary>
    /// <param name="topic"></param>
    /// <returns></returns>
    public static TopicInfo? ParseTopic(string topic)
    {
        var parts = topic.Split('/', StringSplitOptions.RemoveEmptyEntries);
        
        
        if (parts.Length < 4 || parts[0] != "macrozone")
            return null;

        var topicInfo = new TopicInfo
        {
            MacrozoneId = int.Parse(parts[1]),
        };

        int currentIndex = 2;

        // Check if zone is specified
        if (currentIndex < parts.Length && parts[currentIndex] == "zone")
        {
            topicInfo.ZoneId = int.Parse(parts[currentIndex + 1]);
            currentIndex += 2;
        }

        // Check if zone is specified
        if (currentIndex < parts.Length && parts[currentIndex] == "subzone")
        {
            topicInfo.SubzoneId = int.Parse(parts[currentIndex + 1]);
            currentIndex += 2;
        }

        // Check if device ID is specified
        if (currentIndex < parts.Length && parts[currentIndex] == "id")
        {
            topicInfo.DeviceId = int.Parse(parts[currentIndex + 1]);
            currentIndex += 2;
        }

        if (currentIndex < parts.Length)
        {
            topicInfo.DeviceType = Enum.Parse<DeviceType>(parts[currentIndex++], ignoreCase: true);
            topicInfo.DeviceRole =
                Synapse.Shared.Types.DeviceRole.GetDeviceRole(topicInfo.DeviceType, parts[currentIndex++]);
        }

        // The last part should be the action (e.g., update, status)
        if (currentIndex < parts.Length)
        {
            if (Enum.TryParse<TopicAction>(parts[currentIndex].TrimEnd('/'), ignoreCase: true, out var result))
                topicInfo.Action = result;
        }
        
        return topicInfo;
    }
    
    public static string BuildTopic(TopicInfo topicInfo)
    {
        var topic = $"macrozone/{topicInfo.MacrozoneId}/";
        
        if (topicInfo.ZoneId.HasValue)
            topic += $"zone/{topicInfo.ZoneId}/";

        if (topicInfo.SubzoneId.HasValue)
            topic += $"zone/{topicInfo.SubzoneId}/";
            
        if (topicInfo.DeviceId.HasValue)
            topic += $"id/{topicInfo.DeviceId}/";
        
        topic += $"{topicInfo.DeviceType}/{topicInfo.DeviceRole}/";
        topic += topicInfo.Action.ToString().ToLowerInvariant() + "/";
        
        return topic;
    }
    
}
