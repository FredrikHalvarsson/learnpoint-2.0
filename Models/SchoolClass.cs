using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace learnpoint_2._0.Models
{
    public class SchoolClass
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SchoolClassId { get; set; }
        [Display(Name ="Class")]
        public string ClassTitle { get; set; }
        [ForeignKey("ClassSuperintendent")]
        [Display(Name = "Class superintendent")]
        public int FkTeacherId { get; set; }
        public Teacher? ClassSuperintendent { get; set; }
        public IEnumerable<Student>? Students { get; set; }
        public IEnumerable<ClassCourses>? Courses { get; set; }
    }
}
