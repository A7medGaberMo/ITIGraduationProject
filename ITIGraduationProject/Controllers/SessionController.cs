using ITIGraduationProject.BLL.Services;
using ITIGraduationProject.BLL.ViewModels.SessionVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ITIGraduationProject.PL.Controllers
{
    public class SessionController : Controller
    {
        private readonly ISessionService _sessionService;
        private readonly ICourseService _courseService;

        public SessionController(ISessionService sessionService, ICourseService courseService)
        {
            _sessionService = sessionService;
            _courseService = courseService;
        }

        public IActionResult Index(int pageNumber = 1, int pageSize = 3, string? courseName = null, string? search = null)
        {
            var sessions = _sessionService.GetPagedSessions(pageNumber, pageSize, courseName, search);
            var totalCount = _sessionService.GetSessionsCount(courseName, search);

            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalCount = totalCount;
            ViewBag.SelectedCourseName = courseName;
            ViewBag.Search = search;
            ViewBag.Courses = _courseService.GetAll();

            return View(sessions);
        }

        public IActionResult Create()
        {
            ViewBag.Courses = new SelectList(_courseService.GetAll(), "Id", "Name");
            return View(new SessionCreateVM());
        }
        [HttpPost]
        public IActionResult Create(SessionCreateVM vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Courses = new SelectList(_courseService.GetAll(), "Id", "Name", vm.CourseId);
                return View(vm);
            }

            try
            {
                _sessionService.Create(vm);
                TempData["Success"] = "Session created successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                ViewBag.Courses = new SelectList(_courseService.GetAll(), "Id", "Name", vm.CourseId);
                return View(vm);
            }
        }

        public IActionResult Edit(int id)
        {
            var vm = _sessionService.GetById(id);
            if (vm == null) return NotFound();

            ViewBag.Courses = _courseService.GetAll();
            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(SessionEditVM vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Courses = _courseService.GetAll();
                return View(vm);
            }

            try
            {
                _sessionService.Update(vm);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                ViewBag.Courses = _courseService.GetAll();
                return View(vm);
            }
        }

        public IActionResult Delete(int id)
        {
            _sessionService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
