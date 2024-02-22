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
        public UsuarioViewModel(string nombre, string tipo, int id)
        {
            Id = id;
            NombreUsuario = nombre;
            Tipo = (Tipo)Enum.Parse(typeof(Tipo), tipo);
        }
        public UsuarioViewModel(Usuario usuario)
        {
            Id = usuario.Id;
            NombreUsuario = usuario.NombreUsuario;
            Tipo = usuario.Tipo;
        }        
    }
}