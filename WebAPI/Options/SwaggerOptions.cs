using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Options
{
    public class SwaggerOptions
    {
        public string JsonRoute { get; set; }
        public string ApiDescription { get; set; }
        public string UiEndpoint { get; set; }
    }
}
