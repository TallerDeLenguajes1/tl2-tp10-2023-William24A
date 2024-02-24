using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_William24A.Models;


namespace MVC.ViewModels
{
    public class ListarTareaViewModel
    {
        public string NombreTablero { get; set; }
        public string UsuarioPropietario { get; set; }
        public int Id_tablero { get; set; }
        public List<TareaViewModel> TareasVM { get; set; }

        public ListarTareaViewModel(List<Tarea> tareas, List<Usuario> usuarios, TableroViewModel tablero)
        {
            TareasVM = new List<TareaViewModel>();
            NombreTablero = tablero.Nombre;
            Id_tablero = tablero.Id;
            UsuarioPropietario = usuarios.FirstOrDefault(u => u.Id == tablero.IdUsuarioPropietario)?.NombreUsuario;
            foreach (var t in tareas)
            {
                TareaViewModel tareaVM = new TareaViewModel(t);
                if(tareaVM.IdUsuarioAsignado == null)
                {
                    tareaVM.NombreUsuarioAsignado = "Sin asignar";  
                }else
                {        
                    tareaVM.NombreUsuarioAsignado = usuarios.FirstOrDefault(u => u.Id == tareaVM.IdUsuarioAsignado)?.NombreUsuario;
                }
                TareasVM.Add(tareaVM);
            }
        }

        public ListarTareaViewModel(){ }      
    }
}