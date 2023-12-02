using System;
using System.Collections.Generic;

namespace OculusGraphQLApiLib.Results;

public class PDPMetadata
{
    public PDPMetadataApplication application { get; set; } = new PDPMetadataApplication();
    public IARCCertificate iarc_cert{ get; set; } = new IARCCertificate();
    public Nodes<ApplicationTranslation> translations { get; set; } = new Nodes<ApplicationTranslation>();
    public string developer_name { get; set; } = "";
    public string developer_privacy_policy_url { get; set; } = "";
    public string developer_terms_of_service_url { get; set; } = "";
    public string publisher_name { get; set; } = "";
    public string website_url { get; set; } = "";
    public string category { get; set; } = "";
    public Category category_enum
    {
        get
        {
            if (String.IsNullOrEmpty(category)) return Category.UNKNOWN;
            return (Category)Enum.Parse(typeof(Category), category);
        }
    }
    public string comfort_rating { get; set; } = "";
    public ComfortRating comfort_rating_enum
    {
        get
        {
            if (String.IsNullOrEmpty(comfort_rating)) return ComfortRating.UNKNOWN;
            return (ComfortRating)Enum.Parse(typeof(ComfortRating), comfort_rating);
        }
    }
    public ContextCategory context_category { get; set; } = new ContextCategory();
    public string external_subscription_type { get; set; } = "";
    public ExternalSubscriptionType external_subscription_type_enum
    {
        get
        {
            if (String.IsNullOrEmpty(external_subscription_type)) return ExternalSubscriptionType.UNKNOWN;
            return (ExternalSubscriptionType)Enum.Parse(typeof(ExternalSubscriptionType), external_subscription_type);
        }
    }
    public List<string> genres { get; set; } = new List<string>();
    public List<Genre> genres_enum
    {
        get
        {
            List<Genre> genres_enum = new List<Genre>();
            foreach (string genre in genres)
            {
                genres_enum.Add((Genre)Enum.Parse(typeof(Genre), genre));
            }
            return genres_enum;
        }
    }
    public bool has_in_app_ads { get; set; } = false;
    public string internet_connection { get; set; } = "";
    public InternetConnection internet_connection_enum
    {
        get
        {
            if (String.IsNullOrEmpty(internet_connection)) return InternetConnection.UNKNOWN;
            return (InternetConnection)Enum.Parse(typeof(InternetConnection), internet_connection);
        }
    }
    public string play_area { get; set; } = "";
    public PlayArea play_area_enum
    {
        get
        {
            if (String.IsNullOrEmpty(play_area)) return PlayArea.UNKNOWN;
            return (PlayArea)Enum.Parse(typeof(PlayArea), play_area);
        }
    }
    public string recommended_graphics { get; set; } = null;
    public double? recommended_memory_gb { get; set; } = null;
    public string recommended_processor { get; set; } = null;
    public List<string> supported_in_app_languages { get; set; } = new List<string>();
    public List<SupportedInAppLanguage> supported_in_app_languages_enum
    {
        get
        {
            List<SupportedInAppLanguage> supported_in_app_languages_enum = new List<SupportedInAppLanguage>();
            foreach (string supported_in_app_language in supported_in_app_languages)
            {
                supported_in_app_languages_enum.Add((SupportedInAppLanguage)Enum.Parse(typeof(SupportedInAppLanguage), supported_in_app_language, true));
            }
            return supported_in_app_languages_enum;
        }
    }
    public List<string> supported_input_devices { get; set; } = new List<string>();
    public List<SupportedInputDevice> supported_input_devices_enum
    {
        get
        {
            List<SupportedInputDevice> supported_input_devices_enum = new List<SupportedInputDevice>();
            foreach (string supported_input_device in supported_input_devices)
            {
                supported_input_devices_enum.Add((SupportedInputDevice)Enum.Parse(typeof(SupportedInputDevice), supported_input_device, true));
            }
            return supported_input_devices_enum;
        }
    }
    public List<string> supported_player_modes { get; set; } = new List<string>();
    public List<SupportedPlayerMode> supported_player_modes_enum
    {
        get
        {
            List<SupportedPlayerMode> supported_player_modes_enum = new List<SupportedPlayerMode>();
            foreach (string supported_player_mode in supported_player_modes)
            {
                supported_player_modes_enum.Add((SupportedPlayerMode)Enum.Parse(typeof(SupportedPlayerMode), supported_player_mode, true));
            }
            return supported_player_modes_enum;
        }
    }
    public List<string> supported_tracking_modes { get; set; } = new List<string>();
    public List<SupportedTrackingMode> supported_tracking_modes_enum
    {
        get
        {
            List<SupportedTrackingMode> supported_tracking_modes_enum = new List<SupportedTrackingMode>();
            foreach (string supported_tracking_mode in supported_tracking_modes)
            {
                supported_tracking_modes_enum.Add((SupportedTrackingMode)Enum.Parse(typeof(SupportedTrackingMode), supported_tracking_mode, true));
            }
            return supported_tracking_modes_enum;
        }
    }
    public List<string> user_interaction_modes { get; set; } = new List<string>();
    public List<UserInteractionMode> user_interaction_modes_enum
    {
        get
        {
            List<UserInteractionMode> user_interaction_modes_enum = new List<UserInteractionMode>();
            foreach (string user_interaction_mode in user_interaction_modes)
            {
                user_interaction_modes_enum.Add((UserInteractionMode)Enum.Parse(typeof(UserInteractionMode), user_interaction_mode, true));
            }
            return user_interaction_modes_enum;
        }
    }

    public string id { get; set; } = "";
}