namespace SpaceBattles.Core.Domain.Interfaces;

public interface IPosition
{
    public long DistanceTo(IPosition other) => 1000;
}