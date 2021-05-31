﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace usermicroservice.Utilities
{
    public interface IErrorHelper
    {
        void HandleError(string errorTag, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest);
    }
}
