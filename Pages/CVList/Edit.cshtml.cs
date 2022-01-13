using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CVApplication.Model;
using CVApplication.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CVApplication.Pages.CVList
{
    public class EditModel : PageModel
    {
        private readonly IPersonRepository personRepository;
        private readonly IWebHostEnvironment webhost;

        [BindProperty]
        public EditViewModel Model { get; set; }

        public EditModel(IPersonRepository personRepository, IWebHostEnvironment webhost)
        {
            this.personRepository = personRepository;
            this.webhost = webhost;
        }

        public IActionResult OnGet(int Id)
        {
            Person person = personRepository.GetPerson(Id);
            if (person == null)
                return RedirectToPage("PersonNotFound", Id);
            Model = new()
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Gender = person.Gender,
                BirthDate = person.BirthDate,
                Email = person.Email,
                ExistingPhotoPath = person.PhotoPath,
                PageTitle = "Edit Page"
            };
            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                Person person = personRepository.GetPerson(Model.Id);
                
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
                person.FirstName = Model.FirstName;
                person.LastName = Model.LastName;
                person.Gender = Model.Gender;
                person.BirthDate = Model.BirthDate;
                person.Password = Model.Password;
                person.Skills = skills;
                person.Nationality = nationalities;
                person.Email = Model.Email;
                person.PhotoPath = null;
                if (Model.Image != null)
                {
                    if (Model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(webhost.WebRootPath, "images", Model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    if (("1_1_1").Equals(ProcessUploadPhoto(Model)))
                    {
                        return Page();
                    }
                    person.PhotoPath = ProcessUploadPhoto(Model);
                }
                else if(Model.ExistingPhotoPath != null)
                {
                    person.PhotoPath = Model.ExistingPhotoPath;
                }
                personRepository.UpdatePerson(person);
                return RedirectToPage("Details", new { id = person.Id });
            }
            return Page();
        }

        private string ProcessUploadPhoto(EditViewModel model)
        {
            string uniqueName = null;
            if (model.Image != null)
            {
                string imgExt = Path.GetExtension(model.Image.FileName);
                if (imgExt.Equals(".jpg") || imgExt.Equals(".png"))
                {
                    string uploadFolder = Path.Combine(webhost.WebRootPath, "images");
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
