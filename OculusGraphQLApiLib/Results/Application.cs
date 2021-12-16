using System.Collections.Generic;

namespace OculusGraphQLApiLib.Results
{
    public class Application
    {
        public string id { get; set; } = "";
        public string platform { get; set; } = "";
        public Nodes<ReleaseChannel> release_channels { get; set; } = new Nodes<ReleaseChannel>();
        public Nodes<Revision> revisions { get; set; } = new Nodes<Revision>();
        public Organization organization { get; set; } = new Organization();
        public bool is_concept { get; set; } = false; // aka AppLab
        public bool is_approved { get; set; } = false;
        public bool is_enterprise_enabled { get; set; } = false;
        public string canonicalName { get; set; } = "";
        public long release_date { get; set; } = 0;
        public bool viewer_has_preorder { get; set; } = false;
        public Uri cover_landscape_image { get; set; } = new Uri();
        public Uri cover_portrait_image { get; set; } = new Uri();
        public Uri cover_square_image { get; set; } = new Uri();
        public List<Uri> screenshots { get; set; } = new List<Uri>();
    }
    public class EdgesPrimaryBinaryApplication : Application
    {
        public Edges<Node<AndroidBinary>> primary_binaries { get; set; } = new Edges<Node<AndroidBinary>>();
    }

    public class NodesPrimaryBinaryApplication : Application
    {
        public Nodes<AndroidBinary> primary_binaries { get; set; } = new Nodes<AndroidBinary>();
    }

    public class Uri
    {
        public string uri { get; set; } = "";
    }
}