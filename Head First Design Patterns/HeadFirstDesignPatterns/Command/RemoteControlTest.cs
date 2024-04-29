using HeadFirstDesignPatterns.Command.SimpleCommandViaActions;

namespace HeadFirstDesignPatterns.Command;

public class RemoteControlTest
{
    public static void Run()
    {
        var remote = new SimpleRemoteControl();
        var light = new CeilingLight();
        var lightOn = new LightOnCommand(light);
        remote.SetFirstButtonCommand(lightOn);
        remote.FirstButtonPressed();

        //Using Action
        var remoteViaAction = new SimpleRemoteCommandViaActions();
        var lightViaAction = new CeilingLight();
        remoteViaAction.SetFirstButtonCommand(lightViaAction.On);

        remoteViaAction.FirstButtonPressed();
    }
}