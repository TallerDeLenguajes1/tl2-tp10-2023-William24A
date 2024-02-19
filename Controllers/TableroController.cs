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
    private readonly IDTareaRepositorio _repoTarea;

    public TableroController(ILogger<TableroController> logger,IDtableroRepositorio repoTableroC, IDUsuarioRepository repoUsuarioC, IDTareaRepositorio repoTarea)
    {
        _logger = logger;
        _repoTableroC = repoTableroC;
        _repoUsuarioC = repoUsuarioC;
        _repoTarea = repoTarea;
    }

    [HttpGet]
    public IActionResult Listar()
    {
        try
        {
            if (!isLogueado()) return RedirectToRoute(new {controller = "Login", action="Index"});
            if(isOperador())
            {
                var tableros = _repoTableroC.ListarTablerosUsuario(Convert.ToInt32(HttpContext.Session.GetString("Id")));
                var myTableros = tablerosTareasAsignadas(Convert.ToInt32(HttpContext.Session.GetString("Id")));
                var tablerosVM = new ListarTableroViewModel(tableros, myTableros);
                return View(tablerosVM);
            }
            else
            {
               if(isAdmin())
               {
                 var tableros = _repoTableroC.ListarTableros();
                 var tablerosVM = new ListarTableroViewModel(tableros);
                 return View(tablerosVM);
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
            if (!isLogueado()) return RedirectToRoute(new {controller = "Login", action="Index"});
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
            if (!isLogueado()) return RedirectToRoute(new {controller = "Login", action="Index"});
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
            if (!isLogueado()) return RedirectToRoute(new {controller = "Login", action="Index"});
             if(isAdmin() ||  _repoTableroC.ObtenerTableroID(id).Id_usuario_propietario == Convert.ToInt32(HttpContext.Session.GetString("Id")))
            {
                var tablero = _repoTableroC.ObtenerTableroID(id);
                var tableroVM = new TableroViewModel(tablero);
                tableroVM.Usuarios = _repoUsuarioC.GetAll();
                if (tableroVM.Usuarios == null) return NoContent();
                return View(tableroVM);
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
            if (!isLogueado()) return RedirectToRoute(new {controller = "Login", action="Index"});
            if(isAdmin() || tableroVM.Id_usuario_propietario == Convert.ToInt32(HttpContext.Session.GetString("Id")))
            {
                if(!ModelState.IsValid) return RedirectToAction("Listar");
                var tablero = new Tablero(tableroVM);
                _repoTableroC.ModificarTablero(tablero.Id, tablero);
                return RedirectToAction("Listar");
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
            if (!isLogueado()) return RedirectToRoute(new {controller = "Login", action="Index"});
            if(isAdmin() || _repoTableroC.ObtenerTableroID(id).Id_usuario_propietario == Convert.ToInt32(HttpContext.Session.GetString("Id")))
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

    private bool isLogueado()
    {
        if (HttpContext.Session != null && !String.IsNullOrEmpty(HttpContext.Session.GetString("Usuario"))) 
                return true;
                
            return false;
    }
    
    private List<Tablero> tablerosTareasAsignadas(int idUsuario)
    {
        List<Tarea> misTareasAsignadas = _repoTarea.BuscarTodasTarea(idUsuario);

        List<Tablero> tableros = _repoTableroC.ListarTableros();

        List<Tablero> tablerosTareasAsignadas = new List<Tablero>(); 

        foreach (var tablero in tableros)
        {
            if (tablero.Id_usuario_propietario != idUsuario && misTareasAsignadas.Any(tarea => tarea.IdTablero == tablero.Id))
            {
                tablerosTareasAsignadas.Add(tablero);
            }
        }

        return tablerosTareasAsignadas;
    }
}
