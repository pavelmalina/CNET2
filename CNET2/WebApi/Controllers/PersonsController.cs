using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        //private readonly List<Person> _dataSet;
        private readonly PeopleContext _dataSet;

        public PersonsController(PeopleContext peopleContext)
        {
            // From File
            //_dataSet = Serialization.LoadFromXML(@"C:\Users\StudentEN\source\repos\Malina\PersonDataset\dataset-utf.xml");
            _dataSet = peopleContext;
        }

        [HttpGet]
        public IEnumerable<Person> Get() => _dataSet.Persons.Include(x => x.Contracts).Include(x => x.HomeAddress);

        [HttpGet("GetByEmail")]
        public IEnumerable<Person> GetByEmail(string email) => _dataSet.Persons.Where(x => x.Email.ToLowerInvariant().Contains(email.ToLowerInvariant())).Include(x => x.Contracts).Include(x => x.HomeAddress);

        [HttpGet("GetSingleByEmail/{email}")]
        public Person GetSingleByEmail(string email) => _dataSet.Persons.Include(x => x.Contracts).Include(x => x.HomeAddress).Where(x => x.Email.ToLower().Contains(email.ToLower())).FirstOrDefault();

        [HttpGet("GetById/{id}")]
        public Person GetSingleById(int id) => _dataSet.Persons.Include(x => x.Contracts).Include(x => x.HomeAddress).FirstOrDefault(x => x.Id == id);
    }
}