using Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class Response
    {
        public ResponseStatus StatusCode { get; set; }
        public string Msg { get; set; }
    }
}
