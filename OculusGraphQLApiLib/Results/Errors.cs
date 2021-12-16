using System.Collections.Generic;

namespace OculusGraphQLApiLib.Results
{
    public class Error
    {
        public string message { get; set; } = "";
        public string serverity { get; set; } = "";
        public List<string> path { get; set; } = new List<string>();
    }
}