using Microsoft.AspNetCore.Mvc;

namespace MessageFlow.Api.Index.Controllers;

[ApiController]
[Route("/")]
public class IndexController : ControllerBase
{

    [HttpGet]
    public ActionResult<string> Index()
        => Ok("You have reached the MessageFlow Api.\r\n\r\nWelcome! :)");
}
