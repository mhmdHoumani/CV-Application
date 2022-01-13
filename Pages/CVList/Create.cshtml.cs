using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CVApplication.Model;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using CVApplication.ViewModels;

namespace CVApplication.Pages.CVList
{
    public class CreateModel : PageModel
    {

        private readonly IWebHostEnvironment _webhost;
        private readonly IPersonRepository personRepository;

        [BindProperty]
        public CreateViewModel Model { get; set; }

        public CreateModel(IWebHostEnvironment webhost, IPersonRepository personRepository)
        {
            _webhost = webhost;
            this.personRepository = personRepository;
            Model = new CreateViewModel();
        }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            int sum = Model.Num1 + Model.Num2;
            if (Model.VerificationResult != sum)
            {
                ModelState.AddModelError(Model.VerificationResult.ToString(), "Incorrect result, try again.");
            }
            if (ModelState.IsValid)
            {
                string uniqueName = ProcessUploadPhoto(Model);
                if (("1_1_1").Equals(uniqueName))
                {
                    return Page();
                }
                string nationalities = "";
                string skills = "";
                for (int i = 0; i < Model.Nationality.Count; i++)
                {
                    nationalities += Model.Nationality[i];
                    if (i < Model.Nationality.Count - 1)
                    {
                        nationalities += "@#$";
                    }
                }
                for (int i = 0; i < Model.Skills.Count; i++)
                {
                    skills += Model.Skills[i];
                    if(i < Model.Skills.Count - 1)
                    {
                        skills += "@#$";
                    }
                }
                Person person = new()
                {
                    FirstName = Model.FirstName,
                    LastName = Model.LastName,
                    Gender = Model.Gender,
                    Email = Model.Email,
                    BirthDate = Model.BirthDate,
                    Nationality = nationalities,
                    Skills = skills,
                    Password = Model.Password,
                    PhotoPath = uniqueName
                };
                personRepository.AddPerson(person);
                return RedirectToPage("Details", new { id = person.Id });
            }
            return Page();
        }

        private string ProcessUploadPhoto(CreateViewModel model)
        {
            string uniqueName = null;
            if (model.Image != null)
            {
                string imgExt = Path.GetExtension(model.Image.FileName);
                if (imgExt.Equals(".jpg") || imgExt.Equals(".png"))
                {
                    string uploadFolder = Path.Combine(_webhost.WebRootPath, "images");
                    uniqueName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                    string filePath = Path.Combine(uploadFolder, uniqueName);
                    using var fileStream = new FileStream(filePath, FileMode.Create);
                    model.Image.CopyTo(fileStream);
                    return uniqueName;
                }
                ModelState.AddModelError(model.Image.FileName, "Your image should be a .png or .jpg file.");
                return "1_1_1";
            }
            return uniqueName;
        }
    }
}
