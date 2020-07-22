using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.HtmlComponents
{
    public class BigButtonGroup
    {
        public string GroupName { get; set; }
        public List<BigButton> Buttons { get; set; }
    }
}
