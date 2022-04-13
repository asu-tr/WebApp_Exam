using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using WebApp_Exam.Models;

namespace WebApp_Exam.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ExamDbContext _examDbContext = new ExamDbContext();

        [BindProperty]
        public InputModel Input { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var user = _examDbContext.Users.Where(x => x.Username == Input.Username && x.Password == Input.Password).FirstOrDefault();

                if (user != null)
                {
                    // save user to seesssion aetc
                    return RedirectToPage("Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        public class InputModel
        {
            [Required]
            public string Username { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }
    }
}
