namespace Synapse.Shared.Types;

public enum WtsValveCollection
{
    RoValves = 0,
    CollectorValves = 1,
    RoToCollectorValve = 2
}

public static class WtsValveCollectionExtensions
{
    public static bool IsArrayCollection(this WtsValveCollection collection)
    {
        return collection == WtsValveCollection.RoValves ||
               collection == WtsValveCollection.CollectorValves;
    }
}