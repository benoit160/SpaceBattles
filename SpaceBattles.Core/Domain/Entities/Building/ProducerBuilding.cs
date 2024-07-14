namespace SpaceBattles.Core.Domain.Entities.Building;

using SpaceBattles.Core.Domain.Enums;

public sealed class ProducerBuilding : Building
{
    public ProducerBuilding()
    {
        EnergyStatus = ElectricalEntityStatus.Consummer;
    }

    public Resource Resource { get; init; }

    public int ProductionFactor { get; init; }

    public int Production(int level)
    {
        return Convert.ToInt32(ProductionFactor * level * Math.Pow(1.1, level));
    }

    public (double[] Data, string[] Labels) GetProductionChartData(int level)
    {
        int lowerBound = Math.Max(0, level - 4);

        double[] chartData = new double[8];
        string[] chartLabels = new string[8];

        for (int i = 0; i < chartData.Length; i++)
        {
            chartData[i] = Production(lowerBound + i);
            chartLabels[i] = $"level {lowerBound + i}";
        }

        return (chartData, chartLabels);
    }
}