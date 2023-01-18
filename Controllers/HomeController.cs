using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Text.Json;
using WebApplication2.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Reflection.Metadata;



namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserService _userService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, UserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

         public async void addocs()
          {
            string ip = Response.HttpContext.Connection.RemoteIpAddress.ToString();


            var document = new BsonDocument { { "_id", DateTime.Now.ToString() }
                  , { "ipaddress",ip } };

            await _userService.Addocs(document);
          } 
        public IActionResult Index()
        {
            addocs();       
            return View();
        }

        public async Task<IActionResult> Privacy()
        {
            var dbList = await _userService.Get();
            BsonDocument t = new BsonDocument();
            foreach (var item in dbList)
            {
                t = item;
            }

            ViewData["ip"] = "Date : " + t["_id"] + " Ip Address : " + t["ipaddress"];
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
