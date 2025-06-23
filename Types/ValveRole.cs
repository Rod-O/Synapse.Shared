namespace Synapse.Shared.Types;

public enum ValveRole
{
    // In WTS
    RoToZone = 0,
    RoToCollector = 1,
    CollectorToZone = 2,
    
    
    // In Subzone
    Distribution = 10,
    Reflow = 11
    
}

public static class ValveRoleExtensions 
{
    public static bool IsInZone(this ValveRole role)
    {
        switch (role)
        {
            case ValveRole.Distribution:
            case ValveRole.Reflow:
                return true;        
            default:
                return false;
        };
    }
}