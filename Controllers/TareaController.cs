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
        var tableros = repoTareaC.BuscarTodasTarea(2);
        return View(tableros);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new Tarea());
    }
    [HttpPost]
    public IActionResult Create(Tarea tarea)
    {
        repoTareaC.CreaTarea(tarea);
        return RedirectToAction("Listar");
    }

    [HttpGet]
    public IActionResult Update(int id)
    {
        return View(repoTareaC.BuscarPorId(id));
    }
    [HttpPost]
    public IActionResult Update(Tarea tarea)
    {
        repoTareaC.Modificar(tarea.Id, tarea);
        return RedirectToAction("Listar");
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        repoTareaC.DeleteTarea(id);
        return RedirectToAction("Listar");
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
