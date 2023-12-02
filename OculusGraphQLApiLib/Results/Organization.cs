namespace OculusGraphQLApiLib.Results
{
    public class Organization
    {
        public string id { get; set; } = "";
        public bool is_authorized_for_quest { get; set; } = false;
        public Nodes<ApplicationGrouping> application_groupings { get; set; } = new Nodes<ApplicationGrouping>();
    }
}