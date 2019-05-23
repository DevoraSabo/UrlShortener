using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Web.Models;
using shortid;
using UrlShortener.Data;
using Microsoft.Extensions.Configuration;

namespace UrlShortener.Web.Controllers
{

    public class HomeController : Controller
    {
        private string _connectionString;

        public HomeController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UrlShortener (string url)
        {
            UrlRepository repo = new UrlRepository(_connectionString);

            UserRepository ur = new UserRepository(_connectionString);
            string hashedUrl = null;
            if (User.Identity.IsAuthenticated)
            {
                int id = ur.GetByEmail(User.Identity.Name).Id;
                hashedUrl = repo.AddUrl(url, id);
            }
            else
            {
                hashedUrl = repo.AddUrl(url);
            }
            

            return Json(hashedUrl);
        }

        [Route("{hashedUrl}")]
        public IActionResult NewUrl(string hashedUrl)
        {
            UrlRepository repo = new UrlRepository(_connectionString);
            string original = repo.GetOriginalUrl(hashedUrl).OriginalUrl;

            return Json(original);
        }


        [Authorize]
        public IActionResult AllUrlsForUser()
        {
            return View();
        }




        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
