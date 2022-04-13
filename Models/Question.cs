using System.ComponentModel.DataAnnotations;

namespace WebApp_Exam.Models
{
    public class Question
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        
        public virtual Exam RelatedExam { get; set; }
        public virtual List<Answer> Answers { get; set; }
    }
}