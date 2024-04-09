using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class RequestInfo : IRequestInfo
    {
        private readonly IHttpContextAccessor _accessor;
        public RequestInfo(IHttpContextAccessor httpContext)
        {
            _accessor = httpContext;
        }
        public string GetDomain()
        {
            var domain = $"{_accessor.HttpContext.Request.Scheme}://{_accessor.HttpContext.Request.Host.Host}:{_accessor.HttpContext.Request.Host.Port}";
            return domain;
        }
        public string GetAbsoluteURI()
        {
            var request = _accessor.HttpContext.Request;
            return request.Scheme + "://" + request.Host + request.Path;
        }
    }
}
