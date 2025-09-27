using ITIGraduationProject.BLL.Services;
using ITIGraduationProject.BLL.ViewModels.UserVM;
using ITIGraduationProject.DAL.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace ITIGraduationProject.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index(string? searchTerm, UserRole? role, int pageNumber = 1)
        {
            int pageSize = 4;

            var users = _userService.GetPagedUsers(pageNumber, pageSize, searchTerm, role)
                .Select(u => new UserEditVM
                {
                    Id = u.Id,
                    Name = u.Name,
                    Age = u.Age,
                    Role = u.Role
                }).ToList();

            var totalCount = _userService.GetUsersCount(searchTerm, role);

            ViewBag.SearchTerm = searchTerm;
            ViewBag.SelectedRole = role;
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalCount = totalCount;
            ViewBag.Roles = new SelectList(System.Enum.GetValues(typeof(UserRole)));

            return View(users);
        }

        public IActionResult Details(int id)
        {
            var user = _userService.GetById(id);
            if (user == null) return NotFound();

            return View(user);
        }

        public IActionResult Create()
        {
            ViewBag.Roles = new SelectList(System.Enum.GetValues(typeof(UserRole)));
            return View(new UserCreateVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UserCreateVM vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Roles = new SelectList(System.Enum.GetValues(typeof(UserRole)));
                return View(vm);
            }

            _userService.Create(vm);
            TempData["Success"] = "User created successfully!";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var user = _userService.GetById(id);
            if (user == null) return NotFound();

            var vm = new UserEditVM
            {
                Id = user.Id,
                Name = user.Name,
                Age = user.Age,
                Role = user.Role
            };

            ViewBag.Roles = new SelectList(System.Enum.GetValues(typeof(UserRole)), vm.Role);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UserEditVM vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Roles = new SelectList(System.Enum.GetValues(typeof(UserRole)), vm.Role);
                return View(vm);
            }

            _userService.Update(vm);
            TempData["Success"] = "User updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var user = _userService.GetById(id);
            if (user == null) return NotFound();

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _userService.Delete(id);
            TempData["Success"] = "User deleted successfully!";
            return RedirectToAction(nameof(Index));
        }
    }
}
