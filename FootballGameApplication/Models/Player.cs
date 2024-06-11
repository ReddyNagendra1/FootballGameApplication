using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballGameApplication.Models
{
    public class Player
    {
        [Key]
        public int PlayerID { get; set; }
        public string PlayerName { get; set; }
        public DateTime DateOfBirth { get; set; }

        //A Player belongs to one team
        //One Team has many players
        [ForeignKey("Team")]
        public int TeamID { get; set; }
        public virtual Team Team { get; set; }

        //
        public ICollection<Venue> Venues { get; set; }

    }
    public class PlayerDto
    {
        public int PlayerID { get; set; }
        public string PlayerName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string TeamName { get; set; }
    }
}