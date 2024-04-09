using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppUtility
{
    public class FileUploadModel
    {
        public IFormFile file { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public bool IsThumbnailRequired { get; set; }
        public bool IsSizeNotRestricted { get; set; }
        public string ImageConfigration { get; set; }
    }
    public class FileDirectories
    {
        public const string ImageSuffix = "Uploads/";
        public static string Uploads = Path.Combine($"wwwroot/{ImageSuffix}");
        public static string Thumbnail = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Thumbnail/");
    }


}
