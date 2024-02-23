using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_William24A.Models;


namespace MVC.ViewModels
{
    public class ListarTableroViewModel
    {
        public List<TableroViewModel> MisTablerosVM{ get; set;}
        public List<TableroViewModel> TablerosTareasVM{ get; set;}
        public List<TableroViewModel> TodosTablerosVM{ get; set;}
        public List<UsuarioViewModel> usuarios{get; set; }
        public ListarTableroViewModel(List<Tablero> todosTableros, List<Usuario> usuarios)
        {
            TodosTablerosVM = new List<TableroViewModel>();   
            foreach (var t in todosTableros)
            {
                TableroViewModel tableroVM = new TableroViewModel(t);
                tableroVM.NombreUsuarioPropietario = usuarios.FirstOrDefault(u => u.Id == tableroVM.IdUsuarioPropietario)?.NombreUsuario;
                tableroVM.Modificable = true;
                TodosTablerosVM.Add(tableroVM);
            }
            MisTablerosVM = new List<TableroViewModel>();
            TablerosTareasVM = new List<TableroViewModel>();
        }  

        
        public ListarTableroViewModel(List<Tablero> misTableros, List<Tablero> tablerosTarea, List<Usuario> usuarios)
        {
            TodosTablerosVM = new List<TableroViewModel>();   
            
            MisTablerosVM = new List<TableroViewModel>();
            foreach (var t in misTableros)
            {
                TableroViewModel tableroVM = new TableroViewModel(t);
                tableroVM.NombreUsuarioPropietario = usuarios.FirstOrDefault(u => u.Id == tableroVM.IdUsuarioPropietario)?.NombreUsuario;;

                tableroVM.Modificable = true;
                MisTablerosVM.Add(tableroVM);
            }

            TablerosTareasVM = new List<TableroViewModel>();
            foreach (var t in tablerosTarea)
            {
                TableroViewModel tableroVM = new TableroViewModel(t);
                tableroVM.NombreUsuarioPropietario = usuarios.FirstOrDefault(u => u.Id == tableroVM.IdUsuarioPropietario)?.NombreUsuario;

                tableroVM.Modificable = false;
                TablerosTareasVM.Add(tableroVM);
            }

        }

        public ListarTableroViewModel(){}      
    }
}