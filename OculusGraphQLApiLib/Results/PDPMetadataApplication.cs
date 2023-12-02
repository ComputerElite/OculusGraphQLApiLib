using System;

namespace OculusGraphQLApiLib.Results;

public class PDPMetadataApplication
{
    public string default_locale { get; set; } = "";
    public string platform { get; set; } = "";

    public OculusPlatform platform_enum
    {
        get
        {
            if(String.IsNullOrEmpty(platform)) return OculusPlatform.UNKNOWN;
            return (OculusPlatform)Enum.Parse(typeof(OculusPlatform), platform, true);
        }
    }
    public string id { get; set; } = "";
    public bool is_first_party { get; set; } = false;
    public bool enables_in_app_ads { get; set; } = false;
    public bool is_concept { get; set; } = false;
}