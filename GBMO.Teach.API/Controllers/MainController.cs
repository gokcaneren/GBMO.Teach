using GBMO.Teach.Localization.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace GBMO.Teach.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly IStringLocalizer<SharedResources> _localizer;

        public MainController(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
        }

        [HttpGet]
        public string Hello()
        {
            return _localizer["Greetings"];
        }
    }
}
