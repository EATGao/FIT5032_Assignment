using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FIT5031_MyFirstAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitsController : ControllerBase
    {

        private readonly ILogger<UnitsController> _logger;

        public UnitsController(ILogger<UnitsController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetUnit")]
        public IEnumerable<Unit> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new Unit
            {
                Id = index,
                UnitCode = index,
                UnitName = "Name " + index
            })
            .ToArray();
        }
    }
}
