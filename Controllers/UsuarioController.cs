using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVC.ViewModels;
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
        var usuariosView = new ListarUsuarioViewModel(usuarios);
        return View(usuariosView);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new CrearUsuarioViewModel());
    }
    [HttpPost]
    public IActionResult Create(CrearUsuarioViewModel usuarioVMD)
    {
        var usuario = new Usuario(usuarioVMD);
        repoUsuarioC.Create(usuario);
        return RedirectToAction("Listar");
    }

    [HttpGet]
    public IActionResult Update(int id)
    {
        var usuario = repoUsuarioC.GetById(id);
        var usuarioVM = new UsuarioViewModel(usuario);
        return View(usuarioVM);
    }
    [HttpPost]
    public IActionResult Update(UsuarioViewModel usuarioVM)
    {
        var usuario = new Usuario(usuarioVM);
        repoUsuarioC.Update(usuario.Id, usuario);
        return RedirectToAction("Listar");
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        repoUsuarioC.Remove(id);
        return RedirectToAction("Listar");
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}