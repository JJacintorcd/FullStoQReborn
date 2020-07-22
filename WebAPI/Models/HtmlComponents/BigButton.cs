using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.HtmlComponents
{
    public class BigButton
    {
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Icon { get; set; }
        public int Size { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
    }
}
