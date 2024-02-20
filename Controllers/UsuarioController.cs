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
        try
        {
            if (!isLogueado()) return RedirectToRoute(new {controller = "Login", action="Index"});
            if(isAdmin())
            {
                var usuarios = _repoUsuarioC.GetAll();
                var usuariosView = new ListarUsuarioViewModel(usuarios);
                return View(usuariosView);
            }
            return RedirectToRoute(new {controller = "Home", action = "Index"});  
        }
        catch (System.Exception ex)
        {
            
            _logger.LogError(ex.ToString());
            return RedirectToRoute(new {controller = "Shared", action ="Error"});
        }
        
    }

    [HttpGet]
    public IActionResult Create()
    {
        try
        {
            if (!isLogueado()) return RedirectToRoute(new {controller = "Login", action="Index"});
            if(isAdmin())
            {
                return View(new CrearUsuarioViewModel());
            }
            return RedirectToRoute(new {controller = "Home", action = "Index"});  
        }
        catch (System.Exception ex)
        {
            
             _logger.LogError(ex.ToString());
            return RedirectToRoute(new {controller = "Shared", action ="Error"});
        }
       
    }
    [HttpPost]
    public IActionResult Create(CrearUsuarioViewModel usuarioVMD)
    {
        try
        {
            if (!isLogueado()) return RedirectToRoute(new {controller = "Login", action="Index"});
             if(isAdmin())
            {
                if(!ModelState.IsValid) return RedirectToAction("Create");
                var usuario = new Usuario(usuarioVMD);
                _repoUsuarioC.Create(usuario);
                return RedirectToAction("Listar");
            }
            return RedirectToRoute(new {controller = "Home", action = "Index"}); 
        }
        catch (System.Exception ex)
        {
             _logger.LogError(ex.ToString());
            return RedirectToRoute(new {controller = "Shared", action ="Error"});
        }
       
    }

    [HttpGet]
    public IActionResult Update(int id)
    {
        try
        {
            if (!isLogueado()) return RedirectToRoute(new {controller = "Login", action="Index"});
             if(isAdmin())
                {
                    var usuario = _repoUsuarioC.GetById(id);
                    var usuarioVM = new ActualizarUsuarioViewModel(usuario);
                    return View(usuarioVM);
                }
                return RedirectToRoute(new {controller = "Home", action = "Index"}); 
        }
        catch (System.Exception ex)
        {
             _logger.LogError(ex.ToString());
            return RedirectToRoute(new {controller = "Shared", action ="Error"});
        }
        
    }
    [HttpPost]
    public IActionResult Update(ActualizarUsuarioViewModel usuarioVM)
    {
        try
        {
            if (!isLogueado()) return RedirectToRoute(new {controller = "Login", action="Index"});
            if(isAdmin())
            {
                if(!ModelState.IsValid) return RedirectToAction("Listar");
                var usuario = new Usuario(usuarioVM);
                _repoUsuarioC.Update(usuario.Id, usuario);
                return RedirectToAction("Listar");
            }
            return RedirectToRoute(new {controller = "Home", action = "Index"}); 
        }
        catch (System.Exception ex)
        {
             _logger.LogError(ex.ToString());
            return RedirectToRoute(new {controller = "Shared", action ="Error"});
        }
        
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        try
        {
            if (!isLogueado()) return RedirectToRoute(new {controller = "Login", action="Index"});
             if(isAdmin())
            {
                _repoUsuarioC.Remove(id);
                return RedirectToAction("Listar");
            }
            return RedirectToRoute(new {controller = "Home", action = "Index"});
        }
        catch (System.Exception ex)
        {
             _logger.LogError(ex.ToString());
            return RedirectToRoute(new {controller = "Shared", action ="Error"});
        }
        
    }

     private bool isAdmin()
        {
            if (HttpContext.Session != null && HttpContext.Session.GetString("Tipo") == "admin") 
                return true;
                
            return false;
        }
    private bool isLogueado()
    {
        if (HttpContext.Session != null && !String.IsNullOrEmpty(HttpContext.Session.GetString("Usuario"))) 
                return true;
                
            return false;
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
