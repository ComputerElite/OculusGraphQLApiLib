using System;
using System.Collections.Generic;
using ComputerUtils.VarUtils;

namespace OculusGraphQLApiLib.Results;

public class ApplicationRevision
{
    public string id { get; set; } = "";
    // Unknown type
    //public string release_type { get; set; } = null;
    public ApplicationRevisionApplication application { get; set; } = new ApplicationRevisionApplication();
    public OculusBinary binary { get; set; } = null;
    public OculusBinary binary_with_fallback { get; set; } = null;
    public long? created_date { get; set; } = 0;

    public DateTime created_datetime
    {
        get
        {
            if(created_date == null) return DateTime.MinValue;
            return TimeConverter.UnixTimeStampToDateTime(created_date);
        }
    }
    public bool is_initial_release { get; set; } = false;
    public string name_with_version_number { get; set; } = "";
    public long? release_date { get; set; } = 0;
    public DateTime release_datetime
    {
        get
        {
            if(release_date == null) return DateTime.MinValue;
            return TimeConverter.UnixTimeStampToDateTime(release_date);
        }
    }
    public bool is_first { get; set; } = false;
    public string status { get; set; } = "";
    public ApplicationRevisionStatus status_enum
    {
        get
        {
            if(String.IsNullOrEmpty(status)) return ApplicationRevisionStatus.UNKNOWN;
            return (ApplicationRevisionStatus)Enum.Parse(typeof(ApplicationRevisionStatus), status);
        }
    }
    public string release_type { get; set; } = "";
    public ReleaseType release_type_enum
    {
        get
        {
            if (String.IsNullOrEmpty(release_type)) return ReleaseType.UNKNOWN;
            return (ReleaseType)Enum.Parse(typeof(ReleaseType), release_type);
        }
    }
    public PDPMetadata pdp_metadata { get; set; } = new PDPMetadata();
    public List<string> review_messages { get; set; } = new List<string>();
}