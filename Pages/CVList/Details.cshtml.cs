using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CVApplication.Model;
using CVApplication.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CVApplication.Pages.CVList
{
    public class DetailsModel : PageModel
    {
        private readonly IPersonRepository personRepository;

        [BindProperty]
        public DetailsViewModel Model { get; set; }
        public DetailsModel(IPersonRepository personRepository)
        {
            this.personRepository = personRepository;
        }

        public IActionResult OnGet(int? Id)
        {
            if (personRepository.GetPerson(Id.Value) == null)
            {
                Response.StatusCode = 404;
                return RedirectToPage("PersonNotFound", Id);
            }
            Model = new()
            {
                Person = personRepository.GetPerson(Id ?? 1),
                PageTitle = "Details Page"
            };
            return Page();
        }
    }
}
