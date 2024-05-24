#nullable disable
using Business.Models;
using Business.Services;
using DataAccess.Results.Bases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Controllers.Bases;

namespace MVC.Controllers
{
    [Authorize(Roles = "admin")]
    public class ChefsController : MvcControllerBase
    {
        private readonly IChefservice _Chefservice;

        public ChefsController(IChefservice Chefservice)
        {
            _Chefservice = Chefservice;
        }

        public IActionResult Index()
        {
            List<ChefModel> ChefList = _Chefservice.Query().OrderBy(p => p.Name).ToList(); 
            return View(ChefList);
        }

        public IActionResult Details(int id)
        {
            ChefModel Chef = _Chefservice.Query().SingleOrDefault(p => p.Id == id); 
            if (Chef == null)
            {
                return NotFound();
            }
            return View(Chef);
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ChefModel Chef)
        {
            if (ModelState.IsValid)
            {

                Result result = _Chefservice.Add(Chef);
                if (result.IsSuccessful)
                    return RedirectToAction(nameof(Index));
                ModelState.AddModelError("", result.Message);
            }
            return View(Chef);
        }


        public IActionResult Edit(int id)
        {
            ChefModel Chef = _Chefservice.Query().SingleOrDefault(p => p.Id == id); 
            if (Chef == null)
            {
                return NotFound();
            }
            return View(Chef);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ChefModel Chef)
        {
            if (ModelState.IsValid)
            {

                Result result = _Chefservice.Update(Chef);
                if (result.IsSuccessful)
                    return RedirectToAction(nameof(Index));
                ModelState.AddModelError("", result.Message);
            }

            return View(Chef);
        }

        public IActionResult Delete(int id)
        {
            ChefModel Chef = _Chefservice.Query().SingleOrDefault(p => p.Id == id); 
            if (Chef == null)
            {
                return NotFound();
            }
            return View(Chef);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {

            Result result = _Chefservice.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
