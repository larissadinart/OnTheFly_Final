using Microsoft.AspNetCore.Mvc;
using OnTheFly_Final.Services;

namespace OnTheFly_Final.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly SalesServices _salesservices;
    }
}
