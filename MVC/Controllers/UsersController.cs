#nullable disable
using Business.Models;
using Business.Services;
using DataAccess.Results.Bases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.Controllers.Bases;

//Generated from Custom Template.
namespace MVC.Controllers
{
    // Way 1:
    //[Authorize(Roles = "admin,user")]
    // Way 2:
    [Authorize]
    public class UsersController : MvcControllerBase
    {
        // TODO: Add service injections here
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public UsersController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        // GET: Users
        [AllowAnonymous]
        public IActionResult Index()
        {
            List<UserModel> userList = _userService.GetList(); // TODO: Add get collection service logic here
            return View(userList);
        }

        // GET: Users/Details/5
        public IActionResult Details(int id)
        {
            UserModel user = _userService.GetItem(id); // TODO: Add get item service logic here
            if (user == null)
            {
                return NotFound();
                // TODO: Error View
            }
            return View(user);
            // TODO: _Details Partial View
        }

        // GET: Users/Create
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            // TODO: Add get related items service logic here to set ViewData if necessary
            ViewData["RoleId"] = new SelectList(_roleService.Query().ToList(), "Id", "Name");
            // TODO: Foods

            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public IActionResult Create(UserModel user)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add insert service logic here
                Result result = _userService.Add(user);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
					return RedirectToAction(nameof(Details), new { id = user.Id });
				}
                ModelState.AddModelError("", result.Message);
            }

			// TODO: Add get related items service logic here to set ViewData if necessary
			ViewData["RoleId"] = new SelectList(_roleService.Query().ToList(), "Id", "Name");
			// TODO: Foods

			return View(user);
        }

        // GET: Users/Edit/5
        [Authorize(Roles = "admin")]
        public IActionResult Edit(int id)
        {
            UserModel user = _userService.GetItem(id); // TODO: Add get item service logic here
            if (user == null)
            {
                return NotFound();
                // TODO: Error View
            }

            // TODO: Add get related items service logic here to set ViewData if necessary
            ViewData["RoleId"] = new SelectList(_roleService.Query().ToList(), "Id", "Name");
            // TODO: Foods

            return View(user);
        }

        // POST: Users/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public IActionResult Edit(UserModel user)
        {
			if (ModelState.IsValid)
			{
				// TODO: Add update service logic here
				Result result = _userService.Update(user);
				if (result.IsSuccessful)
				{
					TempData["Message"] = result.Message;
					return RedirectToAction(nameof(Details), new { id = user.Id });
				}
				ModelState.AddModelError("", result.Message);
			}

			// TODO: Add get related items service logic here to set ViewData if necessary
			ViewData["RoleId"] = new SelectList(_roleService.Query().ToList(), "Id", "Name");
			// TODO: Foods

			return View(user);
		}

        // GET: Users/Delete/5
        // Way 2:
        [Authorize(Roles = "admin")]
        public IActionResult Delete(int id)
        {
            // Way 1: better way is to use Authorize attribute
            //if (!User.IsInRole("admin"))
            //    return RedirectToAction("Login", "Home", new { area = "Account" });

            UserModel user = _userService.GetItem(id); // TODO: Add get item service logic here
            if (user == null)
            {
                return NotFound();
				// TODO: Error View
			}
			return View(user);
			// TODO: _Details Partial View
		}

		// POST: Users/Delete
		[HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteConfirmed(int id)
        {
            // TODO: Add delete service logic here
            Result result = _userService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
