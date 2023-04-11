using BingoModels;
using BingoModels.ViewModels;
using BingoServices.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Bingo.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBingoService _bingoService;

        //test auth user date, added to db relation usage because
        //we haven`t on here authorization vs authentication
        //this better move to some base controller method or to smth else 
        private readonly PlayerViewModel userData = new PlayerViewModel { Id = 1, Email = "test@test.com" };

        public HomeController(ILogger<HomeController> logger, IBingoService bingoService)
        {
            _logger = logger;
            _bingoService = bingoService;
        }
        
        public async Task<ActionResult> Index()
        {
            _logger.LogInformation("Creare bingo 52-25 game card");
            var card = await _bingoService.CreateBingoCard(userData);
            return View(card);
        }
        
        public async Task<ActionResult> Play(int id) 
        {
            _logger.LogInformation("Generation next number and check winnnig lines");
            if (id <= 0)
                return RedirectToAction(nameof(Index));
            else
                return View(await _bingoService.GetNext(id));
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
