using System.Collections.Generic;

namespace OculusGraphQLApiLib.Results;

public class ApplicationTranslation
{
    public Nodes<OculusImage> imagesExcludingScreenshotsAndMarkdown { get; set; } = new();
    public string id { get; set; } = "";
    public string locale { get; set; } = "";
    public List<OculusImage> screenshots { get; set; } = new();
    public OculusImage video_trailer { get; set; } = new();
    public OculusImage video_trailer_thumbnail { get; set; } = new();
    public string display_name { get; set; } = "";
    public Nodes<OculusImage> images { get; set; } = new();
    public List<string> keywords { get; set; } = new();
    public string long_description { get; set; } = "";
    public bool long_description_uses_markdown { get; set; } = false;
    public string short_description { get; set; } = "";
}