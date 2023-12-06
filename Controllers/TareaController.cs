using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_William24A.Models;
using MVC.ViewModels;

namespace tl2_tp10_2023_William24A.Controllers;

public class TareaController : Controller
{
    private readonly ILogger<TareaController> _logger;

    private IDTareaRepositorio _repoTareaC;

    public TareaController(ILogger<TareaController> logger, IDTareaRepositorio repoTareaC)
    {
        _logger = logger;
        _repoTareaC = repoTareaC;
    }

    [HttpGet]
    public IActionResult Listar(int id)
    {
        try
        {
           if(isAdmin())
            {
                var tableros = _repoTareaC.BuscarTareasTablero(id);
                var tablerosVM = new ListarTareaViewModel(tableros);
                return View(tablerosVM);
            }
            else
            {
                if(isOperador())
                {
                    var tablerosU = _repoTareaC.BuscarTodasTarea(Convert.ToInt32(HttpContext.Session.GetString("Id")));
                    var tablerosUVM = new ListarTareaViewModel(tablerosU);
                    return View(tablerosUVM);
                }
                else
                {
                    return RedirectToRoute(new {controller = "Login", action = "Index"});
                } 
            } 
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
            if(isAdmin())
            {
                return View(new CrearTareaViewModel());
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
            if(isAdmin()){
            if(!ModelState.IsValid) return RedirectToAction("Create");
                var tarea = new Tarea(tareaVM);
                _repoTareaC.CreaTarea(tarea);
                return RedirectToRoute(new {controller = "Tablero", action = "Listar"});;
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
            if(isAdmin())
            {
                var tarea = _repoTareaC.BuscarPorId(id);
                var tareaVM = new TareaViewModel(tarea);
                return View(tareaVM);
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
    public IActionResult Update(TareaViewModel tareaVM)
    {
        try
        {
           if(isAdmin())
            {
                if(!ModelState.IsValid) return RedirectToAction("Listar");
                var tarea = new Tarea(tareaVM);
                _repoTareaC.Modificar(tarea.Id, tarea);
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

    [HttpGet]
    public IActionResult Delete(int id)
    {
        try
        {
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
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
