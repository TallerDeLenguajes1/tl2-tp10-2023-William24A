using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVC.ViewModels;
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
            var tablerosVM = new ListarTableroViewModel(tableros);
            return View(tablerosVM);
        }else{
            if(isOperador())
            {
                var tablerosU = repoTableroC.ListarTablerosUsuario(Convert.ToInt32(HttpContext.Session.GetString("Id")));
                var tablerosUVM = new ListarTableroViewModel(tablerosU);
                return View(tablerosUVM);
            }
        }
        return RedirectToRoute(new { controller = "Login", action = "Index"});
    }

    [HttpGet]
    public IActionResult Create()
    {
        if(isAdmin() || isOperador())
        {
            return View(new CrearTableroViewModel());
        }
        return RedirectToRoute(new { controller = "Login", action = "Index"});
    }
    [HttpPost]
    public IActionResult Create(CrearTableroViewModel tableroVM)
    {
        if(!ModelState.IsValid) return RedirectToAction("Index");
        if(isAdmin()){
            var tablero = new Tablero(tableroVM); 
            repoTableroC.CrearTablero(tablero);
            return RedirectToAction("Listar");
        }
        else
        {
            if(tableroVM.Id_usuario_propietario == Convert.ToInt32(HttpContext.Session.GetString("Id")) && isOperador())
            {
                var tablero = new Tablero(tableroVM); 
                repoTableroC.CrearTablero(tablero);
                return RedirectToAction("Listar");
            }
        }
        return RedirectToRoute(new { controller = "Login", action = "Index"});
    }

    [HttpGet]
    public IActionResult Update(int id)
    {
        if(isAdmin() || isOperador())
        {
            var tablero = repoTableroC.ObtenerTableroID(id);
            var tableroVM = new TableroViewModel(tablero);
             return View(tableroVM);
        }
        return RedirectToRoute(new { controller = "Login", action = "Index"});
    }
    [HttpPost]
    public IActionResult Update(TableroViewModel tableroVM)
    {
        if(isAdmin())
        {
            var tablero = new Tablero(tableroVM);
            repoTableroC.ModificarTablero(tablero.Id, tablero);
            return RedirectToAction("Listar");
        }
        else
        {
            if(isOperador() && tableroVM.Id_usuario_propietario == Convert.ToInt32(HttpContext.Session.GetString("Id")))
            {
                var tablero = new Tablero(tableroVM);
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
