using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_William24A.Models;


namespace MVC.ViewModels
{
    public class ActualizarUsuarioViewModel
    {
        [Required] 
        public int Id {get;set;}
        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Nombre")] 
        public string NombreUsuario {get;set;}
        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Tipo")] 
        public Tipo Tipo {get;set;}
        
        public ActualizarUsuarioViewModel()
        {

        }
        public ActualizarUsuarioViewModel(Usuario usuario)
        {
            Id = usuario.Id;
            NombreUsuario = usuario.NombreUsuario;
            Tipo = usuario.Tipo;
        }        
    }
}