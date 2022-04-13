using System.ComponentModel.DataAnnotations;

namespace WebApp_Exam.Models
{
    public class WiredText
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public string TextUrl { get; set; }
    }
}