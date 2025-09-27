using ITIGraduationProject.BLL.ViewModels.UserVM;
using ITIGraduationProject.DAL.Models;
using ITIGraduationProject.DAL.Models.Enums;
using System.Collections.Generic;

namespace ITIGraduationProject.BLL.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();
        User? GetById(int id);

        void Create(UserCreateVM vm);
        void Update(UserEditVM vm);
        void Delete(int id);

        
        IEnumerable<User> GetPagedUsers(int pageNumber, int pageSize, string? searchTerm = null, UserRole? role = null);
        int GetUsersCount(string? searchTerm = null, UserRole? role = null);

       
        
      
        IEnumerable<User> GetAllInstructors();
        IEnumerable<User> GetAllTrainees();
    }
}
