using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVApplication.Model
{
    public class SQLPersonRepository : IPersonRepository
    {
        public AppDBContext Db { get; }
        public SQLPersonRepository(AppDBContext db)
        {
            Db = db;
        }


        public Person AddPerson(Person person)
        {
            Db.Persons.Add(person);
            Db.SaveChanges();
            return person;
        }

        public async Task<Person> DeletePerson(int Id)
        {
            Person person = Db.Persons.Find(Id);
            if(person != null)
            {
                Db.Persons.Remove(person);
                await Db.SaveChangesAsync();
            }
            return person;
        }

        public IEnumerable<Person> GetAllPersons()
        {
            return Db.Persons;
        }

        public Person GetPerson(int Id)
        {
            return Db.Persons.Find(Id);
        }

        public Person UpdatePerson(Person person)
        {
            var p = Db.Persons.Attach(person);
            p.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            Db.SaveChanges();
            return person;
        }
    }
}
