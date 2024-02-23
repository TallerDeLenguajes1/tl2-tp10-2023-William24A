using tl2_tp10_2023_William24A.Models;


namespace MVC.ViewModels
{
    public class ListarUsuarioViewModel
    {
        public List<UsuarioViewModel> UsuariosViewModels {get;set;}
        public ListarUsuarioViewModel()
        {
            UsuariosViewModels = new List<UsuarioViewModel>();
        }
        public ListarUsuarioViewModel(List<Usuario> usuarios)
        {
            UsuariosViewModels = new List<UsuarioViewModel>();
            foreach (var usuario in usuarios)
            {
                var usuarioViewModel = new UsuarioViewModel(usuario);
                UsuariosViewModels.Add(usuarioViewModel);  
            }
          
        }       
    }
}