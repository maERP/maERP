using maERP.Shared.Models.Database;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace maERP.Server.Controllers.Web;

public class WebTrackingController : Controller
{
    private readonly ILogger<WebTrackingController> _logger;

    public WebTrackingController(ILogger<WebTrackingController> logger)
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