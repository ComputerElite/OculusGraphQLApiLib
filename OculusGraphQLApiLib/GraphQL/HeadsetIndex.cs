using System;
using System.Collections.Generic;
using System.Linq;
using OculusGraphQLApiLib;
using OculusGraphQLApiLib.Results;

public class HeadsetIndex
{
    public static readonly List<HeadsetIndexEntry> entries = new List<HeadsetIndexEntry>
    {
        new()
        {
            headset = Headset.MONTEREY,
            binaryType = HeadsetBinaryType.AndroidBinary,
            group = HeadsetGroup.Quest
        },
        new()
        {
            headset = Headset.HOLLYWOOD,
            binaryType = HeadsetBinaryType.AndroidBinary,
            group = HeadsetGroup.Quest
        },
        new()
        {
            headset = Headset.EUREKA,
            binaryType = HeadsetBinaryType.AndroidBinary,
            group = HeadsetGroup.Quest
        },
        new()
        {
            headset = Headset.SEACLIFF,
            binaryType = HeadsetBinaryType.AndroidBinary,
            group = HeadsetGroup.Quest
        },
        new()
        {
            headset = Headset.PANTHER,
            binaryType = HeadsetBinaryType.AndroidBinary,
            group = HeadsetGroup.Quest
        },
        new()
        {
            headset = Headset.RIFT,
            binaryType = HeadsetBinaryType.PCBinary,
            group = HeadsetGroup.PCVR,
            info = "Link compatible"
        },
        new()
        {
            headset = Headset.LAGUNA,
            binaryType = HeadsetBinaryType.PCBinary,
            group = HeadsetGroup.PCVR,
            info = "Link compatible"
        },
        new()
        {
            headset = Headset.PACIFIC,
            binaryType = HeadsetBinaryType.AndroidBinary,
            group = HeadsetGroup.Go
        },
        new()
        {
            headset = Headset.GEARVR,
            binaryType = HeadsetBinaryType.AndroidBinary,
            group = HeadsetGroup.GearVR
        },
        new()
        {
            headset = Headset.FUTURE_DEVICES,
            binaryType = HeadsetBinaryType.AndroidBinary,
            group = HeadsetGroup.Quest
        }
    };

    public static HeadsetGroup GetHeadsetGroup(Headset appHeadset)
    {
        HeadsetIndexEntry entry = entries.FirstOrDefault(x => x.headset == appHeadset);
        if (entry == null) return HeadsetGroup.Unknown;
        return entry.group;
    }
    public static HeadsetBinaryType GetHeadsetBinaryType(Headset appHeadset)
    {
        HeadsetIndexEntry entry = entries.FirstOrDefault(x => x.headset == appHeadset);
        if (entry == null) return HeadsetBinaryType.Unknown;
        return entry.binaryType;
    }

    public static HeadsetGroup ParseGroup(string s)
    {
        HeadsetGroup group = HeadsetGroup.Unknown;
        if(!Enum.TryParse(s, true, out group)) return HeadsetGroup.Unknown;
        return group;
    }
}

public class HeadsetIndexEntry
{
    public Headset headset { get; set; } = new Headset();

    public string codename
    {
        get
        {
            return headset.ToString();
        }
    }

    public string displayName
    {
        get
        {
            return HeadsetTools.GetHeadsetDisplayName(headset);
        }
    }

    public HeadsetBinaryType binaryType { get; set; } = HeadsetBinaryType.PCBinary;
    public HeadsetGroup group { get; set; } = HeadsetGroup.PCVR;

    public string groupString
    {
        get
        {
            return group.ToString();
        }
    }

    public string info { get; set; } = "";
    
}

public enum HeadsetBinaryType
{
    Unknown = -1,
    AndroidBinary = 1,
    PCBinary = 2,
}

public enum HeadsetGroup
{
    Unknown = -1,
    Quest = 0,
    PCVR = 1,
    Go = 2,
    GearVR = 3,
}