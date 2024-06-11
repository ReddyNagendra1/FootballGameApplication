using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Diagnostics;
using FootballGameApplication.Models;
using System.Web.Script.Serialization;


namespace FootballGameApplication.Controllers
{
    public class PlayerController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        static PlayerController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44334/api/playerdata/");
        }
        // GET: Player
        public ActionResult List()
        {
            // curl https://localhost:44334/api/playerdata/listplayers 

            string url = "listplayers";
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is");
            Debug.WriteLine(response.StatusCode);

            IEnumerable<PlayerDto> players = response.Content.ReadAsAsync<IEnumerable<PlayerDto>>().Result;
            Debug.WriteLine("Number of animals received : ");
            Debug.WriteLine(players.Count());

            return View(players);
        }

        // GET: Player/Details/5
        public ActionResult Details(int id)
        {
            // curl https://localhost:44334/api/playerdata/findplayer/{id}

            string url = "findplayer/"+id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is");
            Debug.WriteLine(response.StatusCode);

            PlayerDto selectedplayer = response.Content.ReadAsAsync<PlayerDto>().Result;
            Debug.WriteLine("player received : ");
            Debug.WriteLine(selectedplayer.PlayerName);

            return View(selectedplayer);
        }

        // GET: Player/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Player/Create
        [HttpPost]
        public ActionResult Create(Player player)
        {
            Debug.WriteLine("the json payload is :");
            //Debug.WriteLine("The inputed player name is :");
            //Debug.WriteLine(player.PlayerName);
            
            string url = "addplayer";

            
            string jsonplayload = jss.Serialize(player);

            Debug.WriteLine(jsonplayload);

            HttpContent content = new StringContent(jsonplayload);
            content.Headers.ContentType.MediaType = "application/json";
            
            client.PostAsync(url, content);



            return RedirectToAction("List");

        }

        // GET: Player/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Player/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Player/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Player/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
