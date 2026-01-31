namespace EduPlatform.ViewModels
{
    public class TeacherCardVM
    {
        public string Id { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public int CoursesCount { get; set; }
    }

}
