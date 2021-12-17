using System.Collections.Generic;

namespace OculusGraphQLApiLib.Results
{
    public class Application
    {
        public string id { get; set; } = "";
        public string platform { get; set; } = "";
        public string display_name { get; set; } = "";
        public string displayName { get { return display_name; } set { display_name = value; } }
        public Nodes<ReleaseChannel> release_channels { get; set; } = new Nodes<ReleaseChannel>();
        public Nodes<Revision> revisions { get; set; } = new Nodes<Revision>();
        public Organization organization { get; set; } = new Organization();
        public bool is_concept { get; set; } = false; // aka AppLab
        public bool is_approved { get; set; } = false;
        public bool is_enterprise_enabled { get; set; } = false;
        public string canonicalName { get; set; } = "";
        public long release_date { get; set; } = 0;
        public bool viewer_has_preorder { get; set; } = false;
        public OculusUri cover_landscape_image { get; set; } = new OculusUri();
        public OculusUri cover_portrait_image { get; set; } = new OculusUri();
        public OculusUri cover_square_image { get; set; } = new OculusUri();
        public List<OculusUri> screenshots { get; set; } = new List<OculusUri>();
        public Edges<Node<AndroidBinary>> supportedBinaries { get; set; } = new Edges<Node<AndroidBinary>>();
    }
    public class EdgesPrimaryBinaryApplication : Application
    {
        public Edges<Node<AndroidBinary>> primary_binaries { get; set; } = new Edges<Node<AndroidBinary>>();
    }

    public class NodesPrimaryBinaryApplication : Application
    {
        public Nodes<AndroidBinary> primary_binaries { get; set; } = new Nodes<AndroidBinary>();
    }

    public class OculusUri
    {
        public string uri { get; set; } = "";
    }
}