using System.ComponentModel.DataAnnotations;

namespace WebApp_Exam.Models
{
    public class Exam
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;


        public virtual User Creator { get; set; }
        public virtual WiredText WiredText { get; set; }

        public virtual List<Question> Questions { get; set; }
    }
}