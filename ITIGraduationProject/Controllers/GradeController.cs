using ITIGraduationProject.BLL.Services;
using ITIGraduationProject.BLL.ViewModels.GradeVM;
using ITIGraduationProject.BLL.ViewModels.SessionVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;

namespace ITIGraduationProject.PL.Controllers
{
    public class GradeController : Controller
    {
        private readonly IGradeService _gradeService;
        private readonly IUserService _userService;
        private readonly ISessionService _sessionService;

        public GradeController(IGradeService gradeService, IUserService userService, ISessionService sessionService)
        {
            _gradeService = gradeService;
            _userService = userService;
            _sessionService = sessionService;
        }

        public IActionResult Index(int pageNumber = 1, int pageSize = 5, string? traineeName = null, int? courseId = null)
        {
            var grades = _gradeService.GetPagedGrades(pageNumber, pageSize, traineeName, courseId);
            var totalCount = _gradeService.GetGradesCount(traineeName, courseId);

            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalCount = totalCount;
            ViewBag.TraineeName = traineeName ?? "";
            ViewBag.CourseId = courseId ?? 0;

            // ✅ Use the new service method to build dropdown
            var courses = _sessionService.GetAllCourses()
                                         .Select(c => new { c.CourseId, c.CourseName })
                                         .ToList();

            ViewBag.Courses = new SelectList(courses, "CourseId", "CourseName", courseId);

            ViewBag.TotalPages = pageSize > 0 ? (int)Math.Ceiling((double)totalCount / pageSize) : 1;

            return View(grades);
        }


        public IActionResult Create()
        {
            ViewBag.Trainees = new SelectList(_userService.GetAll().Where(u => u.Role.ToString() == "Trainee"), "Id", "Name");
            ViewBag.Sessions = new SelectList(_sessionService.GetAll(), "Id", "CourseName");
            return View();
        }

        [HttpPost]
        public IActionResult Create(GradeCreateVM vm)
        {
            if (ModelState.IsValid)
            {
                _gradeService.Create(vm);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Trainees = new SelectList(_userService.GetAll().Where(u => u.Role.ToString() == "Trainee"), "Id", "Name", vm.TraineeId);
            ViewBag.Sessions = new SelectList(_sessionService.GetAll(), "Id", "CourseName", vm.SessionId);
            return View(vm);
        }

        public IActionResult Edit(int id)
        {
            var grade = _gradeService.GetById(id);
            if (grade == null) return NotFound();

            var vm = new GradeEditVM
            {
                Id = grade.Id,
                TraineeId = grade.TraineeId,
                SessionId = grade.SessionId,
                Value = grade.Value
            };

            ViewBag.Trainees = new SelectList(_userService.GetAll().Where(u => u.Role.ToString() == "Trainee"), "Id", "Name", vm.TraineeId);
            ViewBag.Sessions = new SelectList(_sessionService.GetAll(), "Id", "CourseName", vm.SessionId);
            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(GradeEditVM vm)
        {
            if (ModelState.IsValid)
            {
                _gradeService.Update(vm);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Trainees = new SelectList(_userService.GetAll().Where(u => u.Role.ToString() == "Trainee"), "Id", "Name", vm.TraineeId);
            ViewBag.Sessions = new SelectList(_sessionService.GetAll(), "Id", "CourseName", vm.SessionId);
            return View(vm);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _gradeService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
