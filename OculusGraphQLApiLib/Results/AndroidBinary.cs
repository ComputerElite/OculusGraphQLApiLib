namespace OculusGraphQLApiLib.Results
{
    public class AndroidBinary : GraphQLBase
    {
        public string id { get; set; } = "";
        public string version { get; set; } = "";
        public string platform { get; set; } = "";
        public string file_name { get; set; } = "";
        public string uri { get; set; } = "";
        public string change_log { get; set; } = "";
        public EdgesPrimaryBinaryApplication binary_application { get; set; } = new EdgesPrimaryBinaryApplication();
        public string __isAppBinary { get; set; } = "";
        //public Edges<AssetFile> asset_files { get; set; } = ;
        //public Edges<DebugSymbol> debug_symbols { get; set; } = ;
        public string __isAppBinaryWithFileAsset { get; set; } = "";
        public long version_code { get; set; } = 0;
        public long created_date { get; set; } = 0;
        public Nodes<ReleaseChannel> binary_release_channels { get; set; } = null;
    }
}