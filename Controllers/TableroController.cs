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

    public TableroController(ILogger<TableroController> logger,IDtableroRepositorio repoTableroC, IDUsuarioRepository repoUsuarioC )
    {
        _logger = logger;
        _repoTableroC = repoTableroC;
        _repoUsuarioC = repoUsuarioC;
    }

    [HttpGet]
    public IActionResult Listar()
    {
        if(isAdmin())
        {
            var tableros = _repoTableroC.ListarTableros();
            var tablerosVM = new ListarTableroViewModel(tableros);
            return View(tablerosVM);
        }else{
            if(isOperador())
            {
                var tablerosU = _repoTableroC.ListarTablerosUsuario(Convert.ToInt32(HttpContext.Session.GetString("Id")));
                var tablerosUVM = new ListarTableroViewModel(tablerosU);
                return View(tablerosUVM);
            }
        }
        return RedirectToRoute(new { controller = "Home", action = "Index"});
    }

    [HttpGet]
    public IActionResult Create()
    {
        if(isAdmin() || isOperador())
        {
            CrearTableroViewModel crearTableroViewModel = new();
            crearTableroViewModel.Usuarios = _repoUsuarioC.GetAll();
            if (crearTableroViewModel.Usuarios == null) return NoContent();
            return View(crearTableroViewModel);
        }
        return RedirectToRoute(new { controller = "Login", action = "Index"});
    }
    [HttpPost]
    public IActionResult Create(Tablero tableroVM)
    {
        if(isAdmin()){
            //var tablero = new Tablero(tableroVM); 
            if(!ModelState.IsValid)
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                .Where(y=>y.Count>0)
                .ToList();
                return RedirectToAction("Create");
            }
            
            _repoTableroC.CrearTablero(tableroVM);
            return RedirectToAction("Listar");
        }
       /* else
        {
            if(!ModelState.IsValid) return RedirectToAction("Create");
            if(tableroVM.Id_usuario_propietario == Convert.ToInt32(HttpContext.Session.GetString("Id")) && isOperador())
            {
                var tablero = new Tablero(tableroVM); 
                _repoTableroC.CrearTablero(tablero);
                return RedirectToAction("Listar");
            }
        }*/
        return RedirectToRoute(new { controller = "Login", action = "Index"});
    }

    [HttpGet]
    public IActionResult Update(int id)
    {
        if(isAdmin())
        {
            var tablero = _repoTableroC.ObtenerTableroID(id);
            var tableroVM = new TableroViewModel(tablero);
            tableroVM.Usuarios = _repoUsuarioC.GetAll();
            if (tableroVM.Usuarios == null) return NoContent();
             return View(tableroVM);
        }
        else
        {
            if(isOperador() && _repoTableroC.ObtenerTableroID(id).Id_usuario_propietario == Convert.ToInt32(HttpContext.Session.GetString("Id")))
            {
                var tablero = _repoTableroC.ObtenerTableroID(id);
                var tableroVM = new TableroViewModel(tablero);
                return View(tableroVM);
            }
        }
        return RedirectToRoute(new { controller = "Tablero", action = "Listar"});
    }
    [HttpPost]
    public IActionResult Update(TableroViewModel tableroVM)
    {
        if(isAdmin())
        {
            if(!ModelState.IsValid) return RedirectToAction("Listar");
            var tablero = new Tablero(tableroVM);
            _repoTableroC.ModificarTablero(tablero.Id, tablero);
            return RedirectToAction("Listar");
        }
        else
        {
            if(!ModelState.IsValid) return RedirectToAction("Listar");
            if(isOperador() && tableroVM.Id_usuario_propietario == Convert.ToInt32(HttpContext.Session.GetString("Id")))
            {
                var tablero = new Tablero(tableroVM);
                _repoTableroC.ModificarTablero(tablero.Id, tablero);
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
            _repoTableroC.DeleteTablero(id);
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
