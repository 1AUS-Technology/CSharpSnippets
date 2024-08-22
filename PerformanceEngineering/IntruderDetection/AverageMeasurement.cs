namespace PerformanceEngineering.IntruderDetection;

public struct AverageMeasurement
{
    public AverageMeasurement()
    {
    }

    private double sumCO2;
    private double sumO2;
    private double sumTemperature;
    private double sumHumidity;
    private int totalMeasurements;

    public readonly double CO2 => sumCO2 / totalMeasurements;
    public readonly double O2 => sumO2 / totalMeasurements;
    public readonly double Temperature => sumTemperature / totalMeasurements;
    public readonly double Humidity => sumHumidity / totalMeasurements;

    public void AddMeasurement(in SensorMeasurement datum)
    {
        totalMeasurements++;
        sumCO2 += datum.CO2;
        sumO2 += datum.O2;
        sumTemperature += datum.Temperature;
        sumHumidity += datum.Humidity;
    }

    public readonly override string ToString()
    {
        return $"""
                Average measurements:
                    Temp:      {Temperature:F3}
                    Humidity:  {Humidity:P3}
                    Oxygen:    {O2:P3}
                    CO2 (ppm): {CO2:F3}
                """;
    }
}