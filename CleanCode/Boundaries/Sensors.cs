using System.Collections.Concurrent;

namespace CleanCode.Boundaries;

//Wrap around the IDictionary interface
public class Sensors
{
    private IDictionary<string, Sensor> _sensors = new ConcurrentDictionary<string, Sensor>();
    public Sensor GetById(string id)
    {
        return _sensors[id];
    }
}