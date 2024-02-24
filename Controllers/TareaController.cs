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
    public IActionResult Create()
    {
        try
        {
            if(!isLogueado()) return RedirectToRoute(new {controller = "Login", action="Index"});
            if(isAdmin())
            {
                var crearTareaViewModel = new CrearTareaViewModel(_repoUsuarios.GetAll(), _repoTablero.ListarTableros());
                if (crearTareaViewModel.Usuarios == null || crearTareaViewModel.Tableros == null) return NoContent();
                return View(crearTareaViewModel);
            }
            return RedirectToRoute(new {controller = "Login", action = "Index"});
        }
        catch (System.Exception ex)
        {
            
            _logger.LogError(ex.ToString());
            return RedirectToRoute(new {controller = "Shared", action ="Error"});
        }
        
    }
    [HttpPost]
    public IActionResult Create(CrearTareaViewModel tareaVM)
    {
        try
        {
            if(!isLogueado()) return RedirectToRoute(new {controller = "Login", action="Index"});
            if(isAdmin()){
                if(!ModelState.IsValid) return RedirectToAction("Create");
                var tarea = new Tarea(tareaVM);
                _repoTareaC.CreaTarea(tarea);
                return RedirectToRoute(new {controller = "Tarea", action = "Listar", id = tareaVM.IdTablero});
            }
            return RedirectToRoute(new {controller = "Login", action = "Index"}); 
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
            if(!isLogueado()) return RedirectToRoute(new {controller = "Login", action="Index"});
            if(isAdmin())
            {
                var tarea = _repoTareaC.BuscarPorId(id);
                var tareaVM = new ActualizarTareaViewModel(tarea, _repoUsuarios.GetAll(), _repoTablero.ListarTableros());
                if (tareaVM.Usuarios == null || tareaVM.Tableros == null) return NoContent();
                return View(tareaVM);
            }else
            {
                if(isOperador() && _repoTareaC.BuscarPorId(id).IdUsuarioAsignado1 == Convert.ToInt32(HttpContext.Session.GetString("Id")))
                {
                    var tarea = _repoTareaC.BuscarPorId(id);
                    var tareaVM = new ActualizarTareaViewModel(tarea,"operador"); 
                    return View(tareaVM);
                }
            }
            return RedirectToRoute(new {controller = "Login", action = "Index"});
        }
        catch (System.Exception ex)
        {
           _logger.LogError(ex.ToString());
            return RedirectToRoute(new {controller = "Shared", action ="Error"});
        }
        
    }
    [HttpPost]
    public IActionResult Update(ActualizarTareaViewModel tareaVM)
    {
        try
        {
            if(!isLogueado()) return RedirectToRoute(new {controller = "Login", action="Index"});
            
           if(isAdmin())
            {
                if(!ModelState.IsValid) return RedirectToAction("Listar");
                var tarea = new Tarea(tareaVM);
                _repoTareaC.Modificar(tarea.Id, tarea);
                return RedirectToRoute(new {controller = "Tarea", action = "MyTarea"});
            }
            else
            {
                if(isOperador() && tareaVM.IdUsuarioAsignado1 == Convert.ToInt32(HttpContext.Session.GetString("Id")))
                {
                    var tarea = new Tarea(tareaVM);
                    _repoTareaC.Modificar(tarea.Id, tarea);
                    return RedirectToRoute(new {controller = "Tarea", action = "MyTarea"});
                }
            }
            return RedirectToRoute(new {controller = "Login", action = "Index"}); 
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.ToString());
            return RedirectToRoute(new {controller = "Shared", action ="Error"});
        }
        
    }

    [HttpGet]
    public IActionResult MyTarea()
    {
        try
        {
            if(!isLogueado()) return RedirectToRoute(new {controller = "Login", action="Index"});
            var tareas = _repoTareaC.BuscarTodasTarea(Convert.ToInt32(HttpContext.Session.GetString("Id")));
            string operador = "";
            if(isOperador())
            {
                operador = "operador";
            }
            var tareaViewModel = new ListarMiTareaViewModel(tareas, _repoTablero.ListarTableros(), _repoUsuarios.GetById(Convert.ToInt32(HttpContext.Session.GetString("Id"))),operador);
            return View(tareaViewModel);
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
            if(!isLogueado()) return RedirectToRoute(new {controller = "Login", action="Index"});
            if(isAdmin())
            {
                _repoTareaC.DeleteTarea(id);
                return RedirectToRoute(new {controller = "Tablero", action = "Listar"});
            }
            return RedirectToRoute(new {controller = "Login", action = "Index"});
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
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
