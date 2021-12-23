namespace OculusGraphQLApiLib.Results
{
    public class OculusUserWrapper
    {
        public OculusUser user { get; set; } = new OculusUser();
    }
    public class OculusUser
    {
        public string alias { get; set; } = "";
        public OculusUri profile_photo { get;set; } = new OculusUri();
        public string id { get; set; } = "";
    }
}