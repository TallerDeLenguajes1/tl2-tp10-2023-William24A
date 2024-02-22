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


    public IActionResult Login(LoginViewModel usuario)
    {
        try
        { 
             if(!ModelState.IsValid) return RedirectToAction("Index");

            var usuarioLogeado = _repoUsuarioC.login(usuario.NombreUsuario ,usuario.Contrasenia);
            if(usuarioLogeado == null )
            {
                _logger.LogWarning("Intento de acceso invalido "+ usuario.NombreUsuario+" clave "+ usuario.Contrasenia);
                ModelState.AddModelError(string.Empty, "Nombre de usuario o contraseña incorrectos.");
                return View("Index", usuario); 
            }

            _logger.LogInformation("El usuario: " + usuario.NombreUsuario + " Ingreso correctamente"); 

            logearUsuario(usuarioLogeado);
            return RedirectToRoute(new { controller = "Tablero", action = "Listar"});
        }
        catch(Exception ex)
        {
            _logger.LogError($"Error al intentar logear un usuario {ex.ToString()}");
            ModelState.AddModelError(string.Empty, "Ocurrió un error al intentar iniciar sesión. Por favor, inténtalo de nuevo.");
            return View("Index", usuario); 
        }
        
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToRoute(new { controller = "Login", action = "Index" });
    }
    private void logearUsuario(Usuario user)
    {
        HttpContext.Session.SetString("Usuario", user.NombreUsuario);
        HttpContext.Session.SetString("Id", user.Id.ToString());
        HttpContext.Session.SetString("Tipo", user.Tipo.ToString());
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
