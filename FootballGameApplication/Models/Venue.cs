using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FootballGameApplication.Models
{
    public class Venue
    {
        [Key]
        public int VenueID { get; set; }
        public string VenueName { get; set; }
        public string Location { get; set; }

        public ICollection<Player> Players { get; set; }


    }
}