using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_William24A.Models;
using MVC.ViewModels;

namespace tl2_tp10_2023_William24A.Controllers;

public class LoginController : Controller
{
    private readonly ILogger<LoginController> _logger;

    private readonly IDUsuarioRepository _repoUsuarioC;

    public LoginController(ILogger<LoginController> logger,IDUsuarioRepository dUsuarioRepository)
    {
        _logger = logger;
        _repoUsuarioC = dUsuarioRepository;        
    }
    public IActionResult Index()
    {
        return View(new LoginViewModel());
    }


    public IActionResult Login(Usuario usuario)
    {
        try
        { 
            var usuarioLogeado = _repoUsuarioC.login(usuario.NombreUsuario ,usuario.Contrasenia);
            if(usuarioLogeado == null )
            {
                _logger.LogWarning("Intento de acceso invalido "+ usuario.NombreUsuario+" clave "+ usuario.Contrasenia);
                RedirectToRoute(new { controller = "Login", action = "Index"});
            }

            logearUsuario(usuarioLogeado);
            _logger.LogInformation("El usuario " + usuarioLogeado.NombreUsuario + " ingreso correctamente");
            return RedirectToRoute(new { controller = "Usuario", action = "Listar"});
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());            
            return  BadRequest();
        }
        
    }
    private void logearUsuario(Usuario user)
    {
        HttpContext.Session.SetString("Usuario", user.NombreUsuario);
        HttpContext.Session.SetString("Id", user.Id.ToString());
        HttpContext.Session.SetString("Contrasenia", user.Contrasenia);
        HttpContext.Session.SetString("Tipo", user.Tipo.ToString());
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
