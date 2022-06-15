using Data;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractsController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Contract> GetAll()
        {
            var dataSet = Serialization.LoadFromXML(@"C:\Users\StudentEN\source\repos\Malina\PersonDataset\dataset-utf.xml");

            return dataSet.SelectMany(x => x.Contracts);
        }
    }
}
