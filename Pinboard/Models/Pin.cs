using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pinboard.Models
{
    public class Pin
    {
        public int PinId { get; set; }
        public int TypeId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public Type Type { get; set; }
        public Board Board { get; set; }
        public UserProfile User { get; set; }
    }
}