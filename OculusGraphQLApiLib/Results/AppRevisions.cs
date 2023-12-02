using System.Collections.Generic;

namespace OculusGraphQLApiLib.Results
{

    public class AppRevisionsNode : GraphQLBase
    {
        public string id { get; set; } = "";
        public Nodes<OculusBinary> primary_binaries { get; set; } = new Nodes<OculusBinary>();
    }
}