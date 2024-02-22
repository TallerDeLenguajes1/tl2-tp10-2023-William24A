using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_William24A.Models;


namespace MVC.ViewModels
{
    public class CrearUsuarioViewModel
    {
        [Required(ErrorMessage = "Este campo es requerido.")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Este campo debe contener entre 3 y 10 caracteres alfanumericos.")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Este campo debe contener solo letras y números.")]
        [Remote(action: "VerifyUserName", controller: "Usuario")]
        [Display(Name = "Nombre de usuario")] 
        public string NombreUsuario {get;set;}

        [Required(ErrorMessage = "Este campo es requerido.")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Este campo debe contener entre 3 y 10 caracteres alfanumericos.")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Este campo debe contener solo letras y números.")]
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