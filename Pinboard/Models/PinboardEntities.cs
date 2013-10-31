using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Pinboard.Models
{
    public class PinboardEntities :DbContext
    {
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Pin> Pins { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<Board> Boards { get; set; }

    }

}