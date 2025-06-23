namespace Synapse.Shared.Types;

public enum DeviceType
{
    Pump,
    Injector,
    Valve,
    Gauge,
    Sensor
}

public static class DeviceTypeExtensions
{
    public static DeviceType Parse(string value)
    {
        return value.ToLowerInvariant() switch
        {
            "pump" => DeviceType.Pump,
            "injector" => DeviceType.Injector,
            "valve" => DeviceType.Valve,
            "gauge" => DeviceType.Gauge,
            "sensor" => DeviceType.Sensor,
            _ => throw new ArgumentException($"Unknown device type: {value}")
        };
    }
    
}
