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
            if(!isLogueado()) return RedirectToRoute(new {controller = "Login", action="Index"});
            
            int userIdInSession = Convert.ToInt32(HttpContext.Session.GetString("Id"));

            List<Tablero> misTableros = _repoTableroC.ListarTablerosUsuario(userIdInSession);

            List<Tablero> tablerosTareas = tablerosTareasAsignadas(userIdInSession);
            List<Usuario> usuarios = _repoUsuarioC.GetAll();

            return View(new ListarTableroViewModel(misTableros,tablerosTareas,usuarios));

        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al acceder a los tableros {ex.ToString()}");
            return RedirectToRoute(new {controller = "Home", action="Error"}); //?
        }

    }

    [HttpGet]
    public IActionResult TodosTableros() 
    {
        try
        {
            if (!isLogueado()) return RedirectToRoute(new {controller = "Login", action="Index"}); 
            if (!isAdmin()) return RedirectToRoute(new {controller = "Login", action="Index"}); // o tiro un error??

            List<Tablero> todosTableros = _repoTableroC.ListarTableros();
            List<Usuario> usuarios = _repoUsuarioC.GetAll();
            return View(new ListarTableroViewModel(todosTableros, usuarios));
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al acceder a todos los tableros {ex.ToString()}"); // loggeo el error
            return RedirectToRoute(new {controller = "Home", action="Error"}); //?
        }
    }

    [HttpGet]
    public IActionResult Create()
    {
        try
        {
            if (!isLogueado()) return RedirectToRoute(new {controller = "Login", action="Index"});
           
            if (isAdmin())
            {
                var tablero = new CrearTableroViewModel(_repoUsuarioC.GetAll());
                return View(tablero);
            }else
            {
                var tablero = new CrearTableroViewModel(_repoUsuarioC.GetAll());
                tablero.Id_usuario_propietario = Convert.ToInt32(HttpContext.Session.GetString("Id"));
                return View(tablero); 
            }
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToRoute(new {controller = "Home", action ="Error"});
        }
        
    }
    [HttpPost]
    public IActionResult Create(CrearTableroViewModel tableroVM)
    {

        try
        {
            if (!isLogueado()) return RedirectToRoute(new {controller = "Login", action="Index"});
            if(!ModelState.IsValid) return RedirectToAction("Create");
            Tablero tablero = new Tablero(tableroVM);
            _repoTableroC.CrearTablero(tablero);
            return RedirectToAction("Listar");
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToRoute(new {controller = "Home", action ="Error"});
        }
        
    }

        [HttpGet]
    public IActionResult CreateTodosTableros()
    {
        try
        {
            if (!isLogueado()) return RedirectToRoute(new {controller = "Login", action="Index"});
           
            if (isAdmin())
            {
                var tablero = new CrearTableroViewModel(_repoUsuarioC.GetAll());
                return View(tablero);
            }
            return RedirectToRoute(new {controller = "Home", action ="Error"});
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToRoute(new {controller = "Home", action ="Error"});
        }
        
    }
    [HttpPost]
    public IActionResult CreateTodosTableros(CrearTableroViewModel tableroVM)
    {

        try
        {
            if (!isLogueado()) return RedirectToRoute(new {controller = "Login", action="Index"});
            if(!ModelState.IsValid) return RedirectToAction("Create");
            if (isAdmin())
            {
                Tablero tablero = new Tablero(tableroVM);
                _repoTableroC.CrearTablero(tablero);
                return RedirectToAction("TodosTableros");
            }
            return RedirectToRoute(new {controller = "Home", action ="Error"});
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToRoute(new {controller = "Home", action ="Error"});
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
                var tableroVM = new ActualizarTableroViewModel(tablero, _repoUsuarioC.GetAll());
                if (tableroVM.Usuarios == null) return NoContent();
                return View(tableroVM);
            }
            return RedirectToRoute(new { controller = "Home", action = "Error"});
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToRoute(new {controller = "Home", action ="Error"});
        }
       
    }
    [HttpPost]
    public IActionResult Update(ActualizarTableroViewModel tableroVM)
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
            return RedirectToRoute(new { controller = "Home", action = "Error"});
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToRoute(new {controller = "Home", action ="Error"});
        }
        
    }

    [HttpGet]
    public IActionResult UpdateTodosTableros(int id)
    {
        try
        {
            if (!isLogueado()) return RedirectToRoute(new {controller = "Login", action="Index"});
             if(isAdmin() )
            {
                var tablero = _repoTableroC.ObtenerTableroID(id);
                var tableroVM = new ActualizarTableroViewModel(tablero, _repoUsuarioC.GetAll());
                if (tableroVM.Usuarios == null) return NoContent();
                return View(tableroVM);
            }
            return RedirectToRoute(new { controller = "Home", action = "Error"});
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToRoute(new {controller = "Home", action ="Error"});
        }
       
    }
    [HttpPost]
    public IActionResult UpdateTodosTableros(ActualizarTableroViewModel tableroVM)
    {
        try
        {
            if (!isLogueado()) return RedirectToRoute(new {controller = "Login", action="Index"});
            if(isAdmin())
            {
                if(!ModelState.IsValid) return RedirectToAction("UpdateTodosTableros", new {id = tableroVM.Id});
                var tablero = new Tablero(tableroVM);
                _repoTableroC.ModificarTablero(tablero.Id, tablero);
                return RedirectToAction("TodosTableros");
            }
            return RedirectToRoute(new { controller = "Home", action = "Error"});
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToRoute(new {controller = "Home", action ="Error"});
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
                foreach (var tarea in _repoTarea.BuscarTareasTablero(id))
                {
                    _repoTarea.DeleteTarea(tarea.Id);   
                }
                _repoTableroC.DeleteTablero(id);
                return RedirectToAction("Listar");
            }
            return RedirectToRoute(new { controller = "Login", action = "Index"});
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToRoute(new {controller = "Home", action ="Error"});
        }
        
    }

    [HttpGet]
    public IActionResult DeleteTodosTableros(int id)
    {
        try
        {
            if (!isLogueado()) return RedirectToRoute(new {controller = "Login", action="Index"});
            if(isAdmin() || _repoTableroC.ObtenerTableroID(id).Id_usuario_propietario == Convert.ToInt32(HttpContext.Session.GetString("Id")))
            {
                foreach (var tarea in _repoTarea.BuscarTareasTablero(id))
                {
                    _repoTarea.DeleteTarea(tarea.Id);   
                }
                _repoTableroC.DeleteTablero(id);
                return RedirectToAction("TodosTableros");
            }
            return RedirectToRoute(new { controller = "Login", action = "Index"});
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToRoute(new {controller = "Home", action ="Error"});
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
