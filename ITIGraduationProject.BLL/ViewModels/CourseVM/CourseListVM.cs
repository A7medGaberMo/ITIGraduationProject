namespace ITIGraduationProject.BLL.ViewModels
{
    public class CourseListVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string? InstructorName { get; set; } = string.Empty;
    }
}
