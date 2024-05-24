#nullable disable
using Business.Models;
using Business.Services;
using DataAccess.Results.Bases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Controllers.Bases;

//Generated from Custom Template.
namespace MVC.Controllers
{
	// Authorization Way 2:
	[Authorize(Roles = "admin")]
    public class RolesController : MvcControllerBase
    {
        // TODO: Add service injections here
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        // GET: Roles
        // Authorization Way 1:
        //[Authorize(Roles = "admin")]
        public IActionResult Index()
        {
            List<RoleModel> roleList = _roleService.Query().ToList(); // TODO: Add get collection service logic here
            return View(roleList);
        }

		// GET: Roles/Details/5
		// Authorization Way 1:
		//[Authorize(Roles = "admin")]
		public IActionResult Details(int id)
        {
            // Way 1:
            //RoleModel role = _roleService.Query().Where(r => r.Id == id).SingleOrDefault(); // TODO: Add get item service logic here
            // Way 2:
            //RoleModel role = _roleService.Query().FirstOrDefault(r => r.Id == id);
            // Way 3:
            //RoleModel role = _roleService.Query().LastOrDefault(r => r.Id == id);
            // Way 4:
            RoleModel role = _roleService.Query().SingleOrDefault(r => r.Id == id);

            //if (role == null)
            if (role is null)
            {
                return NotFound(); // 404 HTTP Status Code
            }
            return View("RoleDetails", role);
        }

		// GET: Roles/Create
		// Authorization Way 1:
		//[Authorize(Roles = "admin")]
		public IActionResult Create()
        {
            // TODO: Add get related items service logic here to set ViewData if necessary
            return View();
        }

        // POST: Roles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		// Authorization Way 1:
		//[Authorize(Roles = "admin")]
		public IActionResult Create(RoleModel role)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add insert service logic here
                Result result = _roleService.Add(role); // polymorphism
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message; // if redirect to another action, use TempData
                    return RedirectToAction(nameof(Index));
                }
                // Carrying data to the view way 1:
                //ViewData["CreateMessage"] = result.Message; // if returning the related view, use ViewData or ViewBag
                // Carrying data to the view way 2:
                //ViewBag.CreateMessage = result.Message;
                // Carrying data to the view way 3:
                ModelState.AddModelError("", result.Message);
            }
            // TODO: Add get related items service logic here to set ViewData if necessary
            return View(role);
        }

		// GET: Roles/Edit/5
		// Authorization Way 1:
		//[Authorize(Roles = "admin")]
		public IActionResult Edit(int id)
        {
            RoleModel role = _roleService.Query().SingleOrDefault(r => r.Id == id); // TODO: Add get item service logic here
            if (role == null)
            {
                return NotFound();
            }
            // TODO: Add get related items service logic here to set ViewData if necessary
            return View(role);
        }

        // POST: Roles/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		// Authorization Way 1:
		//[Authorize(Roles = "admin")]
		public IActionResult Edit(RoleModel role)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add update service logic here
                Result result = _roleService.Update(role);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", result.Message);
            }
            // TODO: Add get related items service logic here to set ViewData if necessary
            return View(role);
        }

		// GET: Roles/Delete/5
		// Authorization Way 1:
		//[Authorize(Roles = "admin")]
		public IActionResult Delete(int id)
        {
            RoleModel role = _roleService.Query().SingleOrDefault(r => r.Id == id); // TODO: Add get item service logic here
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        // POST: Roles/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
		// Authorization Way 1:
		//[Authorize(Roles = "admin")]
		public IActionResult DeleteConfirmed(int id)
        {
            // TODO: Add delete service logic here
            Result result = _roleService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
