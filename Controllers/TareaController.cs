using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_William24A.Models;
using MVC.ViewModels;

namespace tl2_tp10_2023_William24A.Controllers;

public class TareaController : Controller
{
    private readonly ILogger<TareaController> _logger;

    private IDTareaRepositorio repoTareaC;

    public TareaController(ILogger<TareaController> logger)
    {
        _logger = logger;
        repoTareaC = new RepoTareaC();
    }

    [HttpGet]
    public IActionResult Listar()
    {
        if(isAdmin())
        {
            var tableros = repoTareaC.BuscarTareasTablero(1);
            var tablerosVM = new ListarTareaViewModel(tableros);
            return View(tablerosVM);
        }
        else
        {
            if(isOperador())
            {
                var tablerosU = repoTareaC.BuscarTodasTarea(Convert.ToInt32(HttpContext.Session.GetString("Id")));
                var tablerosUVM = new ListarTareaViewModel(tablerosU);
                return View(tablerosUVM);
            }
            else
            {
                return RedirectToRoute(new {controller = "Login", action = "Index"});
            } 
        }
    }

    [HttpGet]
    public IActionResult Create()
    {
        if(isAdmin())
        {
            return View(new CrearTareaViewModel());
        }
        return RedirectToRoute(new {controller = "Login", action = "Index"});
    }
    [HttpPost]
    public IActionResult Create(CrearTareaViewModel tareaVM)
    {
        if(isAdmin()){
            var tarea = new Tarea(tareaVM);
            repoTareaC.CreaTarea(tarea);
            return RedirectToAction("Listar");
        }
        return RedirectToRoute(new {controller = "Login", action = "Index"});
    }

    [HttpGet]
    public IActionResult Update(int id)
    {
        if(isAdmin())
        {
            var tarea = repoTareaC.BuscarPorId(id);
            var tareaVM = new TareaViewModel(tarea);
             return View(tareaVM);
        }
       return RedirectToRoute(new {controller = "Login", action = "Index"});
    }
    [HttpPost]
    public IActionResult Update(TareaViewModel tareaVM)
    {
        if(isAdmin())
        {
            var tarea = new Tarea(tareaVM);
            repoTareaC.Modificar(tarea.Id, tarea);
            return RedirectToAction("Listar");
        }
        return RedirectToRoute(new {controller = "Login", action = "Index"});
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        if(isAdmin())
        {
            repoTareaC.DeleteTarea(id);
            return RedirectToAction("Listar");
        }
       return RedirectToRoute(new {controller = "Login", action = "Index"});
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
