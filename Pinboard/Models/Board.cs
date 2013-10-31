using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pinboard.Models
{
    public class Board
    {
        public int BoardId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public UserProfile User { get; set; }
        public List<Pin> Pins { get; set; }
    }
}