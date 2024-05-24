using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MVC.Controllers
{
    [Authorize]
    public class RecommendationsController : Controller
    {
        private const string _SESSIONKEY = "Recommendationssessionkey";

        private readonly IFoodservice _Foodservice;

        public RecommendationsController(IFoodservice Foodservice)
        {
            _Foodservice = Foodservice;
        }

        public IActionResult Index()
        {
            var Recommendations = GetSession();
            return View(Recommendations);
        }

        public IActionResult Add(int gameId)
        {
            var Recommendations = GetSession();
            var game = _Foodservice.GetItem(gameId);
            var favorite = new FavoriteModel()
            {
                GameId = game.Id,
                GameName = game.Name,
                SalesPrice = game.SalesPrice,
                SalesPriceOutput = game.SalesPriceOutput,
                UserName = User.Identity.Name
            };
            if (!Recommendations.Any(f => f.GameId == favorite.GameId))
                Recommendations.Add(favorite);
            SetSession(Recommendations);
            return RedirectToAction("Index", "Foods");
        }

        public IActionResult Clear()
        {
            var Recommendations = GetSession();
            Recommendations.RemoveAll(f => f.UserName == User.Identity.Name);
            SetSession(Recommendations);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int gameId)
        {
            var Recommendations = GetSession();
            Recommendations.RemoveAll(f => f.GameId == gameId);
            SetSession(Recommendations);
            return RedirectToAction(nameof(Index));
        }

        private List<FavoriteModel> GetSession()
        {
            var Recommendations = new List<FavoriteModel>();
            var json = HttpContext.Session.GetString(_SESSIONKEY);
            if (!string.IsNullOrWhiteSpace(json))
                Recommendations = JsonConvert.DeserializeObject<List<FavoriteModel>>(json);
            return Recommendations;
        }

        private void SetSession(List<FavoriteModel> Recommendations)
        {
            var json = JsonConvert.SerializeObject(Recommendations);
            HttpContext.Session.SetString(_SESSIONKEY, json);
        }
    }
}
