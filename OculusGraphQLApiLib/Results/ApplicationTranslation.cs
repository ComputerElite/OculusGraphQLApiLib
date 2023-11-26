using System.Collections.Generic;

namespace OculusGraphQLApiLib.Results;

public class ApplicationTranslation
{
    public List<OculusImage> images { get; set; } = new List<OculusImage>();
    public OculusImage video_trailer { get; set; } = new OculusImage();
    public OculusImage video_trailer_thumbnail { get; set; } = new OculusImage();
    public string locale { get; set; } = "";
    public List<string> keywords { get; set; } = new List<string>();
    public string long_description { get; set; } = "";
    public bool long_description_uses_markdown { get; set; } = false;
    public Nodes<OculusImage> long_description_videos { get; set; } = new Nodes<OculusImage>();
    public string short_description { get; set; } = "";
    public string display_name { get; set; } = "";
    public string id { get; set; } = "";
    public Nodes<OculusImage> imagesExcludingScreenshotsAndMarkdown { get; set; } = new Nodes<OculusImage>();
}