using tl2_tp10_2023_William24A.Models;


namespace MVC.ViewModels
{
    public class ListarMiTareaViewModel
    {
        public string NombreUsuarioAsignado { get; set; }
        public List<TareaViewModel> TareasVM { get; set; }

        public ListarMiTareaViewModel(List<Tarea> tareas, List<Tablero> tableros, Usuario usuario)
        {
            NombreUsuarioAsignado = usuario.NombreUsuario;
            TareasVM = new List<TareaViewModel>();
            foreach (var t in tareas)
            {
                TareaViewModel tareaVM = new TareaViewModel(t);  
                
                Tablero tableroVM =  tableros.FirstOrDefault(t => t.Id == tareaVM.Id_tablero);
                
                tareaVM.NombreTablero = tableroVM.Nombre;
                
                /*if(tableroVM.Id_usuario_propietario == usuario.Id)
                {
                    tareaVM.Modificable = true;
                }else
                {
                    tareaVM.Modificable = false;
                }*/
                TareasVM.Add(tareaVM);
            }
        }
    }
}