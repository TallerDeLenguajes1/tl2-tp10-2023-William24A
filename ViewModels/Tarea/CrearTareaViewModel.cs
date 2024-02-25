using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_William24A.Models;


namespace MVC.ViewModels
{
    public class CrearTareaViewModel
    {
        public List<Usuario> Usuarios {get;set;}
        public string? Tablero {get;set;}
        public int IdTablero {get;set;}
        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Nombre")] 
        public string Nombre {get;set;}
        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Descripcion")] 
        public string Descripcion  {get;set;}
        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Color")] 
        public string Color {get;set;}
        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Estado")] 
        public EstadoTarea Estado {get;set;}
        public int? IdUsuarioAsignado1  {get;set;}
        
        public CrearTareaViewModel()
        {
            Usuarios = new List<Usuario>();
        }
        public CrearTareaViewModel(Tarea tarea)
        {
            IdTablero = tarea.IdTablero;
            Nombre = tarea.Nombre;
            Descripcion = tarea.Descripcion;
            Color = tarea.Color;
            Estado = tarea.Estado;
            IdUsuarioAsignado1 = tarea.IdUsuarioAsignado1;
            Usuarios = new List<Usuario>();
        } 

        public CrearTareaViewModel(List<Usuario> usuarios, string tableros)
        {
            Usuarios = usuarios;
            Tablero = tableros;
        }        
    }
}