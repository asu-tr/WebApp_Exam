using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp_Exam.Models;

namespace WebApp_Exam.Pages
{
    public class DeleteExamModel : PageModel
    {

        private readonly ExamDbContext _examDbContext = new ExamDbContext();

        public void OnGet(int examId)
        {
            // delete exam
            Exam e = _examDbContext.Exams.Find(examId);

            if (e != null)
            {
                e.Questions = _examDbContext.Questions.Where(x => x.RelatedExamId == e.Id).ToList();

                foreach (Question q in e.Questions)
                {
                    q.Answers = _examDbContext.Answers.Where(x => x.QuestionId == q.Id).ToList();
                }

                foreach (Question q in e.Questions)
                {
                    foreach (Answer a in q.Answers)
                    {
                        // delete all answers
                        _examDbContext.Answers.Remove(a);
                    }
                    // delete all questions
                    _examDbContext.Questions.Remove(q);
                }

                _examDbContext.Exams.Remove(e);

                _examDbContext.SaveChanges();
            }
        }
    }
}