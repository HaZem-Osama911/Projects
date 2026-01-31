using System.Collections.Generic;

namespace EduPlatform.Models
{
    public class TeacherWithStudentsViewModel
    {
        public ApplicationUser Teacher { get; set; } = new ApplicationUser();
        public List<ApplicationUser> Students { get; set; } = new List<ApplicationUser>();
    }
}
