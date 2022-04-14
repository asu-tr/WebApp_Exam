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
            // get 20 most recent exams
            Exams = _examDbContext.Exams.OrderBy(e => e.CreatedAt).Take(20).ToList();

            foreach (Exam e in Exams)
            {
                e.Creator = _examDbContext.Users.Find(e.CreatorId);
                e.WiredText = _examDbContext.WiredTexts.Find(e.WiredTextId);
            }
        }
    }
}
