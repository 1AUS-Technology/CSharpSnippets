namespace PerformanceEngineering.IntruderDetection;

public enum IntruderRisk
{
    None,
    Low,
    Medium,
    High,
    Extreme
}

public class Room(string name)
{
    private AverageMeasurement average = new();
    private DebounceMeasurement debounce = new();

    public ref readonly AverageMeasurement Average => ref average;
    public ref readonly DebounceMeasurement Debounce => ref debounce;
    public string Name { get; } = name;

    private IntruderRisk risk;
    public ref readonly IntruderRisk RiskStatus => ref risk;

    public int Intruders { get; set; }

    public void TakeMeasurements(Func<bool> MeasurementHandler)
    {
        SensorMeasurement measure = default;
        do
        {
            measure = SensorMeasurement.TakeMeasurement(Name, Intruders);
            average.AddMeasurement(measure);
            debounce.AddMeasurement(measure);
            var CO2Variance = (debounce.CO2 - average.CO2) > 10.0 / 4;
            var O2Variance = (average.O2 - debounce.O2) > 0.005 / 4.0;
            var TempVariance = (debounce.Temperature - average.Temperature) > 0.05 / 4.0;
            var HumidityVariance = (debounce.Humidity - average.Humidity) > 0.20 / 4;
            risk = IntruderRisk.None;
            if (CO2Variance) { risk++; }
            if (O2Variance) { risk++; }
            if (TempVariance) { risk++; }
            if (HumidityVariance) { risk++; }
        } while (MeasurementHandler());
    }

    public override string ToString()
    {
        return $"Calculated intruder risk: {RiskStatus switch
        {
            IntruderRisk.None => "None",
            IntruderRisk.Low => "Low",
            IntruderRisk.Medium => "Medium",
            IntruderRisk.High => "High",
            IntruderRisk.Extreme => "Extreme",
            _ => "Error!"
        }}, Current intruders: {Intruders.ToString()}";
    }
}