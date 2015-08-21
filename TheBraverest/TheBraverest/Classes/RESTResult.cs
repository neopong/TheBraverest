using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace TheBraverest.Classes
{
    public class RESTResult<T>
    {
        public bool Success { get; set; }
        public T ReturnObject { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}