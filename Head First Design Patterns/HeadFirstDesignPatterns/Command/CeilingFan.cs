namespace HeadFirstDesignPatterns.Command;

// Generate a ceiling fan class with three speeds: high, medium, and low.
// The ceiling fan should also have an "off" speed.
// The ceiling fan should be initialized to "off".
// The ceiling fan should have a method to get the current speed.
// The ceiling fan should have a method to set the speed.

public class CeilingFan
{
    public CeilingFan(string location)
    {
        Location = location;
        Speed = FanSpeed.Off;
    }

    public void High()
    {
        Speed = FanSpeed.High;
    }

    public void Medium()
    {
        Speed = FanSpeed.Medium;
    }

    public void Low()
    {
        Speed = FanSpeed.Low;
    }

    public void Off()
    {
        Speed = FanSpeed.Off;
    }

    public FanSpeed Speed { get; private set; }
    public string Location { get; }
}

public enum FanSpeed
{
    High,
    Medium,
    Low,
    Off

}