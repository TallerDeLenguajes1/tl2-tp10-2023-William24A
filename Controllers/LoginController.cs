using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_William24A.Models;
using MVC.ViewModels;

namespace tl2_tp10_2023_William24A.Controllers;

public class LoginController : Controller
{
    private readonly ILogger<LoginController> _logger;

    private IDUsuarioRepository repoUsuarioC;
    List<Usuario> Usuarios;

    public LoginController(ILogger<LoginController> logger)
    {
        _logger = logger;
        repoUsuarioC = new RepoUsuarioC();
        Usuarios = repoUsuarioC.GetAll();
    }
    public IActionResult Index()
    {
        return View(new LoginViewModel());
    }


    public IActionResult Login(Usuario usuario)
    {
        //existe el usuario?
        var usuarioLogeado = Usuarios.FirstOrDefault(u => u.NombreUsuario == usuario.NombreUsuario && u.Contrasenia == usuario.Contrasenia);

        // si el usuario no existe devuelvo al index
        if (usuarioLogeado == null)
        {
            _logger.LogWarning("Intento de acceso invalido "+ usuario.NombreUsuario+" clave "+ usuario.Contrasenia);
            return RedirectToRoute(new { controller = "Login", action = "Index"});
        } 
        
        //Registro el usuario
        logearUsuario(usuarioLogeado);
        
        //Devuelvo el usuario al Home
        _logger.LogInformation("El usuario " + usuarioLogeado.NombreUsuario + " ingreso correctamente");
        return RedirectToRoute(new { controller = "Usuario", action = "Listar"});
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
