using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_William24A.Models;


namespace MVC.ViewModels
{
    public class ActualizarTableroViewModel
    {
        
        [Required] 
        public int Id {get;set;}
        
        [Required] 
        public int Id_usuario_propietario {get;set;}
        [Required(ErrorMessage = "Este campo es requerido.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Este campo debe contener entre 1 y 100 caracteres.")]
        [Display(Name = "Nombre")]
        public string? Nombre {get;set;}
        [Required(ErrorMessage = "Este campo es requerido.")]
        [StringLength(1000, MinimumLength = 1, ErrorMessage = "Este campo debe contener entre 1 y 1000 caracteres.")]
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

         public ActualizarTableroViewModel(Tablero tablero, List<Usuario> usuarios)
        {
            Id = tablero.Id;
            Id_usuario_propietario = tablero.Id_usuario_propietario;
            Nombre = tablero.Nombre;
            Descripcion = tablero.Descripcion;
            Usuarios = usuarios;
        }       
    }
}