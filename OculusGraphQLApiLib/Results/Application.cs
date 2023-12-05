using System;
using System.Collections.Generic;

namespace OculusGraphQLApiLib.Results
{
    public class Application
    {
        public string appName { get; set; } = "";
        public bool cloud_backup_enabled { get; set; } = false;
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
        public OculusPlatform platform_enum
        {
            get
            {
                if(String.IsNullOrEmpty(platform)) return OculusPlatform.UNKNOWN;
                return (OculusPlatform)Enum.Parse(typeof(OculusPlatform), platform);
            }
        }
        public string publisher_name { get; set; } = "";
        public double? quality_rating_aggregate { get; set; } = 0.0;
        public List<QualityRating> quality_rating_history_aggregate_all { get; set; } = new List<QualityRating>();
        public Nodes<ReleaseChannel> release_channels { get; set; } = new Nodes<ReleaseChannel>();
        public long? release_date { get; set; } = 0;
        public DateTime? releaseDate
        {
            get
            {
                if(release_date == null) return null;
                return DateTimeOffset.FromUnixTimeSeconds((long)release_date).DateTime;
            }
        }
        public Nodes<Revision> revisions { get; set; } = new Nodes<Revision>();
        public List<OculusImage> screenshots { get; set; } = new List<OculusImage>();
        public Edges<Node<OculusBinary>> supportedBinaries { get; set; } = new Edges<Node<OculusBinary>>();
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
        public OculusBinary latest_supported_binary { get; set; } = new OculusBinary();
        public ReleaseChannel viewer_release_channel { get; set; } = new ReleaseChannel();
        public bool? is_blocked_by_verification { get; set; } = null;
        public string release_status { get; set; } = "";
        public ReleaseStatus release_status_enum
        {
            get
            {
                if(String.IsNullOrEmpty(release_status)) return ReleaseStatus.UNKNOWN;
                return (ReleaseStatus)Enum.Parse(typeof(ReleaseStatus), release_status);
            }
        }
        public bool is_device_targeting_enabled { get; set; } = false;
        public List<IAPEntitlement> active_dlc_entitlements { get; set; } = new List<IAPEntitlement>();
        public Nodes<AppStoreOffer> scheduled_offers { get; set; } = new Nodes<AppStoreOffer>();
        public List<ContextTopic> context_topics { get; set; } = new List<ContextTopic>();
        public ContextCategory context_category { get; set; } = new ContextCategory();
        public List<string> user_interaction_modes { get; set; } = new List<string>();
        public List<UserInteractionMode> user_interaction_modes_enum
        {
            get
            {
                List<UserInteractionMode> userInteractionModes = new List<UserInteractionMode>();
                foreach (string s in user_interaction_modes)
                {
                    userInteractionModes.Add((UserInteractionMode)Enum.Parse(typeof(UserInteractionMode), s));
                }
                return userInteractionModes;
            }
        }
        public bool is_quest_for_business { get; set; } = false;
        public ApplicationGrouping grouping { get; set; } = new ApplicationGrouping();
        public bool is_test { get; set; } = false;
        public bool is_trial_offer_valid { get; set; } = false;
        public Nodes<ApplicationRevision> firstRevision { get; set; } = new Nodes<ApplicationRevision>();
        public Nodes<ApplicationRevision> lastRevision { get; set; } = new Nodes<ApplicationRevision>();
        public Nodes<AppStoreOffer> firstOffer { get; set; } = new Nodes<AppStoreOffer>();
        public bool is_for_oculus_keys_only { get; set; } = false;
        public Nodes<ApplicationRevision> revisionsIncludingVariantMetadataRevisions { get; set; } = new Nodes<ApplicationRevision>();
        public List<string> share_capabilities { get; set; } = null;
        public List<ShareCapability> share_capabilities_enum
        {
            get
            {
                if (share_capabilities == null) return new List<ShareCapability>();
                List<ShareCapability> shareCapabilities = new List<ShareCapability>();
                foreach (string s in share_capabilities)
                {
                    shareCapabilities.Add((ShareCapability)Enum.Parse(typeof(ShareCapability), s));
                }
                return shareCapabilities;
            }
        }
        
        
    }
    public class EdgesPrimaryBinaryApplication : Application
    {
        public Edges<Node<OculusBinary>> primary_binaries { get; set; } = new Edges<Node<OculusBinary>>();
    }

    public class NodesPrimaryBinaryApplication : Application
    {
        public Nodes<OculusBinary> primary_binaries { get; set; } = new Nodes<OculusBinary>();
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
                if (image_type == "") return ImageType.UNKNOWN;
                return (ImageType)Enum.Parse(typeof(ImageType), image_type);
            }
        }
    }

    public enum ImageType
    {
        UNKNOWN = -1,
        APP_IMG_LOGO_TRANSPARENT = 0,
        APP_IMG_HERO = 1,
        APP_IMG_COVER_PORTRAIT = 2,
        APP_IMG_COVER_SQUARE = 3,
        APP_IMG_COVER_LANDSCAPE = 4,
        APP_IMG_SMALL_LANDSCAPE = 5,
        APP_IMG_ICON = 6,
        APP_IMG_SCREENSHOT = 7,
        ACHIEVEMENT_IMG_LOCKED = 8,
        ACHIEVEMENT_IMG_UNLOCKED = 9,
        APP_IMG_IMMERSIVE_LAYER_OBJECT_RIGHT = 10,
        APP_IMG_IMMERSIVE_LAYER_OBJECT_LEFT = 11,
        APP_IMG_IMMERSIVE_LAYER_BACKDROP = 12,
        APP_IMG_IMMERSIVE_LAYER_LOGO = 13,
        APP_IMG_CUBEMAP_SOURCE = 14
    }
    
    public class ApplicationForApplicationGroupingNodes : Application
    {
        public ApplicationGroupingNodes grouping { get; set; } = new ApplicationGroupingNodes();
    }
}