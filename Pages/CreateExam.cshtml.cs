using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using WebApp_Exam.Models;

namespace WebApp_Exam.Pages
{
    [Authorize]
    public class CreateExamModel : PageModel
    {
        private readonly string url = "https://www.wired.com/";
        private readonly ExamDbContext _examDbContext = new ExamDbContext();

        public List<WiredText> WiredTexts { get; set; } = new List<WiredText>();

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            public string WiredTextTitle { get; set; }

            [Required]
            public string Q1 { get; set; }

            [Required]
            public string A1 { get; set; }

            [Required]
            public string A2 { get; set; }

            [Required]
            public string A3 { get; set; }

            [Required]
            public string A4 { get; set; }

            [Required]
            public int Answer1 { get; set; }

            [Required]
            public string Q2 { get; set; }

            [Required]
            public string A5 { get; set; }

            [Required]
            public string A6 { get; set; }

            [Required]
            public string A7 { get; set; }

            [Required]
            public string A8 { get; set; }

            [Required]
            public int Answer2 { get; set; }

            [Required]
            public string Q3 { get; set; }

            [Required]
            public string A9 { get; set; }

            [Required]
            public string A10 { get; set; }

            [Required]
            public string A11 { get; set; }

            [Required]
            public string A12 { get; set; }

            [Required]
            public int Answer3 { get; set; }

            [Required]
            public string Q4 { get; set; }

            [Required]
            public string A13 { get; set; }

            [Required]
            public string A14 { get; set; }

            [Required]
            public string A15 { get; set; }

            [Required]
            public string A16 { get; set; }

            [Required]
            public int Answer4 { get; set; }
        }

        public void OnGet()
        {
            this.WiredTexts = GetAllTextInfo();
        }

        private List<WiredText> GetAllTextInfo()
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);

            // i get the divs with the class i want
            IList<HtmlNode> nodes = doc.QuerySelectorAll("div .summary-list__items");
            // most recent table is second table on this page so i select it
            HtmlNode recentsNode = nodes[1];
            // for every text i get it's own node
            IList<HtmlNode> recents = recentsNode.QuerySelectorAll("div .SummaryItemWrapper-gdEuvf a .SummaryItemHedLink-cgPsOZ");

            List<WiredText> tempList = new List<WiredText>();

            //getting text details from each node
            foreach (HtmlNode node in recents)
            {
                tempList.Add(new WiredText
                {
                    Title = node.QuerySelector("h2").InnerText,
                    // i'll get the texts later.
                    Text = "",
                    // href value doesn't have the website so i added it manually.
                    TextUrl = url.Substring(0, url.Length - 1) + node.Attributes["href"].Value
                });
            }

            // add texts to WiredTexts from their url
            foreach (WiredText text in tempList)
            {
                HtmlDocument doc2 = web.Load(text.TextUrl);
                HtmlNode node = doc2.QuerySelector("div .BodyWrapper-ctnerm p");

                text.Text = node.InnerHtml;
            }

            return tempList;
        } 

        public IActionResult OnPost()
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            if (ModelState.IsValid)
            {
                // create a new exam
                Exam newExam = new Exam()
                {
                    Creator = _examDbContext.Users.Where(x => x.Username == User.Identity.Name).FirstOrDefault(),
                    WiredText = GetAllTextInfo().Where(x => x.Text == Input.WiredTextTitle).FirstOrDefault(),
                    Questions = new List<Question>()
                    {
                        new Question()
                        {
                            Text = Input.Q1,
                            Answers = new List<Answer>()
                            {
                                new Answer()
                                {
                                    Text = Input.A1,
                                    IsRightAnswer = false
                                },
                                new Answer()
                                {
                                    Text = Input.A2,
                                    IsRightAnswer = false
                                },
                                new Answer()
                                {
                                    Text = Input.A3,
                                    IsRightAnswer = false
                                },
                                new Answer()
                                {
                                    Text = Input.A4,
                                    IsRightAnswer = false
                                }
                            },
                        },
                        new Question()
                        {
                            Text = Input.Q2,
                            Answers = new List<Answer>()
                            {
                                new Answer()
                                {
                                    Text = Input.A5,
                                    IsRightAnswer = false
                                },
                                new Answer()
                                {
                                    Text = Input.A6,
                                    IsRightAnswer = false
                                },
                                new Answer()
                                {
                                    Text = Input.A7,
                                    IsRightAnswer = false
                                },
                                new Answer()
                                {
                                    Text = Input.A8,
                                    IsRightAnswer = false
                                }
                            }
                        },
                        new Question()
                        {
                            Text = Input.Q3,
                            Answers = new List<Answer>()
                            {
                                new Answer()
                                {
                                    Text = Input.A9,
                                    IsRightAnswer = false
                                },
                                new Answer()
                                {
                                    Text = Input.A10,
                                    IsRightAnswer = false
                                },
                                new Answer()
                                {
                                    Text = Input.A11,
                                    IsRightAnswer = false
                                },
                                new Answer()
                                {
                                    Text = Input.A12,
                                    IsRightAnswer = false
                                }
                            }
                        },
                        new Question()
                        {
                            Text = Input.Q4,
                            Answers = new List<Answer>()
                            {
                                new Answer()
                                {
                                    Text = Input.A13,
                                    IsRightAnswer = false
                                },
                                new Answer()
                                {
                                    Text = Input.A14,
                                    IsRightAnswer = false
                                },
                                new Answer()
                                {
                                    Text = Input.A15,
                                    IsRightAnswer = false
                                },
                                new Answer()
                                {
                                    Text = Input.A16,
                                    IsRightAnswer = false
                                }
                            }
                        }
                    }
                };

                // add right answers
                int[] rightAnswers = { Input.Answer1, Input.Answer2, Input.Answer3, Input.Answer4 };
                for (int i = 0; i < 4; i++)
                {
                    newExam.Questions[i].Answers[rightAnswers[i] - 1].IsRightAnswer = true;
                }

                _examDbContext.Exams.Add(newExam);
                _examDbContext.SaveChanges();

                return RedirectToPage("Home");
            }

            return Page();
        }
    }
}
