using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_William24A.Models;

namespace tl2_tp10_2023_William24A.Controllers;

public class UsuarioController : Controller
{
    private readonly ILogger<UsuarioController> _logger;

    private IDUsuarioRepository repoUsuarioC;

    public UsuarioController(ILogger<UsuarioController> logger)
    {
        _logger = logger;
        repoUsuarioC = new RepoUsuarioC();
    }

    [HttpGet]
    public IActionResult Listar()
    {
        var usuarios = repoUsuarioC.GetAll();
        return View(usuarios);
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
