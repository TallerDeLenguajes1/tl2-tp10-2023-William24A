using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_William24A.Models;


namespace MVC.ViewModels
{
    public class ListarTareaViewModel
    {
        public List<TareaViewModel> TareasViewModels {get;set;}
        public List<TareaViewModel> MyTareasViewModels {get;set;}
        public string Operador {get;set;}
        public ListarTareaViewModel()
        {
            TareasViewModels = new List<TareaViewModel>();
            MyTareasViewModels = new List<TareaViewModel>();
        }
        public ListarTareaViewModel(List<Tarea> tareas)
        {
            TareasViewModels = new List<TareaViewModel>();
            foreach (var tarea in tareas)
            {
                var tareaViewModel = new TareaViewModel(tarea);
                TareasViewModels.Add(tareaViewModel);  
            }
            MyTareasViewModels = new List<TareaViewModel>();
            Operador = "";
        } 

        public ListarTareaViewModel(List<Tarea> tareas, int id, string operador)
        {
            TareasViewModels = new List<TareaViewModel>();
            MyTareasViewModels = new List<TareaViewModel>();
            foreach (var tarea in tareas)
            {
                if(tarea.IdUsuarioAsignado1 == id)
                {
                    var mytareaViewModel = new TareaViewModel(tarea);
                    MyTareasViewModels.Add(mytareaViewModel);
                }else{
                    var tareaViewModel = new TareaViewModel(tarea);
                    TareasViewModels.Add(tareaViewModel);
                }
                  
            }
            Operador = operador;
          
        }       
    }
}