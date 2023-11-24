﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OculusGraphQLApiLib
{
    public enum Headset
    {
        INVALID = -1,
        RIFT = 0,
        MONTEREY = 1, // aka quest 1
        HOLLYWOOD = 2, // aka quest 2
        GEARVR = 3,
        PACIFIC = 4, // aka Go
        LAGUNA = 5, // aka Rift S
        SEACLIFF = 6, // aka Quest Pro
        EUREKA = 7, // aka Quest 3 ?
        PANTHER = 8 // Unknown
    }

    public class HeadsetTools
    {
		public static Headset GetHeadsetFromOculusLink(string link, Headset fallback)
        {
			if (link.Split('/').Length < 5) return fallback;
			switch (link.Split('/')[4])
            {
                case "quest":
                    return Headset.HOLLYWOOD;
                case "rift":
                    return Headset.RIFT;
                case "go":
                    return Headset.PACIFIC;
                case "gearvr":
                    return Headset.GEARVR;
            }
            return fallback;

		}

        public static Headset GetHeadsetFromCodeName(string codename)
        {
            Headset headset = Headset.INVALID;
            if (!Enum.TryParse(codename, true, out headset)) return Headset.INVALID;
            return headset;
        }

        public static string GetHeadsetCodeName(Headset headset)
        {
            return Enum.GetName(typeof(Headset), headset);
        }
        public static string GetHeadsetDisplayName(Headset headset)
        {
            switch (headset)
            {
                case Headset.RIFT:
                    return "Rift";
                case Headset.LAGUNA:
                    return "Rift S";
                case Headset.MONTEREY:
                    return "Quest 1";
                case Headset.HOLLYWOOD:
                    return "Quest 2";
                case Headset.EUREKA:
                    return "Quest 3";
                case Headset.GEARVR:
                    return "GearVR";
                case Headset.PACIFIC:
                    return "Go";
                case Headset.SEACLIFF:
                    return "Quest Pro";
                case Headset.PANTHER:
                    return "Unknown headset (PANTHER)";
                default:
                    return "Unknown " + headset;
            }
        }

        public static string GetHeadsetDisplayNameGeneral(Headset headset)
        {
            switch (headset)
            {
                case Headset.RIFT:
                    return "Rift";
                case Headset.LAGUNA:
                    return "Rift";
                case Headset.MONTEREY:
                    return "Quest";
                case Headset.HOLLYWOOD:
                    return "Quest";
                case Headset.EUREKA:
                    return "Quest";
                case Headset.GEARVR:
                    return "GearVR";
                case Headset.PACIFIC:
                    return "Go";
                case Headset.SEACLIFF:
                    return "Quest";
                case Headset.PANTHER:
                    return "Quest (maybe)";
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
                case Headset.LAGUNA:
                    return "Launch";
                case Headset.MONTEREY:
                    return "Install";
                case Headset.HOLLYWOOD:
                    return "Install";
                case Headset.EUREKA:
                    return "Install";
                case Headset.GEARVR:
                    return "Install";
                case Headset.PACIFIC:
                    return "Install";
                case Headset.SEACLIFF:
                    return "Install";
                case Headset.PANTHER:
                    return "Install";
            }
            return "unknown";
        }
    }
}
