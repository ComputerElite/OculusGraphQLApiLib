using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OculusGraphQLApiLib
{
    public enum Headset
    {
        RIFT = 0,
        MONTEREY = 1, //aka quest
        GEARVR = 2
    }

    public class HeadsetTools
    {
        public static string GetHeadsetDisplayName(Headset headset)
        {
            switch (headset)
            {
                case Headset.RIFT:
                    return "Rift";
                case Headset.MONTEREY:
                    return "Quest";
                case Headset.GEARVR:
                    return "GearVR";
                default:
                    return "unknown";
            }
        }

        public static string GetHeadsetInstallActionName(Headset headset)
        {
            switch (headset)
            {
                case Headset.RIFT:
                    return "Launch";
                    break;
                case Headset.MONTEREY:
                    return "Install";
                    break;
                case Headset.GEARVR:
                    return "Install";
                    break;
            }
            return "unknown";
        }
    }
}
