using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVC.ViewModels;
using tl2_tp10_2023_William24A.Models;

namespace tl2_tp10_2023_William24A.Controllers;

public class TableroController : Controller
{
    private readonly ILogger<TableroController> _logger;

    private readonly IDtableroRepositorio _repoTableroC;
    private readonly IDUsuarioRepository _repoUsuarioC;

    public TableroController(ILogger<TableroController> logger,IDtableroRepositorio repoTableroC, IDUsuarioRepository repoUsuarioC )
    {
        _logger = logger;
        _repoTableroC = repoTableroC;
        _repoUsuarioC = repoUsuarioC;
    }

    [HttpGet]
    public IActionResult Listar()
    {
        try
        {
            if(isAdmin())
            {
                var tableros = _repoTableroC.ListarTableros();
                var tablerosVM = new ListarTableroViewModel(tableros);
                return View(tablerosVM);
            }else{
                if(isOperador())
                {
                    var tablerosU = _repoTableroC.ListarTablerosUsuario(Convert.ToInt32(HttpContext.Session.GetString("Id")));
                    var tablerosUVM = new ListarTableroViewModel(tablerosU);
                    return View(tablerosUVM);
                }
            }
            return RedirectToRoute(new { controller = "Home", action = "Index"});
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());            
            return RedirectToRoute(new { controller = "Shared", action = "Error"});
        }

    }

    [HttpGet]
    public IActionResult Create()
    {
        try
        {
            if(isAdmin() || isOperador())
            {
                CrearTableroViewModel crearTableroViewModel = new();
                crearTableroViewModel.Usuarios = _repoUsuarioC.GetAll();
                if (crearTableroViewModel.Usuarios == null) return NoContent();
                return View(crearTableroViewModel);
            }
            return RedirectToRoute(new { controller = "Login", action = "Index"});
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToRoute(new {controller = "Shared", action ="Error"});
        }
        
    }
    [HttpPost]
    public IActionResult Create(CrearTableroViewModel tableroVM)
    {

        try
        {
            if(isAdmin()){
                var tablero = new Tablero(tableroVM); 
                if(!ModelState.IsValid)
                    return RedirectToAction("Create");
                _repoTableroC.CrearTablero(tablero);
                return RedirectToAction("Listar");
            }
            else
            {
                if(!ModelState.IsValid) return RedirectToAction("Create");
                if(tableroVM.Id_usuario_propietario == Convert.ToInt32(HttpContext.Session.GetString("Id")) && isOperador())
                {
                    var tablero = new Tablero(tableroVM); 
                    _repoTableroC.CrearTablero(tablero);
                    return RedirectToAction("Listar");
                }
            }
            return RedirectToRoute(new { controller = "Login", action = "Index"});
        }
        catch(Exception ex)
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
             if(isAdmin())
            {
                var tablero = _repoTableroC.ObtenerTableroID(id);
                var tableroVM = new TableroViewModel(tablero);
                tableroVM.Usuarios = _repoUsuarioC.GetAll();
                if (tableroVM.Usuarios == null) return NoContent();
                return View(tableroVM);
            }
            else
            {
                if(isOperador() && _repoTableroC.ObtenerTableroID(id).Id_usuario_propietario == Convert.ToInt32(HttpContext.Session.GetString("Id")))
                {
                    var tablero = _repoTableroC.ObtenerTableroID(id);
                    var tableroVM = new TableroViewModel(tablero);
                    return View(tableroVM);
                }
            }
            return RedirectToRoute(new { controller = "Tablero", action = "Listar"});
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToRoute(new {controller = "Shared", action ="Error"});
        }
       
    }
    [HttpPost]
    public IActionResult Update(TableroViewModel tableroVM)
    {
        try
        {
            if(isAdmin())
            {
                if(!ModelState.IsValid) return RedirectToAction("Listar");
                var tablero = new Tablero(tableroVM);
                _repoTableroC.ModificarTablero(tablero.Id, tablero);
                return RedirectToAction("Listar");
            }
            else
            {
                if(!ModelState.IsValid) return RedirectToAction("Listar");
                if(isOperador() && tableroVM.Id_usuario_propietario == Convert.ToInt32(HttpContext.Session.GetString("Id")))
                {
                    var tablero = new Tablero(tableroVM);
                    _repoTableroC.ModificarTablero(tablero.Id, tablero);
                    return RedirectToAction("Listar");
                }
            }
            return RedirectToRoute(new { controller = "Tablero", action = "Listar"});
        }
        catch(Exception ex)
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
            if(isAdmin())
            {
                _repoTableroC.DeleteTablero(id);
                return RedirectToAction("Listar");
            }
            return RedirectToRoute(new { controller = "Login", action = "Index"});
        }
        catch(Exception ex)
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
