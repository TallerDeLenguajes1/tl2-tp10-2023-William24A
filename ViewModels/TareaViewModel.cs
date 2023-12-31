using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_William24A.Models;


namespace MVC.ViewModels
{
    public class TareaViewModel
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
        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "ID Usuario asignado")]
        public int? IdUsuarioAsignado1  {get;set;}
        
        public TareaViewModel()
        {

        }
        public TareaViewModel(Tarea tarea)
        {
            Id = tarea.Id;
            IdTablero = tarea.IdTablero;
            Nombre = tarea.Nombre;
            Descripcion = tarea.Descripcion;
            Color = tarea.Color;
            Estado = tarea.Estado;
            IdUsuarioAsignado1 = tarea.IdUsuarioAsignado1;
        }        
    }
}