using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiniTwitterFrontEnd.Models;
using MiniTwitterShared;
using System.Diagnostics;

namespace MiniTwitterFrontEnd.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Index(TweetViewInput model)
        {
            if (ModelState.IsValid)
            {

                return View(
                    "Tweets",
                    new UserTweetDisplay(
                        model.UserFile.ReadAsList(),
                        model.TweetFile.ReadAsList()
                    )
                );
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Tweets(UserTweetDisplay model)
        {

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
