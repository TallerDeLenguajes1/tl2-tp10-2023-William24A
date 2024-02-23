using System.ComponentModel.DataAnnotations;

namespace MVC.ViewModels
{
    public class CambiarContraseñaViewModel
    {
        
        [Required(ErrorMessage = "Este campo es requerido.")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Este campo debe contener entre 3 y 10 caracteres alfanumericos.")]
        [Display(Name = "Contraseña")]
        public string Contrasenia {get;set;}
        [Required(ErrorMessage = "Este campo es requerido.")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Este campo debe contener entre 3 y 10 caracteres alfanumericos.")]
        [Display(Name = "Nueva contraseña")]
        public string NuevaContrasenia {get;set;}
        [Required(ErrorMessage = "Este campo es requerido.")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Este campo debe contener entre 3 y 10 caracteres alfanumericos.")]
        [Compare("NuevaContrasenia", ErrorMessage = "La nueva contraseña y la confirmación no coinciden.")]
        public string ConfirmarContraseña { get; set; }
        
        public CambiarContraseñaViewModel()
        {

        }     
    }
}