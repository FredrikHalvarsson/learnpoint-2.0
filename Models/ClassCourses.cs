using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace learnpoint_2._0.Models
{
    public class ClassCourses
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClassCourseId { get; set; }
        [ForeignKey("SchoolClass")]
        public int FKSchoolClassId { get; set; }
        public SchoolClass? SchoolClass { get; set; }
        [ForeignKey("Course")]
        public int FKCourseId { get; set; }
        public Course? Course { get; set; }
    }
}
