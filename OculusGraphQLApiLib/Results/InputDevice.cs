using System;

namespace OculusGraphQLApiLib.Results;

public class InputDevice
{
    public string name { get; set; } = "";
    public string tag { get; set; } = "";
    public SupportedInputDevice tag_enum
    {
        get
        {
            if(String.IsNullOrEmpty(tag)) return SupportedInputDevice.UNKNOWN;
            return (SupportedInputDevice)Enum.Parse(typeof(SupportedInputDevice), tag);
        }
    }
}