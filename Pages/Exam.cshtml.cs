using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp_Exam.Models;

namespace WebApp_Exam.Pages
{
    public class ExamModel : PageModel
    {
        public int Id { get; set; }
        public Exam currentExam { get; set; }

        private readonly ExamDbContext _examDbContext = new ExamDbContext();

        public void OnGet(int examId)
        {
            Id = examId;

            currentExam = _examDbContext.Exams.Find(examId);

            if (currentExam != null)
            {
                currentExam.WiredText = _examDbContext.WiredTexts.Find(currentExam.WiredTextId);

                currentExam.Questions = _examDbContext.Questions.Where(x => x.RelatedExamId == currentExam.Id).ToList();

                foreach (Question q in currentExam.Questions)
                {
                    q.Answers = _examDbContext.Answers.Where(x => x.QuestionId == q.Id).ToList();
                }
            }
        }

    }
}