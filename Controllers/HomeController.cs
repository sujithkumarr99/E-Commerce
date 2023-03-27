using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Websitara2.Models;

namespace Websitara2.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Product()
    {
        return View();
    }

    public IActionResult ManageCategory()
    {
        return View();
    }
    public IActionResult price()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

