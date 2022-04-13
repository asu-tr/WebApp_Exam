using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp_Exam.Models;

namespace WebApp_Exam.Pages
{
    [Authorize]
    public class HomeModel : PageModel
    {
        private readonly ExamDbContext _examDbContext = new ExamDbContext();
        public List<Exam> Exams { get; set; } = new List<Exam>();

        public void OnGet()
        {
            Exams.Clear();
            // get 20 most recent exams
            Exams = _examDbContext.Exams.OrderBy(e => e.CreatedAt).Take(20).ToList();
        }
    }
}
