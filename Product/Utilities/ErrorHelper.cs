using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Productmicroservice.Utilities
{
    public class ErrorHelper : IErrorHelper
    {
        public void HandleError(string errorTag, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
        {
            throw new MyMicroserviceException
            {
                ErrorTag = errorTag,
                HttpStatusCode = httpStatusCode,
                Success = false
            };
        }
    }
}
