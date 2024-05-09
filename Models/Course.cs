using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace learnpoint_2._0.Models
{
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CourseId { get; set; }
        [Display(Name = "Name")]
        [Required]
        public string CourseName { get; set; }
        public IEnumerable<ClassCourses>? Classes { get; set; }
        public IEnumerable<TeacherCourses>? Teachers { get; set; }
    }
}
