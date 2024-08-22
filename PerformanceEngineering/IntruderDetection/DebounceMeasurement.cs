namespace PerformanceEngineering.IntruderDetection;

public struct DebounceMeasurement
{
    public DebounceMeasurement()
    {
    }

    private const int debounceSize = 50;
    private readonly SensorMeasurement[] recentMeasurements = new SensorMeasurement[debounceSize];
    private int totalMeasurements;
    public double CO2 { get; private set; }
    public double O2 { get; private set; }
    public double Temperature { get; private set; }
    public double Humidity { get; private set; }

    public void AddMeasurement(in SensorMeasurement datum)
    {
        int index = totalMeasurements % debounceSize;
        recentMeasurements[index] = datum;
        totalMeasurements++;
        double sumCO2 = 0;
        double sumO2 = 0;
        double sumTemp = 0;
        double sumHumidity = 0;
        for (int i = 0; i < debounceSize; i++)
        {
            if (recentMeasurements[i].Room is not null)
            {
                sumCO2 += recentMeasurements[i].CO2;
                sumO2 += recentMeasurements[i].O2;
                sumTemp += recentMeasurements[i].Temperature;
                sumHumidity += recentMeasurements[i].Humidity;
            }
        }

        O2 = sumO2 / (totalMeasurements > debounceSize ? debounceSize : totalMeasurements);
        CO2 = sumCO2 / (totalMeasurements > debounceSize ? debounceSize : totalMeasurements);
        Temperature = sumTemp / (totalMeasurements > debounceSize ? debounceSize : totalMeasurements);
        Humidity = sumHumidity / (totalMeasurements > debounceSize ? debounceSize : totalMeasurements);
    }

    public override string ToString()
    {
        return $"""
                Debounced measurements:
                    Temp:      {Temperature:F3}
                    Humidity:  {Humidity:P3}
                    Oxygen:    {O2:P3}
                    CO2 (ppm): {CO2:F3}
                """;
    }
}