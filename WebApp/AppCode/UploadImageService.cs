using AppUtility;
using Data;
using Infrastructure.Enum;
using Infrastructure;
using Newtonsoft.Json;
using System.Text;
using WebApp.Models;
using System.Drawing.Imaging;
using System.Drawing;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;

namespace WebApp.AppCode
{
    
    public class UploadImageService: IUploadImageService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AppSettings _appsetting;
        private readonly IRequestInfo _rinfo;

        public UploadImageService(IWebHostEnvironment webHostEnvironment, AppSettings appsetting, IRequestInfo rinfo)
        {
            _webHostEnvironment = webHostEnvironment;
            _appsetting = appsetting;
            _rinfo = rinfo;
        }

        public Response Upload(FileUploadModel model)
        {
            var response = new Response
            {
                StatusCode = ResponseStatus.Success,
                Msg = ResponseStatus.Success.ToString()
            };
            if (model.file.Length > 100 * 1024) // 100 KB limit
            {
                response.StatusCode = ResponseStatus.Failed;
                response.Msg = "Image size should not exceed 100 KB.";
                return response;
            }

            if (!model.IsSizeNotRestricted)
            {
                response = ValidateImage(model);
                if (response.StatusCode != ResponseStatus.Success)
                {
                    return response;
                }
            }

            bool isUploadDirectory = true;
            try
            {

                /* Cloud Flare */
                if (_appsetting.CloudFlare != null && !string.IsNullOrEmpty(_appsetting.CloudFlare.BaseURL) && !string.IsNullOrEmpty(_appsetting.CloudFlare.ApiKey) && !string.IsNullOrEmpty(_appsetting.CloudFlare.Authorization))
                {
                    isUploadDirectory = false;
                    CloudFlareResponse res = new CloudFlareResponse();
                    using (var client = new HttpClient())
                    {
                        string URL = _appsetting.CloudFlare.BaseURL + _appsetting.CloudFlare.ApiKey + "/images/v1";
                        var cloudrequest = new HttpRequestMessage(HttpMethod.Post, URL);
                        cloudrequest.Headers.Add("Authorization", "Bearer " + _appsetting.CloudFlare.Authorization);
                        var content = new MultipartFormDataContent();
                        using (var ms = new MemoryStream())
                        {
                            model.file.CopyTo(ms);
                            var msArr = ms.ToArray();
                            var fileStream = new StreamContent(new MemoryStream(msArr));
                            content.Add(fileStream, "file", DateTime.Now.ToString("ddMMyyyyhhmmssff"));
                            cloudrequest.Content = content;
                            var cloudresponse = client.SendAsync(cloudrequest).Result;
                            if (cloudresponse.StatusCode == System.Net.HttpStatusCode.NotFound)
                            {
                                //response.StatusCode = ResponseStatus.Failed;
                                //response.ResponseText = "Invalid Cloud Url!";
                                isUploadDirectory = true; // Set flag to save the file to the directory
                            }
                            else
                            {
                                res = JsonConvert.DeserializeObject<CloudFlareResponse>(cloudresponse.Content.ReadAsStringAsync().Result);
                                if (res.success)
                                {
                                    response.StatusCode = ResponseStatus.Success;
                                    response.Msg = res.result.variants.FirstOrDefault();
                                }
                                else
                                {
                                    //response.StatusCode = ResponseStatus.Failed;
                                    //response.ResponseText = res.errors.FirstOrDefault()?.message;
                                    isUploadDirectory = true;
                                }
                            }
                        }
                    }
                }

                if (isUploadDirectory)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(model.FilePath);
                    if (!Directory.Exists(sb.ToString()))
                    {
                        Directory.CreateDirectory(sb.ToString());
                    }
                    var filename = ContentDispositionHeaderValue.Parse(model.file.ContentDisposition).FileName.Trim('"');
                    string originalExt = Path.GetExtension(filename).ToLower();
                    string[] Extensions = { ".png", ".jpeg", ".jpg", ".svg" };
                    if (Extensions.Contains(originalExt))
                    {
                        //originalExt = ".jpg";
                    }
                    if (string.IsNullOrEmpty(model.FileName))
                    {
                        model.FileName = filename;//Path.GetFileNameWithoutExtension(request.FileName).ToLower() + originalExt;
                    }
                    sb.Append(model.FileName);
                    using (FileStream fs = File.Create(sb.ToString()))
                    {
                        model.file.CopyTo(fs);
                        fs.Flush();
                        if (model.IsThumbnailRequired)
                        {
                            GenrateThumbnail(model.file, model.FileName, 20L);
                        }
                    }
                    response.StatusCode = ResponseStatus.Success;
                    response.Msg = $"/{model.FilePath.Replace("wwwroot/", "")}{model.FileName}";
                }
            }
            catch (Exception ex)
            {
                response.Msg = "Error in file uploading. Try after sometime...";
            }
            return response;
        }

        private Response ValidateImage(FileUploadModel request)
        {
            var response = new Response
            {
                StatusCode = ResponseStatus.Success,
                Msg = ResponseStatus.Success.ToString()
            };
            var resText = "Image size is not correct";
            if (_appsetting.IsImageSizeRestricted)
            {
                if (request.file.Length > 100 * 1024) // 100 KB limit
                {
                    response.StatusCode = ResponseStatus.Failed;
                    response.Msg = "Image size should not exceed 100 KB.";
                    return response;
                }

                if (!request.IsSizeNotRestricted)
                {
                    request.ImageConfigration = string.IsNullOrEmpty(request.ImageConfigration) ? "DEFAULT" : request.ImageConfigration.ToUpper();
                    var allowedSize = _appsetting.ImageConfiguration.Where(x => x.Name?.ToUpper() == request.ImageConfigration).FirstOrDefault();
                    if (allowedSize == null)
                    {
                        request.ImageConfigration = "DEFAULT";
                        allowedSize = _appsetting.ImageConfiguration.Where(x => x.Name?.ToUpper() == request.ImageConfigration).FirstOrDefault();
                    }
                    Image images = Image.FromStream(request.file.OpenReadStream());
                    var attribute = allowedSize.Attribute;
                    if (request.ImageConfigration == "DEFAULT" || request.ImageConfigration == "DOCIMAGE")
                    {
                        resText = "Image size must be between " + attribute.MinWidth + "px x " + attribute.MaxHeight + "px";
                    }
                    if (images.Width < attribute.MinWidth || images.Width > attribute.MaxWidth || images.Height < attribute.MinHeight || images.Height > attribute.MaxHeight)
                    {
                        response.StatusCode = ResponseStatus.Failed;
                        response.Msg = resText;//"Image size must be between 1080*1080 pixels.";
                        return response;
                    }
                }
            }

            var fileValidRes = Validate.O.IsFileValid(request.file);
            if (fileValidRes.StatusCode != ResponseStatus.Success)
            {
                response.StatusCode = fileValidRes.StatusCode;
                response.Msg = fileValidRes.Msg;
                return response;
            }
            return response;
        }
        private bool GenrateThumbnail(IFormFile file, string fileName, long quality = 20L)
        {
            string tempImgNameWithPath = string.Concat(FileDirectories.Thumbnail, fileName);
            var newimg = new Bitmap(file.OpenReadStream());
            ImageCodecInfo jgpEncoder = GetEncoderInfo("image/jpeg");
            // for the Quality parameter category.
            System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
            EncoderParameters myEncoderParameters = new EncoderParameters(1);
            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, quality);
            myEncoderParameters.Param[0] = myEncoderParameter;
            try
            {
                if (File.Exists(tempImgNameWithPath))
                {
                    File.Delete(tempImgNameWithPath);
                }
                newimg.Save(tempImgNameWithPath, jgpEncoder, myEncoderParameters);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == mimeType)
                    return codecs[i];
            return null;
        }
    }
}

