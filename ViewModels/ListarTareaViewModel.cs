using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_William24A.Models;


namespace MVC.ViewModels
{
    public class ListarTareaViewModel
    {
        public List<TareaViewModel> TareasViewModels {get;set;}
        public ListarTareaViewModel()
        {
            TareasViewModels = new List<TareaViewModel>();
        }
        public ListarTareaViewModel(List<Tarea> tareas)
        {
            TareasViewModels = new List<TareaViewModel>();
            foreach (var tarea in tareas)
            {
                var tareaViewModel = new TareaViewModel(tarea);
                TareasViewModels.Add(tareaViewModel);  
            }
          
        }        
    }
}