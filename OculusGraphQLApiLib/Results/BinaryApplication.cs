using System;

namespace OculusGraphQLApiLib.Results;

public class BinaryApplication : GraphQLBase
{
    public string id { get; set; } = "";
    public bool is_concept { get; set; } = false;
    public string release_status { get; set; } = "";
    public ReleaseStatus release_status_enum
    {
        get
        {
            if(String.IsNullOrEmpty(release_status)) return ReleaseStatus.UNKNOWN;
            return (ReleaseStatus)Enum.Parse(typeof(ReleaseStatus), release_status);
        }
    }
    public bool is_blocked_by_verification { get; set; } = false;
}