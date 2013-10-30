using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pinboard.Models
{
    public class Type
    {
        public int TypeId { get; set; }
        public string Name { get; set; }
        public List<Pin> Pins { get; set; }

    }
}