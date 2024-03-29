﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputerUtils.VarUtils;

namespace OculusGraphQLApiLib.Results
{
    public class AssetFile
    {
        public string file_name { get; set; } = "";
        public string uri { get; set; } = "";
        public string size { get; set; } = "0";
        public string id { get; set; } = "";
        public bool is_required { get; set; } = false;
        public bool? is_segmented { get; set; } = null;
        public BinaryApplication binary_application { get; set; } = new BinaryApplication();
        public string platform { get; set; } = "";
        public OculusPlatform platform_enum
        {
            get
            {
                if(String.IsNullOrEmpty(platform)) return OculusPlatform.UNKNOWN;
                return (OculusPlatform)Enum.Parse(typeof(OculusPlatform), platform);
            }
        }
        public long? created_date { get; set; } = 0;
        public DateTime created_date_datetime
        {
            get
            {
                if(created_date == null) return DateTime.MinValue;
                return TimeConverter.UnixTimeStampToDateTime(created_date);
            }
        }
        public long sizeNumerical
        {
            get
            {
                return long.Parse(size);
            }
            set
            {
                return;
            }
        }
    }
}
