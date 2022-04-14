using System.ComponentModel.DataAnnotations;

namespace WebApp_Exam.Models
{
    public class Answer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }

        [Required]
        public bool IsRightAnswer { get; set; }

        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }
    }
}
