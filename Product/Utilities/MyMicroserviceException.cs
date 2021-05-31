using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Productmicroservice.Utilities
{
    public class MyMicroserviceException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public string ErrorTag { get; set; }

        public bool Success { get; set; }
    }
}
