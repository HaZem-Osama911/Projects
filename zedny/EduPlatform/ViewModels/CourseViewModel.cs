using System.ComponentModel.DataAnnotations;

namespace EduPlatform.ViewModels
{
    public class AddCourseViewModel
    {
        [Required(ErrorMessage = "Course name is required")]
        [MaxLength(200, ErrorMessage = "Name cannot exceed 200 characters")]
        [Display(Name = "Course Name")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required")]
        [MaxLength(2000, ErrorMessage = "Description cannot exceed 2000 characters")]
        [Display(Name = "Course Description")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Price is required")]
        [Range(0, 100000, ErrorMessage = "Price must be between 0 and 100,000")]
        [Display(Name = "Course Price")]
        public decimal Price { get; set; }
    }

    public class EditCourseViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Course name is required")]
        [MaxLength(200, ErrorMessage = "Name cannot exceed 200 characters")]
        [Display(Name = "Course Name")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required")]
        [MaxLength(2000, ErrorMessage = "Description cannot exceed 2000 characters")]
        [Display(Name = "Course Description")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Price is required")]
        [Range(0, 100000, ErrorMessage = "Price must be between 0 and 100,000")]
        [Display(Name = "Course Price")]
        public decimal Price { get; set; }
    }
}