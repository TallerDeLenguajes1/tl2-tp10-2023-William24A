using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_William24A.Models;


namespace MVC.ViewModels
{
    public class ActualizarTableroViewModel
    {
        
        [Required] 
        public int Id {get;set;}
        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "ID usuario asignado")] 
        public int Id_usuario_propietario {get;set;}
        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Nombre")]
        public string? Nombre {get;set;}
        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Descripcion")]
        public string? Descripcion {get;set;}
        public List<Usuario> Usuarios {get;set;}
        
        public ActualizarTableroViewModel()
        {
            Usuarios = new List<Usuario>();
        }
        public ActualizarTableroViewModel(Tablero tablero)
        {
            Id = tablero.Id;
            Id_usuario_propietario = tablero.Id_usuario_propietario;
            Nombre = tablero.Nombre;
            Descripcion = tablero.Descripcion;
            Usuarios = new List<Usuario>();
        }        
    }
}