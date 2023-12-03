using System;

namespace OculusGraphQLApiLib.Results;

public class ApplicationGrouping
{
    public bool viewer_has_update_access { get; set; } = false;
    public string id { get; set; } = "";
    public Nodes<OculusId> full_applications { get; set; } = new Nodes<OculusId>();
    public string report_method { get; set; } = "";
    public ReportMethod report_method_enum
    {
        get
        {
            if(report_method == "") return ReportMethod.UNKNOWN;
            return (ReportMethod)Enum.Parse(typeof(ReportMethod), report_method);
        }
    }
    public Nodes<AchievementDefinition> achievement_definitions { get; set; } = new Nodes<AchievementDefinition>();
    
    public Edges<Node<IAPItem>> add_ons { get; set; } = new Edges<Node<IAPItem>>();
}

public class ApplicationGroupingNodes : ApplicationGrouping
{
    public Nodes<IAPItem> add_ons { get; set; } = new Nodes<IAPItem>();
}