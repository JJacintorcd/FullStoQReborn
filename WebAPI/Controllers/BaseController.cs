using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public class BaseController : ControllerBase
    {
        public ObjectResult InternalServerError()
        {
            return new ObjectResult(HttpStatusCode.InternalServerError);
        }
    }
}
