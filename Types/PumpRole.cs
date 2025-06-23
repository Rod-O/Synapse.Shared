namespace Synapse.Shared.Types;

public enum PumpRole 
{
    Ro = 0,
    IntakeA,
    IntakeB,
    Backwash,
    Collector,
    Waste,
    Distribution,
    Drain,
    Supply,
    VenturiA,
    VenturiB,
    Hydronics,
    Hydronics2,
    Flow,
    Return
}

public static class PumpKindExtensions 
{
    public static bool IsInZone(this PumpRole role)
    {
        switch (role)
        {
            case PumpRole.Ro:
            case PumpRole.IntakeA:
            case PumpRole.IntakeB:
            case PumpRole.Backwash:
            case PumpRole.Collector:
            case PumpRole.Waste:
                return false;

            default:
                return true;
        };
    }
    public static bool IsInLss(this PumpRole role)
    {
        switch (role)
        {
            case PumpRole.Flow:
            case PumpRole.Return:
                return false;
            default:
                return false;
        };
    }
}