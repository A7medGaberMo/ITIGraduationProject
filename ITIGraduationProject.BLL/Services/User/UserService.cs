
using ITIGraduationProject.BLL.ViewModels.UserVM;
using ITIGraduationProject.DAL.Models;
using ITIGraduationProject.DAL.Models.Enums;
using ITIGraduationProject.DAL.Repository.UnitofWork;
using System.Collections.Generic;
using System.Linq;

namespace ITIGraduationProject.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<User> GetAll()
        {
            return _unitOfWork.Users.GetAll();
        }

        public User? GetById(int id)
        {
            return _unitOfWork.Users.GetById(id);
        }

        public void Create(UserCreateVM vm)
        {
            var user = new User
            {
                Name = vm.Name,
                Age = vm.Age,
                Role = vm.Role
            };

            _unitOfWork.Users.Add(user);
            _unitOfWork.Complete();
        }

        public void Update(UserEditVM vm)
        {
            var user = _unitOfWork.Users.GetById(vm.Id);
            if (user == null) return;

            user.Name = vm.Name;
            user.Age = vm.Age;
            user.Role = vm.Role;

            _unitOfWork.Users.Update(user);
            _unitOfWork.Complete();
        }

        public void Delete(int id)
        {
            var user = _unitOfWork.Users.GetById(id);
            if (user == null) return;

            _unitOfWork.Users.Remove(user);
            _unitOfWork.Complete();
        }

        // Search + Filter + Pagination
        public IEnumerable<User> GetPagedUsers(int pageNumber, int pageSize, string? searchTerm = null, UserRole? role = null)
        {
            var query = _unitOfWork.Users.GetAll().AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(u => u.Name.Contains(searchTerm));
            }

            if (role.HasValue)
            {
                query = query.Where(u => u.Role == role.Value);
            }

            return query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public int GetUsersCount(string? searchTerm = null, UserRole? role = null)
        {
            var query = _unitOfWork.Users.GetAll().AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(u => u.Name.Contains(searchTerm));
            }

            if (role.HasValue)
            {
                query = query.Where(u => u.Role == role.Value);
            }

            return query.Count();
        }

       
        public IEnumerable<User> GetAllInstructors()
        {
            return _unitOfWork.Users.GetAll().Where(u => u.Role == UserRole.Instructor);
        }

        public IEnumerable<User> GetAllTrainees()
        {
            return _unitOfWork.Users.GetAll().Where(u => u.Role == UserRole.Trainee);
        }


 
       
    }
}
