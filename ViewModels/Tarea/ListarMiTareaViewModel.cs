using tl2_tp10_2023_William24A.Models;


namespace MVC.ViewModels
{
    public class ListarMiTareaViewModel
    {
        public Usuario Usuario {get;set;}
        public List<MiTareaViewModel> TareasViewModels {get;set;}
        public string Operador {get;set;}
        public ListarMiTareaViewModel(List<Tarea> tareas, List<Tablero> tableros, Usuario usuario, string operador)
        {
            TareasViewModels = new List<MiTareaViewModel>();
            Usuario = usuario;
            Operador = operador;
            foreach (var tarea in tareas)
            {
                foreach(var tablero in tableros)
                {
                    if(tarea.IdTablero == tablero.Id)
                    {
                         var tareaViewModel = new MiTareaViewModel(tarea,tablero);
                         TareasViewModels.Add(tareaViewModel);
                    }
                }  
            }
        }        
    }
}