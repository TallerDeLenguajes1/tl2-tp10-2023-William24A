using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_William24A.Models;

namespace tl2_tp10_2023_William24A.Controllers;

public class TableroController : Controller
{
    private readonly ILogger<TableroController> _logger;

    private IDtableroRepositorio repoTableroC;

    public TableroController(ILogger<TableroController> logger)
    {
        _logger = logger;
        repoTableroC = new RepoTableroC();
    }

    [HttpGet]
    public IActionResult Listar()
    {
        var tableros = repoTableroC.ListarTableros();
        return View(tableros);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new Tablero());
    }
    [HttpPost]
    public IActionResult Create(Tablero tablero)
    {
        repoTableroC.CrearTablero(tablero);
        return RedirectToAction("Listar");
    }

    [HttpGet]
    public IActionResult Update(int id)
    {
        return View(repoTableroC.ObtenerTableroID(id));
    }
    [HttpPost]
    public IActionResult Update(Tablero tablero)
    {
        repoTableroC.ModificarTablero(tablero.Id, tablero);
        return RedirectToAction("Listar");
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        repoTableroC.DeleteTablero(id);
        return RedirectToAction("Listar");
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
