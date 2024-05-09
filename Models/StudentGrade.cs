using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace learnpoint_2._0.Models
{
    public enum Grade
    {
        A, B, C, D, E, F
    }
    public class StudentGrade
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentGradeId { get; set; }
        public Grade? Grade { get; set; }
        [ForeignKey("Course")]
        public int FkCourseId { get; set; }
        public Course? Course { get; set; }
        [ForeignKey("Student")]
        public int FkStudentId { get; set; }
        public Student? Student { get; set; }
        [ForeignKey("GradingTeacher")]
        public int FkTeacherId { get; set; }
        public Teacher? GradingTeacher { get; set; }
    }
}
