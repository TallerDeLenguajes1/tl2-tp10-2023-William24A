using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_William24A.Models;
using MVC.ViewModels;

namespace tl2_tp10_2023_William24A.Controllers;

public class TareaController : Controller
{
    private readonly ILogger<TareaController> _logger;

    private IDTareaRepositorio _repoTareaC;
    private IDtableroRepositorio _repoTablero;
    private IDUsuarioRepository _repoUsuarios;

    public TareaController(ILogger<TareaController> logger, IDTareaRepositorio repoTareaC, IDtableroRepositorio repoTablerp, IDUsuarioRepository repoUsuario)
    {
        _logger = logger;
        _repoTareaC = repoTareaC;
        _repoTablero = repoTablerp;
        _repoUsuarios = repoUsuario;
    }

    [HttpGet]
    public IActionResult Listar(int id)
    {
        try
        {
            if(!isLogueado()) return RedirectToRoute(new {controller = "Login", action="Index"});
            
            int userIdInSession = Convert.ToInt32(HttpContext.Session.GetString("Id"));
            
            Tablero tablero = _repoTablero.ObtenerTableroID(id);
            List<Tarea> tareas = _repoTareaC.BuscarTareasTablero(id);
            List<Usuario> usuarios = _repoUsuarios.GetAll();
            TableroViewModel tableroVM = new TableroViewModel(tablero);

            if (isAdmin() || tablero.Id_usuario_propietario == userIdInSession) // admin o operador dueño del tab.
            {
                return View(new ListarTareaViewModel(tareas, usuarios, tableroVM)); 
            }else
            {
                return View("ListarTareasAsignadas", new ListarTareasAsignadasViewModel(tareas, usuarios, tableroVM, userIdInSession));
            }     
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al acceder a las tareas{ex.ToString()}");
            return RedirectToRoute(new {controller = "Home", action="Error"});
        }
        
    }

    [HttpGet]
    public IActionResult Create(int id)
    {
        try
        {
            if(!isLogueado()) return RedirectToRoute(new {controller = "Login", action="Index"});
            if(isAdmin() || _repoTablero.ObtenerTableroID(id).Id_usuario_propietario == Convert.ToInt32(HttpContext.Session.GetString("Id")))
            {
                var nombre = _repoTablero.ObtenerTableroID(id).Nombre;
                var crearTareaViewModel = new CrearTareaViewModel(_repoUsuarios.GetAll(), nombre);
                crearTareaViewModel.IdTablero = id;
                return View(crearTareaViewModel);
            }
            return RedirectToRoute(new {controller = "Home", action = "Error"});
        }
        catch (System.Exception ex)
        {
            
            _logger.LogError(ex.ToString());
            return RedirectToRoute(new {controller = "Home", action ="Error"});
        }
        
    }
    [HttpPost]
    public IActionResult Create(CrearTareaViewModel tareaVM)
    {
        try
        {
            if(!isLogueado()) return RedirectToRoute(new {controller = "Login", action="Index"});
            if(!ModelState.IsValid) return RedirectToAction("Create");
            
             var tarea = new Tarea(tareaVM);
            _repoTareaC.CreaTarea(tarea.IdTablero,tarea);
           return RedirectToAction("Listar", new{id = tarea.IdTablero});
        }
        catch (System.Exception ex)
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
            if(!isLogueado()) return RedirectToRoute(new {controller = "Login", action="Index"});
            int userIdInSession = Convert.ToInt32(HttpContext.Session.GetString("Id"));
            Tarea nuevaTarea = _repoTareaC.BuscarPorId(id);
            var tableros = _repoTablero.ListarTableros();
            var usuarios = _repoUsuarios.GetAll();
            if (isAdmin() || nuevaTarea.IdUsuarioAsignado1 == userIdInSession)
            {
                return View(new ActualizarTareaViewModel(nuevaTarea, usuarios , tableros ));
            }else
            {
                return RedirectToRoute(new {controller = "Home", action="Error"});
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"{ex.ToString()}");
            return RedirectToRoute(new {controller = "Home", action="Error"});
        }
        
    }
    [HttpPost]
    public IActionResult Update(ActualizarTareaViewModel tareaVM)
    {
       try
        {
            if(!ModelState.IsValid) return RedirectToAction("Update");
            if(!isLogueado()) return RedirectToRoute(new {controller = "Login", action="Index"});

            int userIdInSession = Convert.ToInt32(HttpContext.Session.GetString("Id"));

            if(isAdmin() || tareaVM.IdUsuarioAsignado1 == userIdInSession){
                Tarea tarea = new Tarea(tareaVM);
                _repoTareaC.Modificar(tarea.Id, tarea);
                return RedirectToAction("Listar", new{id = tarea.IdTablero});
            }else
            {
                return RedirectToRoute(new {controller = "Home", action="Error"});
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"{ex.ToString()}");
            return RedirectToRoute(new {controller = "Home", action="Error"});
        } 
        
    }

    [HttpGet]
    public IActionResult MyTarea()
    {
        try
        {
            if(!isLogueado()) return RedirectToRoute(new {controller = "Login", action="Index"});
            int userIdInSession = Convert.ToInt32(HttpContext.Session.GetString("Id"));
            List<Tarea> misTareas = _repoTareaC.BuscarTodasTarea(userIdInSession);
            List<Tablero> tableros = _repoTablero.ListarTableros();
            Usuario usuario = _repoUsuarios.GetById(userIdInSession);
            return View(new ListarMiTareaViewModel(misTareas, tableros, usuario));
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al acceder a las tareas{ex.ToString()}");
            return RedirectToRoute(new {controller = "Home", action="Error"});
        }
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        try
        {
            if(!isLogueado()) return RedirectToRoute(new {controller = "Login", action="Index"});
            int userIdInSession = Convert.ToInt32(HttpContext.Session.GetString("Id"));

            Tarea tareaEliminar = _repoTareaC.BuscarPorId(id);
            if (isAdmin() || _repoTareaC.BuscarPorId(id).IdUsuarioAsignado1 == userIdInSession)
            {
                _repoTareaC.DeleteTarea(id);
                return RedirectToAction("Listar", new{id = tareaEliminar.IdTablero});   
            }else
            {
                return RedirectToRoute(new {controller = "Home", action="Error"});
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"{ex.ToString()}");
            return RedirectToRoute(new {controller = "Home", action="Error"});
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
}
