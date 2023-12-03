using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputerUtils.VarUtils;

namespace OculusGraphQLApiLib.Results
{
    public class IAPItem : GraphQLBase
    {
        public AppStoreOffer current_offer { get; set; } = new AppStoreOffer();
        public string display_name { get; set; } = "";
        public string display_short_description { get; set; } = "";
        public string file_name { get; set; }
        public string id { get; set; } = "";
        public ParentApplication parentApplication { get; set; } = new ParentApplication();
        public ParentApplication parent_application
        {
            get
            {
                return parentApplication;
            }
        }
        public AssetFile latest_supported_asset_file { get; set; } = new AssetFile();
        public Nodes<AssetFile> asset_files { get; set; } = new Nodes<AssetFile>();
        public bool is_cancelled { get; set; } = false;
        public bool is_concept { get; set; } = false;
        public bool show_in_store { get; set; } = false;
        public long? release_date { get; set; } = 0;
        public ApplicationGrouping app_grouping { get; set; } = new ApplicationGrouping();
        public Nodes<IAPRevision> revisions { get; set; } = new Nodes<IAPRevision>();
        public IAPRevision latest_revision { get; set; } = new IAPRevision();
        public DateTime release_date_datetime
        {
            get
            {
                if(release_date == null) return DateTime.MinValue;
                return TimeConverter.UnixTimeStampToDateTime(release_date);
            }
        }
        public string sku { get; set; } = "";
        public Nodes<MSRPOffer> msrp_offers { get; set; } = new Nodes<MSRPOffer>();
        public string iap_type { get; set; } = "";
        public IAPType iap_type_enum
        {
            get
            {
                if(String.IsNullOrEmpty(iap_type)) return IAPType.UNKNOWN;
                return (IAPType)Enum.Parse(typeof(IAPType), iap_type);
            }
        }
        public override string ToString()
        {
            return "DLC '" + display_name + "' (" + id + ") of " + parentApplication.id;
        }
    }
}
