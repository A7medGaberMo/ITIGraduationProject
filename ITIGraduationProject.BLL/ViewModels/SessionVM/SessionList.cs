using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.BLL.ViewModels.SessionVM
{
    public class SessionListVM
    {
        public int Id { get; set; }
        public string CourseName { get; set; } = "N/A";
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
