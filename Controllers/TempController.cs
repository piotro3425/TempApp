using Microsoft.AspNetCore.Mvc;
using TempApp.Models;
using TempApp.Services;

namespace TempApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TempController : ControllerBase
    {
        ITempProvider tempProvider;
        public TempController(ITempProvider tempProvider)
        {
            this.tempProvider = tempProvider;
        }

        [HttpGet(Name = "GetTemp")]
        public Temp Get()
        {
            return new Temp() { Temperature = tempProvider.GetTemp() };
        }
    }
}
