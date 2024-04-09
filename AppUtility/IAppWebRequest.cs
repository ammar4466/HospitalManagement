using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppUtility
{
    public interface IAppWebRequest
    {
        Task<HttpResponse> PostAsync(string URL, string PostData = "", string AccessToken = "", string ContentType = "application/json", int timeout = 0);
    }
}
