using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppUtility
{
    public class CloudFlareResponse
    {
        public Result result { get; set; }
        public bool success { get; set; }
        public List<Error> errors { get; set; }
    }
    public class Result
    {
        public List<string> variants { get; set; }
    }
    public class Error
    {
        public int code { get; set; }
        public string message { get; set; }
    }
}
