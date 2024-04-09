using AppUtility.Extensions;
using Infrastructure;
using Infrastructure.Enum;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AppUtility
{
     public class Validate
    {
        public static Validate O => instance.Value;
        private static Lazy<Validate> instance = new Lazy<Validate>(() => new Validate());
        private Validate() { }
        public string WebURLExpression = @"^(http|https)\://[a-zA-Z0-9\-\.]+\.[a-zA-Z](:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;%\$#\=~])*[^\.\,\)\(\s]$";
        private byte[] GetSubBytes(byte[] oldBytes, int start, int len)
        {
            if (oldBytes.Length >= len && start > -1 && start < len)
            {
                byte[] newByteArr = new byte[len];
                Array.Copy(oldBytes, start, destinationArray: newByteArr, destinationIndex: 0, length: len);
                return newByteArr;
            }
            return null;
        }
        public readonly IEnumerable<string> FileFormatsAllowed = new List<string> { ".WEBP", ".JPEG", ".JPG", ".PNG", ".DOCX", ".GIF", ".PDF", ".ZIP", ".RAR" };
        private IEnumerable<string> CheckFFSignature = new List<string> { "RIFF", "EXIF", "JPG", "JPEG", "JFIF", "PNG", "GIF", "%PDF", "PK", "GIF"};
        
        public bool IsValidEmployeeCount(int EmployeeCount, string Scope)
        {
            bool result = true;
            Scope = Scope.Replace("and", ",");
            var Arr = Scope.Split(',');
            if (EmployeeCount < Arr.Count())
            {
                result = false;
            }
            return result;
        }


        public bool IsFileAllowed(byte[] fileContent, string ext)
        {
            if (fileContent == null)
                return false;
            if (string.IsNullOrEmpty(ext))
                return false;
            if (!ext.ToUpper().In(FileFormatsAllowed))
                return false;
            var SubByte = GetSubBytes(fileContent, 0, 20);
            string Start20BytesStr = SubByte?.Length > 0 ? Encoding.UTF8.GetString(SubByte) : "";
            if (Start20BytesStr.Length < 1)
                return false;
            if (!CheckFFSignature.Any(Start20BytesStr.ToUpper().Contains))
                return false;
            return true;
        }

        public Response IsFileValid(IFormFile file)
        {
            var res = new Response
            {
                StatusCode = ResponseStatus.Failed,
                Msg = "Temperory Error"
            };
            if (file != null)
            {
                var filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                string ext = Path.GetExtension(filename).ToLower();

                byte[] filecontent = null;
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    filecontent = ms.ToArray();
                }
                if (!IsFileAllowed(filecontent, ext))
                {
                    res.Msg = "Invalid File Format!";
                    return res;
                }
                else if (!file.ContentType.Any())
                    res.Msg = "File not found!";
                else if (file.Length < 1)
                    res.Msg = "Empty file not allowed!";
                else if (file.Length / 1024 > 1024 && !ext.ToLower().In(".zip", ".rar"))
                    res.Msg = "File size exceeded! Not more than 1 MB is allowed";
                else
                {
                    res.StatusCode = ResponseStatus.Success;
                    res.Msg = "it is a valid file";
                }

            }
            else
            {
                res.Msg = "No File Found";
            }
            return res;
        }


    }
}
