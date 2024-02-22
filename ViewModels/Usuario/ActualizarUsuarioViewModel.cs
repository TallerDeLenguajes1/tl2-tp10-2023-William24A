using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_William24A.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace MVC.ViewModels
{
    public class ActualizarUsuarioViewModel
    {
        [Required] 
        public int Id {get;set;}
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
        [PasswordPropertyText]
        public string Contrasenia{get;set;}
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