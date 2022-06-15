using Data;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly List<Person> _dataSet;

        public PersonsController()
        {
            _dataSet = Serialization.LoadFromXML(@"C:\Users\StudentEN\source\repos\Malina\PersonDataset\dataset-utf.xml");
        }

        [HttpGet]
        public IEnumerable<Person> Get() => _dataSet;

        [HttpGet("GetByEmail")]
        public IEnumerable<Person> GetByEmail(string email) => _dataSet.Where(x => x.Email.ToLowerInvariant().Contains(email.ToLowerInvariant()));

        [HttpGet("GetSingleByEmail/{email}")]
        public Person GetSingleByEmail(string email) => _dataSet.Where(x => x.Email.ToLowerInvariant().Contains(email.ToLowerInvariant())).FirstOrDefault();
    }
}
