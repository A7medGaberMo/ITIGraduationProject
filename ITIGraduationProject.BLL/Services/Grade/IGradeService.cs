using ITIGraduationProject.BLL.ViewModels.GradeVM;
using ITIGraduationProject.DAL.Models;
using System.Collections.Generic;

namespace ITIGraduationProject.BLL.Services
{
    public interface IGradeService
    {
        IEnumerable<Grade> GetAll();
        Grade? GetById(int id);
        void Create(GradeCreateVM vm);
        void Update(GradeEditVM vm);
        void Delete(int id);

        
        IEnumerable<Grade> GetPagedGrades(int pageNumber, int pageSize, string? traineeName = null, int? courseId = null);
        int GetGradesCount(string? traineeName = null, int? courseId = null);
    }
}
