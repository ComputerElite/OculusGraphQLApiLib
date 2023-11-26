using System;
using System.Collections.Generic;

namespace OculusGraphQLApiLib.Results
{
    public class Application
    {
        public string appName { get; set; } = "";
        public AppStoreOffer baseline_offer { get; set; } = null;
        public string canonicalName { get; set; } = "";
        public OculusImage cover_landscape_image { get; set; } = new OculusImage();
        public OculusImage cover_portrait_image { get; set; } = new OculusImage();
        public OculusImage cover_square_image { get; set; } = new OculusImage();
        public AppStoreOffer current_gift_offer { get; set; } = null;
        public AppStoreOffer current_offer { get; set; } = null;
        public AppStoreTrialOffer current_trial_offer { get; set; } = null;
        public string displayName { get { return display_name; } set { display_name = value; } }
        public string display_long_description { get; set; } = "";
        public string display_name { get; set; } = "";
        public Edges<Node<Review>> firstQualityRatings { get; set; } = new Edges<Node<Review>>();
        public List<string> genre_names { get; set; } = new List<string>();
        public bool has_in_app_ads { get; set; } = false;
        public string id { get; set; } = "";
        public bool is_approved { get; set; } = false;
        public bool is_concept { get; set; } = false; // aka AppLab
        public bool is_enterprise_enabled { get; set; } = false;
        public Organization organization { get; set; } = new Organization();
        public string platform { get; set; } = "";
        public string publisher_name { get; set; } = "";
        public double? quality_rating_aggregate { get; set; } = 0.0;
        public List<QualityRating> quality_rating_history_aggregate_all { get; set; } = new List<QualityRating>();
        public Nodes<ReleaseChannel> release_channels { get; set; } = new Nodes<ReleaseChannel>();
        public long? release_date { get; set; } = 0;
        public Nodes<Revision> revisions { get; set; } = new Nodes<Revision>();
        public List<OculusImage> screenshots { get; set; } = new List<OculusImage>();
        public Edges<Node<AndroidBinary>> supportedBinaries { get; set; } = new Edges<Node<AndroidBinary>>();
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
        public bool viewer_has_preorder { get; set; } = false;
        public string website_url { get; set; } = "";
        public AndroidBinary latest_supported_binary { get; set; } = new AndroidBinary();
        public ReleaseChannel viewer_release_channel { get; set; } = new ReleaseChannel();
        public bool is_blocked_by_verification { get; set; } = false;
        public string release_status { get; set; } = "";
        public ReleaseStatus release_status_enum
        {
            get
            {
                return (ReleaseStatus)Enum.Parse(typeof(ReleaseStatus), release_status);
            }
        }
        public bool is_device_targeting_enabled { get; set; } = false;
        public List<IAPEntitlement> active_dlc_entitlements { get; set; } = new List<IAPEntitlement>();
        public Nodes<AppStoreOffer> scheduled_offers { get; set; } = new Nodes<AppStoreOffer>();
        public List<ContextTopic> context_topics { get; set; } = new List<ContextTopic>();
        public ContextCategory context_category { get; set; } = new ContextCategory();
    }
    public class EdgesPrimaryBinaryApplication : Application
    {
        public Edges<Node<AndroidBinary>> primary_binaries { get; set; } = new Edges<Node<AndroidBinary>>();
    }

    public class NodesPrimaryBinaryApplication : Application
    {
        public Nodes<AndroidBinary> primary_binaries { get; set; } = new Nodes<AndroidBinary>();
    }

    public class OculusImage
    {
        public string file_name { get; set; } = "";
        public string uri { get; set; } = "";
        public string id { get; set; } = "";
        public string image_type { get; set; } = "";
        public ImageType image_type_enum
        {
            get
            {
                return (ImageType)Enum.Parse(typeof(ImageType), image_type);
            }
        }
    }

    public enum ImageType
    {
        APP_IMG_LOGO_TRANSPARENT,
        APP_IMG_HERO,
        APP_IMG_COVER_PORTRAIT,
        APP_IMG_COVER_SQUARE,
        APP_IMG_COVER_LANDSCAPE,
        APP_IMG_SMALL_LANDSCAPE,
        APP_IMG_ICON,
        APP_IMG_SCREENSHOT
    }
}