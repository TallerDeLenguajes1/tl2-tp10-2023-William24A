using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_William24A.Models;


namespace MVC.ViewModels
{
    public class ListarUsuarioViewModel
    {
        
        public string NombreUsuario {get;set;}
        public string Tipo {get;set;}
        public ListarUsuarioViewModel()
        {

        }
        public ListarUsuarioViewModel(Usuario usuario)
        {
            NombreUsuario = usuario.NombreUsuario;
            Tipo = usuario.Tipo.ToString();
        }        
    }
}