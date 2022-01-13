using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVApplication.Model
{
    public interface IPersonRepository
    {
        Person GetPerson(int Id);
        IEnumerable<Person> GetAllPersons();
        Person AddPerson(Person person);
        Person UpdatePerson(Person person);
        Task<Person> DeletePerson(int Id);
    }
}
