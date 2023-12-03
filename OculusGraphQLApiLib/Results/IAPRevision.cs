using System;

namespace OculusGraphQLApiLib.Results;

public class IAPRevision
{
    public string id { get; set; } = "";
    
    public string status { get; set; } = "";
    public BinaryStatus status_enum
    {
        get
        {
            if(String.IsNullOrEmpty(status)) return BinaryStatus.UNKNOWN;
            return (BinaryStatus)Enum.Parse(typeof(BinaryStatus), status);
        }
    }
    public string release_status { get; set; } = "";
    public ReleaseStatus release_status_enum
    {
        get
        {
            if(String.IsNullOrEmpty(release_status)) return ReleaseStatus.UNKNOWN;
            return (ReleaseStatus)Enum.Parse(typeof(ReleaseStatus), release_status);
        }
    }
    public string release_type { get; set; } = "";
    public ReleaseType release_type_enum
    {
        get
        {
            if(String.IsNullOrEmpty(release_type)) return ReleaseType.UNKNOWN;
            return (ReleaseType)Enum.Parse(typeof(ReleaseType), release_type);
        }
    }
}