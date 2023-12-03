using System;
using System.Collections.Generic;

namespace OculusGraphQLApiLib.Results;

public class AchievementDefinition
{
    public ApplicationGrouping application_grouping { get; set; } = new ApplicationGrouping();
    public bool is_archived { get; set; } = false;
    public bool is_draft { get; set; } = false;
    public bool is_secret { get; set; } = false;
    public string api_name { get; set; } = "";
    public string description { get; set; } = "";
    public string id { get; set; } = "";
    public string title { get; set; } = "";
    public OculusImage unlocked_image { get; set; } = new OculusImage();
    public string achievement_type { get; set; } = "";
    public AchievementType achievement_type_enum
    {
        get
        {
            if(String.IsNullOrEmpty(achievement_type)) return AchievementType.UNKNOWN;
            return (AchievementType)Enum.Parse(typeof(AchievementType), achievement_type);
        }
    }
    public string achievement_write_policy { get; set; } = "";
    public AchievementWritePolicy achievement_write_policy_enum
    {
        get
        {
            if(String.IsNullOrEmpty(achievement_write_policy)) return AchievementWritePolicy.UNKNOWN;
            return (AchievementWritePolicy)Enum.Parse(typeof(AchievementWritePolicy), achievement_write_policy);
        }
    }
    public List<OverrideTranslation> description_locale_map { get; set; } = new List<OverrideTranslation>();
    public List<OverrideTranslation> title_locale_map { get; set; } = new List<OverrideTranslation>();
    public List<OverrideTranslation> unlocked_description_override_locale_map { get; set; } = new List<OverrideTranslation>();
}