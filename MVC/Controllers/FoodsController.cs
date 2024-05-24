#nullable disable
using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.Controllers.Bases;


namespace MVC.Controllers
{
    [Authorize]
    public class FoodsController : MvcControllerBase
    {

        private readonly IFoodservice _Foodservice;
        private readonly IChefservice _Chefservice;
        private readonly IUserService _userService;

		public FoodsController(IFoodservice Foodservice, IChefservice Chefservice, IUserService userService)
		{
			_Foodservice = Foodservice;
			_Chefservice = Chefservice;
			_userService = userService;
		}

		public IActionResult Index()
        {
            List<FoodModel> gameList = _Foodservice.GetList();
            return View("List", gameList);
        }

        public IActionResult Details(int id)
        {
            FoodModel game = _Foodservice.GetItem(id); 
            if (game == null)
            {

                return View("Error", $"Food with ID {id} not found!");
            }
            return View(game);
        }


        [Authorize(Roles = "user")]
        public IActionResult Create()
        {

            ViewData["ChefId"] = new SelectList(_Chefservice.Query().ToList(), "Id", "Name");
            ViewBag.Users = new MultiSelectList(_userService.GetList(), "Id", "UserName");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "user")]
        public IActionResult Create(FoodModel game)
        {
            if (ModelState.IsValid)
            {

                var result = _Foodservice.Add(game);
                if (result.IsSuccessful)
                    return RedirectToAction(nameof(Details), new { id = game.Id });
            }

            ViewData["ChefId"] = new SelectList(_Chefservice.Query().ToList(), "Id", "Name");
            ViewBag.Users = new MultiSelectList(_userService.GetList(), "Id", "UserName");
            return View(game);
        }


        [Authorize(Roles = "user")]
        public IActionResult Edit(int id)
        {
            FoodModel game = _Foodservice.GetItem(id); 
            if (game == null)
            {
                return View("Error", $"Food with ID {id} not found!");
            }
   
            ViewData["ChefId"] = new SelectList(_Chefservice.Query().ToList(), "Id", "Name");
            ViewBag.Users = new MultiSelectList(_userService.GetList(), "Id", "UserName");
            return View(game);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "user")]
        public IActionResult Edit(FoodModel game)
        {
            if (ModelState.IsValid)
            {

                var result = _Foodservice.Update(game);
                if (result.IsSuccessful)
					return RedirectToAction(nameof(Details), new { id = game.Id });
			}

			ViewData["ChefId"] = new SelectList(_Chefservice.Query().ToList(), "Id", "Name");
			ViewBag.Users = new MultiSelectList(_userService.GetList(), "Id", "UserName");
			return View(game);
        }


        [Authorize(Roles = "user")]
        public IActionResult Delete(int id)
        {
            var result = _Foodservice.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
