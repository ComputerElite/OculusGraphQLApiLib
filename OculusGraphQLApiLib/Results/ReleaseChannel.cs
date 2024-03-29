﻿namespace OculusGraphQLApiLib.Results
{
    public class ReleaseChannel : ReleaseChannelWithoutLatestSupportedBinary
    {
        public OculusBinary latest_supported_binary { get; set; } = new OculusBinary();
        
    }
    
    public class ReleaseChannelWithoutLatestSupportedBinary
    {
        public string id { get; set; } = "";
        public string channel_name { get; set; } = "";
        
    }
}