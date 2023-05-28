using maERP.Server.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace maERP.Server.Controllers.Web;

public class WebController : Controller
{
    private readonly ILogger<WebController> _logger;

    public WebController(ILogger<WebController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}