using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_William24A.Models;


namespace MVC.ViewModels
{
    public class CrearUsuarioViewModel
    {
        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Nombre")] 
        public string NombreUsuario {get;set;}

        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Contraseña")] 
        public string Contrasenia {get;set;}

        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Tipo")] 
        public Tipo Tipo {get;set;}
        
        public CrearUsuarioViewModel()
        {

        }
        public CrearUsuarioViewModel(Usuario usuario)
        {
            NombreUsuario = usuario.NombreUsuario;
            Contrasenia = usuario.Contrasenia;
            Tipo = usuario.Tipo;
        }        
    }
}