using System.Drawing;

namespace GoodCodeBadCode.CodeContracts;

public class UserSettings
{
    //Can create an instance
    private UserSettings()
    {
    }

    // have to call this method to create an instance
    public static UserSettings? Create(string location)
    {
        UserSettings settings = new UserSettings();

        if (!settings.LoadSettings(location))
        {
            return null;
        }

        settings.Init();
        return settings;
    }

    private void Init()
    {
        
    }

    private bool LoadSettings(string location)
    {
        return true;
    }

    // null if users have not chosen a color
    public Color? GetUiColor()
    {
        return Color.AliceBlue;
    }

}