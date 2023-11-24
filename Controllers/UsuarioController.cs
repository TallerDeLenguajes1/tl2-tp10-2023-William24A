using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVC.ViewModels;
using tl2_tp10_2023_William24A.Models;

namespace tl2_tp10_2023_William24A.Controllers;

public class UsuarioController : Controller
{
    private readonly ILogger<UsuarioController> _logger;

    private readonly IDUsuarioRepository _repoUsuarioC;

    public UsuarioController(ILogger<UsuarioController> logger, IDUsuarioRepository reporUsuario)
    {
        _logger = logger;
        _repoUsuarioC = reporUsuario;
    }

    [HttpGet]
    public IActionResult Listar()
    {
        if(isAdmin())
        {
            var usuarios = _repoUsuarioC.GetAll();
            var usuariosView = new ListarUsuarioViewModel(usuarios);
            return View(usuariosView);
        }
        return RedirectToRoute(new {controller = "Login", action = "Index"});  
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new CrearUsuarioViewModel());
    }
    [HttpPost]
    public IActionResult Create(CrearUsuarioViewModel usuarioVMD)
    {
        if(!ModelState.IsValid) return RedirectToAction("Create");
        var usuario = new Usuario(usuarioVMD);
        _repoUsuarioC.Create(usuario);
        return RedirectToAction("Listar");
    }

    [HttpGet]
    public IActionResult Update(int id)
    {
        var usuario = _repoUsuarioC.GetById(id);
        var usuarioVM = new UsuarioViewModel(usuario);
        return View(usuarioVM);
    }
    [HttpPost]
    public IActionResult Update(UsuarioViewModel usuarioVM)
    {
        if(!ModelState.IsValid) return RedirectToAction("Listar");
        var usuario = new Usuario(usuarioVM);
        _repoUsuarioC.Update(usuario.Id, usuario);
        return RedirectToAction("Listar");
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        _repoUsuarioC.Remove(id);
        return RedirectToAction("Listar");
    }

     private bool isAdmin()
        {
            if (HttpContext.Session != null && HttpContext.Session.GetString("Tipo") == "admin") 
                return true;
                
            return false;
        }
    private bool isOperador()
    {
        if (HttpContext.Session != null && HttpContext.Session.GetString("Tipo") == "operador") 
                return true;
                
            return false;
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
