using ITIGraduationProject.BLL.Services;
using ITIGraduationProject.BLL.ViewModels;
using ITIGraduationProject.BLL.ViewModels.CourseVM;
using ITIGraduationProject.DAL.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;

namespace ITIGraduationProject.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly IUserService _userService; 

        public CourseController(ICourseService courseService, IUserService userService)
        {
            _courseService = courseService;
            _userService = userService;
        }

        public IActionResult Index(string? searchTerm, string? category, string? instructorName, int pageNumber = 1, int pageSize = 3)
        {
        
            var courses = _courseService.GetPagedCourses(pageNumber, pageSize, searchTerm, category, instructorName)
                                        .Select(c => new CourseListVM
                                        {
                                            Id = c.Id,
                                            Name = c.Name,
                                            Category = c.Category,
                                            InstructorName = c.Instructor?.Name
                                        }).ToList();

            var totalCount = _courseService.GetCoursesCount(searchTerm, category, instructorName);

            
            var categories = _courseService.GetAll()
                                           .Select(c => c.Category)
                                           .Distinct()
                                           .ToList();

            var instructors = _userService.GetAllInstructors()
                                          .Select(i => new SelectListItem
                                          {
                                              Value = i.Name,
                                              Text = i.Name
                                          }).ToList();

            ViewBag.Categories = categories;
            ViewBag.Instructors = instructors;
            ViewBag.SelectedCategory = category;
            ViewBag.SelectedInstructor = instructorName;
            ViewBag.SearchTerm = searchTerm;
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalCount = totalCount;

            return View(courses);
        }



        public IActionResult Create()
        {
            PopulateDropdowns();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CourseCreateVM vm)
        {
            if (!ModelState.IsValid)
            {
                PopulateDropdowns();
                return View(vm);
            }

            _courseService.Create(vm);
            TempData["Success"] = "Course created successfully!";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var course = _courseService.GetById(id);
            if (course == null) return NotFound();

            var vm = new CourseEditVM
            {
                Id = course.Id,
                Name = course.Name,
                Category = course.Category,
                InstructorId = course.InstructorId
            };

            PopulateDropdowns(course.Category, course.InstructorId);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CourseEditVM vm)
        {
            if (!ModelState.IsValid)
            {
                PopulateDropdowns(vm.Category, vm.InstructorId);
                return View(vm);
            }

            _courseService.Update(vm);
            TempData["Success"] = "Course updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _courseService.Delete(id);
            TempData["Success"] = "Course deleted successfully!";
            return RedirectToAction(nameof(Index));
        }

        private void PopulateDropdowns(string? selectedCategory = null, int? selectedInstructorId = null)
        {
            ViewBag.Categories = new SelectList(Enum.GetNames(typeof(CourseCategory)), selectedCategory);
            ViewBag.Instructors = new SelectList(
                _userService.GetAll().Where(u => u.Role == UserRole.Instructor),
                "Id",
                "Name",
                selectedInstructorId
            );
        }
        public IActionResult Details(int id)
        {
            var course = _courseService.GetById(id);
            if (course == null)
            {
                TempData["Error"] = "Course not found.";
                return RedirectToAction(nameof(Index));
            }

            var vm = new CourseListVM
            {
                Id = course.Id,
                Name = course.Name,
                Category = course.Category,
                InstructorName = course.Instructor?.Name
            };

            return View(vm);
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult IsCourseNameUnique(string name, int id)
        {
            if (!_courseService.IsCourseNameUnique(name, id))
                return Json($"Course name '{name}' is already used.");
            return Json(true);
        }
    }
}
