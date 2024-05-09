using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace learnpoint_2._0.Models
{
    public class Teacher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeacherId { get; set; }
        [Display(Name = "Name")]
        [Required]
        public string TeacherName { get; set; }
        public virtual ICollection<Course>? Courses { get; set; }
        public ICollection<StudentGrade>? Grades { get; set; }
    }
}
