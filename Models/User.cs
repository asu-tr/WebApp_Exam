using System.ComponentModel.DataAnnotations;

namespace WebApp_Exam.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        public virtual List<Exam> CreatedExams { get; set; }
    }
}