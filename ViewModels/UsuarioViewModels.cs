using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_William24A.Models;


namespace MVC.ViewModels
{
    public class UsuarioViewModel
    {
        
        public int Id {get;set;}
        public string NombreUsuario {get;set;}
        public Tipo Tipo {get;set;}
        
        public UsuarioViewModel()
        {

        }
        public UsuarioViewModel(Usuario usuario)
        {
            Id = usuario.Id;
            NombreUsuario = usuario.NombreUsuario;
            Tipo = usuario.Tipo;
        }        
    }
}