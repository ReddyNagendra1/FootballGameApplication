using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using FootballGameApplication.Models;
using System.Diagnostics;

namespace FootballGameApplication.Controllers
{
    public class PlayerDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/PlayerData/ListPlayers
        [HttpGet]
        public IEnumerable<PlayerDto> ListPlayers()
        {
            List<Player> Players = db.Players.Include(p => p.Team).ToList();
            List<PlayerDto> PlayerDtos = new List<PlayerDto>();

            Players.ForEach(p => PlayerDtos.Add(new PlayerDto()
            {
                PlayerID = p.PlayerID,
                PlayerName = p.PlayerName,
                DateOfBirth = p.DateOfBirth,
                TeamName = p.Team.TeamName
            }));

            return PlayerDtos;

        }

        // GET: api/PlayerData/FindAnimal/5
        [ResponseType(typeof(Player))]
        [HttpGet]
        public IHttpActionResult FindPlayer(int id)
        {
            Player Player = db.Players.Find(id);
            PlayerDto PlayerDto = new PlayerDto()
            {
                PlayerID = Player.PlayerID,
                PlayerName = Player.PlayerName,
                DateOfBirth = Player.DateOfBirth,
                TeamName = Player.Team.TeamName
            };
            if (Player == null)
            {
                return NotFound();
            }

            return Ok(PlayerDto);
        }

        // PUT: api/PlayerData/UpdateAnimal/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdatePlayer(int id, Player player)
        {
            Debug.WriteLine("I have reached the update player method!");
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model state is invalid");
                return BadRequest(ModelState);
            }

            if (id != player.PlayerID)
            {
                Debug.WriteLine("ID mismatch");
                Debug.WriteLine("GET parameter"+id);
                Debug.WriteLine("POST parameter" + player.PlayerID);
                return BadRequest();
            }

            db.Entry(player).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerExists(id))
                {
                    Debug.WriteLine("Animal not found");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            Debug.WriteLine("None of the conditions trigerred");
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/PlayerData/AddPlayer
        [ResponseType(typeof(Player))]
        [HttpPost]
        public IHttpActionResult AddPlayer(Player player)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Players.Add(player);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = player.PlayerID }, player);
        }

        // POST: api/PlayerData/DeletePlayer/5
        [ResponseType(typeof(Player))]
        [HttpPost]
        public IHttpActionResult DeletePlayer(int id)
        {
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return NotFound();
            }

            db.Players.Remove(player);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PlayerExists(int id)
        {
            return db.Players.Count(e => e.PlayerID == id) > 0;
        }
    }
}