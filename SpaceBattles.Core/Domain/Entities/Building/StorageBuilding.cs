namespace SpaceBattles.Core.Domain.Entities.Building;

using SpaceBattles.Core.Domain.Enums;

public sealed class StorageBuilding : Building
{
    public StorageBuilding()
    {
        EnergyStatus = ElectricalEntityStatus.None;
    }

    public Resource Resource { get; init; }

    public long Storage(int level)
    {
        return 5_000 * Convert.ToInt64(Math.Floor(2.5d * Math.Pow(Math.E, 20d / 33d * level)));
    }

    public (double[] Data, string[] Labels) GetStorageChartData(int level)
    {
        int lowerBound = Math.Max(0, level - 4);

        double[] chartData = new double[8];
        string[] chartLabels = new string[8];

        for (int i = 0; i < chartData.Length; i++)
        {
            chartData[i] = Storage(lowerBound + i);
            chartLabels[i] = $"level {lowerBound + i}";
        }

        return (chartData, chartLabels);
    }
}