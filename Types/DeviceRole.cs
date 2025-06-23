using System.Collections.Concurrent;

namespace Synapse.Shared.Types;

public static class DeviceRole
{
    private static readonly ConcurrentDictionary<Type, object> RoleMaps = new();

    private static Dictionary<string, TRole> GetRoleMap<TRole>() where TRole : struct, Enum
    {
        return (Dictionary<string, TRole>)RoleMaps.GetOrAdd(typeof(TRole), _ =>
            Enum.GetValues<TRole>()
                .ToDictionary(role => role.ToString(), 
                    role => role, 
                    StringComparer.OrdinalIgnoreCase));
    }

    public static TRole ParseRole<TRole>(string value) where TRole : struct, Enum
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Value cannot be null, empty, or whitespace", nameof(value));

        var roleMap = GetRoleMap<TRole>();
        if (roleMap.TryGetValue(value.Trim(), out var role))
        {
            return role;
        }

        throw new ArgumentException($"Unknown {typeof(TRole).Name}: '{value}'", nameof(value));
    }

    public static bool TryParseRole<TRole>(string value, out TRole role) where TRole : struct, Enum
    {
        role = default;
        if (string.IsNullOrWhiteSpace(value))
            return false;

        var roleMap = GetRoleMap<TRole>();
        return roleMap.TryGetValue(value.Trim(), out role);
    }

    public static int GetDeviceRole(DeviceType deviceType, string value)
    {
        int result;
        switch (deviceType)
        {
            case DeviceType.Pump:
                result = (int)ParsePumpRole(value);
                break;
            case DeviceType.Gauge:
                result = (int)ParseGaugeRole(value);
                break;
            case DeviceType.Valve:
                result = (int)ParseValveRole(value);
                break;
            case DeviceType.Injector:
                result = (int)ParseInjectorRole(value);
                break;
            default:
                throw new ArgumentException($"Unknown device type: {deviceType}");
        }
        return result;
    }
    
    public static PumpRole ParsePumpRole(string value) => ParseRole<PumpRole>(value);
    public static GaugeRole ParseGaugeRole(string value) => ParseRole<GaugeRole>(value);
    public static ValveRole ParseValveRole(string value) => ParseRole<ValveRole>(value);
    public static InjectorRole ParseInjectorRole(string value) => ParseRole<InjectorRole>(value);
}