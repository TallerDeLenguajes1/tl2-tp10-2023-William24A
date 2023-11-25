using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_William24A.Models;


namespace MVC.ViewModels
{
    public class CrearTableroViewModel
    {
        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "ID usuario propietario")] 
        public int Id_usuario_propietario {get;set;}

        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Nombre de Tablero")] 
        public string? Nombre {get;set;}

        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Descripcion")] 
        public string? Descripcion {get;set;}
        public List<Usuario> Usuarios {get;set;}
        
        public CrearTableroViewModel()
        {

        }
        public CrearTableroViewModel(Tablero tablero)
        {
            Id_usuario_propietario = tablero.Id_usuario_propietario;
            Nombre = tablero.Nombre;
            Descripcion = tablero.Descripcion;
            Usuarios = new List<Usuario>();
        }        
    }
}