using System.Collections.Generic;
using Newtonsoft.Json;

public static class SettingsHelper
{
    public static bool ReadSettings(KMModSettings settings)
    {
        settings.RefreshSettings();
        Dictionary<string, bool> set = JsonConvert.DeserializeObject<Dictionary<string, bool>>(settings.Settings);
        bool holiday = System.DateTime.Now.Day == 1 && System.DateTime.Now.Month == 4;
        return set["AprilFools"] || holiday;
    }

    private static readonly List<Dictionary<string, object>> TweaksEditorSettings = new List<Dictionary<string, object>>()
    {
        new Dictionary<string, object>()
        {
            {
                "Filename",
                "501And42-settings.txt"
            },
            {
                "Name",
                "501 And 42"
            },
            {
                "Listings",
                new List<Dictionary<string, object>>()
                {
                    new Dictionary<string, object>()
                    {
                        {
                            "Text",
                            "April Fool's Mode"
                        },
                        {
                            "Key",
                            "AprilFools"
                        },
                        {
                            "Description",
                            "Should the module use April Fool's mode?"
                        },
                        {
                            "Type",
                            "Checkbox"
                        }
                    }
                }
            }
        }
    };
}