using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace learnpoint_2._0.Models
{
    public class TeacherCourses
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeacherCoursesId { get; set; }
        [ForeignKey("Teacher")]
        public int FKTeacherId { get; set; }
        public Teacher? Teacher { get; set; }
        [ForeignKey("Course")]
        public int FKCourseId { get; set; }
        public Course? Course { get; set; }
    }
}
