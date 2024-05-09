using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace learnpoint_2._0.Models
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentId { get; set; }
        [Display(Name = "Name")]
        [Required]
        public string StudentName { get; set; }
        [ForeignKey("SchoolClass")]
        [Display(Name ="Class")]
        [Required]
        public int FKSchoolClassId { get; set; }
        public SchoolClass? SchoolClass { get; set; }
        public IEnumerable<StudentGrade>? Grades { get; set; }
    }
}
