using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_William24A.Models;

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
            return View(tableros);
        }
        else
        {
            if(isOperador())
            {
                var tablerosU = repoTareaC.BuscarTodasTarea(Convert.ToInt32(HttpContext.Session.GetString("Id")));
                return View(tablerosU);
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
            return View(new Tarea());
        }
        return RedirectToRoute(new {controller = "Login", action = "Index"});
    }
    [HttpPost]
    public IActionResult Create(Tarea tarea)
    {
        if(isAdmin()){
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
             return View(repoTareaC.BuscarPorId(id));
        }
       return RedirectToRoute(new {controller = "Login", action = "Index"});
    }
    [HttpPost]
    public IActionResult Update(Tarea tarea)
    {
        if(isAdmin())
        {
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
