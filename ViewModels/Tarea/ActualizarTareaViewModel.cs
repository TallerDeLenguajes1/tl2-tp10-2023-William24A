using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_William24A.Models;


namespace MVC.ViewModels
{
    public class ActualizarTareaViewModel
    {
        [Required]
        public int Id {get;set;}
        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "ID Tablero")]
        public int? IdTablero {get;set;}
        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Nombre")]
        public string? Nombre {get;set;}
        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Descripcion")]
        public string? Descripcion  {get;set;}
        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Color de tarea")]
        public string? Color {get;set;}
        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Estado")]
        public EstadoTarea Estado {get;set;}
        [Display(Name = "ID Usuario asignado")]
        public int? IdUsuarioAsignado1  {get;set;}
        public List<Usuario> Usuarios {get;set;}
        public List<Tablero> Tableros {get;set;}
        public string Operador {get;set;}
        
        public ActualizarTareaViewModel()
        {
            Usuarios = new List<Usuario>();
            Tableros = new List<Tablero>();
            Operador = "";
        }
        public ActualizarTareaViewModel(Tarea tarea, List<Usuario> usuarios, List<Tablero> tableros)
        {
            Id = tarea.Id;
            IdTablero = tarea.IdTablero;
            Nombre = tarea.Nombre;
            Descripcion = tarea.Descripcion;
            Color = tarea.Color;
            Estado = tarea.Estado;
            IdUsuarioAsignado1 = tarea.IdUsuarioAsignado1;
            Operador = "";
            Usuarios = usuarios;
            Tableros = tableros;
        } 

        public ActualizarTareaViewModel(Tarea tarea, string operador)
        {
            Id = tarea.Id;
            IdTablero = tarea.IdTablero;
            Nombre = tarea.Nombre;
            Descripcion = tarea.Descripcion;
            Color = tarea.Color;
            Estado = tarea.Estado;
            IdUsuarioAsignado1 = tarea.IdUsuarioAsignado1;
            Usuarios = new List<Usuario>();
            Tableros = new List<Tablero>();
            Operador = operador;
        }         
    }
}