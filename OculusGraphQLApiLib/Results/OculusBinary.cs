using System;
using System.Collections.Generic;
using System.ComponentModel;
using ComputerUtils.VarUtils;

namespace OculusGraphQLApiLib.Results
{
    public class OculusBinary : GraphQLBase
    {
        public BinaryApplication binary_application { get; set; } = null;
        public string id { get; set; } = "";
        public string version { get; set; } = "";

        public string platform
        {
            get
            {
                return _platform;
            }
            set
            {
                if (value != "") _platform = value;
            }
        }

        private string _platform = "";

        public string package_name { get; set; } = null;
        public List<string> targeted_devices { get; set; } = new List<string>();
        public int? min_android_sdk_version { get; set; } = null;
        public int? max_android_sdk_version { get; set; } = null;
        public int? target_android_sdk_version { get; set; } = null;

        public List<Headset> targeted_devices_enum
        {
            get
            {
                List<Headset> headsets = new List<Headset>();
                foreach (string s in targeted_devices)
                {
                    headsets.Add((Headset)Enum.Parse(typeof(Headset), s));
                }
                return headsets;
            }
        }

        public string file_name { get; set; } = "";
        public string uri { get; set; } = "";
        public string change_log { get { return changeLog; } set { changeLog = value; } }
        public string changeLog { get; set; } = null;
        public string richChangeLog { get; set; } = "";
        public bool firewall_exceptions_required { get; set; } = false;
        public bool is_2d_mode_supported { get; set; } = false;
        public string launch_file { get; set; } = "";
        public string launch_file_2d { get; set; } = null;
        public string launch_parameters { get; set; } = "";
        public string launch_parameters_2d { get; set; } = null;
        public string Platform  {
            get
            {
                return _platform;
            }
            set
            {
                if (value != "") _platform = value;
            }
        }
        public string release_notes_plain_text { get; set; } = "";
        public string required_space { get; set; } = "0";
        public long required_space_numerical { get
            {
                return long.Parse(required_space);
            } }
        public string total_installed_space { get; set; } = "0";
        public long total_installed_space_numerical { get
        {
            return long.Parse(total_installed_space);
        } }

        public string required_space_adjusted { get; set; } = "0";

        public long required_space_adjusted_numerical { get
        {
            return long.Parse(required_space_adjusted);
        } }

        public string size { get; set; } = "0";
        public long size_numerical
        {
            get
            {
                return long.Parse(size);
            }
        }
        public List<string> supported_hmd_platforms { get; set; } = new List<string>();
        public List<Headset> supported_hmd_platforms_enum
        {
            get
            {
                List<Headset> headsets = new List<Headset>();
                foreach (string s in supported_hmd_platforms)
                {
                    headsets.Add((Headset)Enum.Parse(typeof(Headset), s));
                }
                return headsets;
            }
        }

        public List<string> permissions { get; set; } = new List<string>();
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// WARNING!!! IN CASE SOMETHING DOESN'T WORK SOMEWHERE ADD binary_application BACK IN AND FIX RECURSION ///
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //public EdgesPrimaryBinaryApplication binary_application { get; set; } = new EdgesPrimaryBinaryApplication();
        public string __isAppBinary { get; set; } = "";
        //public Edges<AssetFile> asset_files { get; set; } = new Edges<AssetFile>();
        public Edges<DebugSymbol> debug_syymbols { get; set; } = new Edges<DebugSymbol>();
        public string __isAppBinaryWithFileAsset { get; set; } = "";
        public long version_code { get { return versionCode; } set { versionCode = value; } }
        public long versionCode { get; set; } = 0;
        public long? created_date { get; set; } = 0;
        public DateTime created_date_datetime
        {
            get
            {
                if(created_date == null) return DateTime.MinValue;
                return TimeConverter.UnixTimeStampToDateTime(created_date);
            }
        }
        public Nodes<ReleaseChannel> binary_release_channels { get; set; } = null;
        public Edges<Node<AppItemBundle>> lastIapItems { get; set; } = new Edges<Node<AppItemBundle>>();
        public Edges<Node<AppItemBundle>> firstIapItems { get; set; } = new Edges<Node<AppItemBundle>>();
        public Nodes<AssetFile> asset_files { get; set; } = new Nodes<AssetFile>();
        public AssetFile obb_binary { get; set; } = null;
        public List<TargetableDevicesLockInfo> targetable_devices_lock_info { get; set; } = new List<TargetableDevicesLockInfo>();

        public override string ToString()
        {
            return "Version: " + version + " (" + id + ")\nChangelog: " + change_log;
        }
        public string release_status { get; set; } = "";

        public ReleaseStatus release_status_enum
        {
            get
            {
                if(String.IsNullOrEmpty(release_status)) return ReleaseStatus.UNKNOWN;
                return (ReleaseStatus)Enum.Parse(typeof(ReleaseStatus), release_status, true);
            }
        }

        public string status { get; set; } = "";

        public BinaryStatus status_enum
        {
            get
            {
                if(String.IsNullOrEmpty(release_status)) return BinaryStatus.UNKNOWN;
                return (BinaryStatus)Enum.Parse(typeof(BinaryStatus), status);
            }
        }
        
        public bool is_pre_download_enabled { get; set; } = false;
    }
}