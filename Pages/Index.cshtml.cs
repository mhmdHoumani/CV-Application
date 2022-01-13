using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CVApplication.Model;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace CVApplication.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IPersonRepository personRepository;
        private readonly IWebHostEnvironment webHost;

        public IEnumerable<Person> Persons { get; set; }
        public IndexModel(IPersonRepository personRepository, IWebHostEnvironment webHost)
        {
            this.personRepository = personRepository;
            this.webHost = webHost;
        }
        public void OnGet()
        {
            Persons = personRepository.GetAllPersons();
        }
        public async Task<IActionResult> OnPostDelete(int id)
        {
            var person = personRepository.GetPerson(id);
            if (person == null)
                return RedirectToPage("/CVList/PersonNotFound", id);
            if(person.PhotoPath != null)
            {
                string filePath = Path.Combine(webHost.WebRootPath, "images", person.PhotoPath);
                System.IO.File.Delete(filePath);
            }
            await personRepository.DeletePerson(id);
            return RedirectToPage();
        }
    }
}
