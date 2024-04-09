namespace WebApp.Models
{
    public class AppSettings
    {
        public string WebAPIBaseUrl { get; set; }
        public CloudFlareSetting CloudFlare { get; set; }
        public bool IsImageSizeRestricted { get; set; }
        public List<ImageSizeConfig> ImageConfiguration { get; set; }
    }
    public class CloudFlareSetting
    {
        public string Authorization { get; set; }
        public string ApiKey { get; set; }
        public string BaseURL { get; set; }
    }
    public class ImageSizeConfig
    {
        public string Name { get; set; }
        public ImageSizeAttribute Attribute { get; set; }
    }
    public class ImageSizeAttribute
    {
        public int MinWidth { get; set; }
        public int MaxWidth { get; set; }
        public int MinHeight { get; set; }
        public int MaxHeight { get; set; }
    }
}
