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
        if(isAdmin())
        {
            var tableros = repoTableroC.ListarTableros();
            return View(tableros);
        }else{
            if(isOperador())
            {
                var tablerosU = repoTableroC.ListarTablerosUsuario(Convert.ToInt32(HttpContext.Session.GetString("Id")));
                return View(tablerosU);
            }
        }
        return RedirectToRoute(new { controller = "Login", action = "Index"});
    }

    [HttpGet]
    public IActionResult Create()
    {
        if(isAdmin() || isOperador())
        {
            return View(new Tablero());
        }
        return RedirectToRoute(new { controller = "Login", action = "Index"});
    }
    [HttpPost]
    public IActionResult Create(Tablero tablero)
    {
        if(!ModelState.IsValid) return RedirectToAction("Index");
        if(isAdmin()){
            repoTableroC.CrearTablero(tablero);
            return RedirectToAction("Listar");
        }
        else
        {
            if(tablero.Id_usuario_propietario == Convert.ToInt32(HttpContext.Session.GetString("Id")) && isOperador())
            {
                repoTableroC.CrearTablero(tablero);
                return RedirectToAction("Listar");
            }
        }
        return RedirectToRoute(new { controller = "Login", action = "Index"});
    }

    [HttpGet]
    public IActionResult Update(int id)
    {
        if(isAdmin() && isOperador())
        {
             return View(repoTableroC.ObtenerTableroID(id));
        }
        return RedirectToRoute(new { controller = "Login", action = "Index"});
    }
    [HttpPost]
    public IActionResult Update(Tablero tablero)
    {
        if(isAdmin())
        {
            repoTableroC.ModificarTablero(tablero.Id, tablero);
            return RedirectToAction("Listar");
        }
        else
        {
            if(isOperador() && tablero.Id_usuario_propietario == Convert.ToInt32(HttpContext.Session.GetString("Id")))
            {
                repoTableroC.ModificarTablero(tablero.Id, tablero);
                return RedirectToAction("Listar");
            }
        }
        return RedirectToRoute(new { controller = "Login", action = "Index"});
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        if(isAdmin())
        {
            repoTableroC.DeleteTablero(id);
            return RedirectToAction("Listar");
        }
        return RedirectToRoute(new { controller = "Login", action = "Index"});
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
