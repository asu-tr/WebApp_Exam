using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using WebApp_Exam.Models;

namespace WebApp_Exam.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly ExamDbContext _examDbContext = new ExamDbContext();

        [BindProperty]
        public InputModel Input { get; set; }


        public void OnGet()
        {
        }

        public class InputModel
        {
            [Required]
            public string Username { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm Password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var existingUser = _examDbContext.Users.Where(x => x.Username == Input.Username).FirstOrDefault();

                if (existingUser == null)
                {
                    var user = new User()
                    {
                        Username = Input.Username,
                        Password = Input.Password
                    };

                    _examDbContext.Users.Add(user);
                    _examDbContext.SaveChanges();

                    return RedirectToPage("Login");
                }

                return Page();
            }

            return Page();
        }
    }
}