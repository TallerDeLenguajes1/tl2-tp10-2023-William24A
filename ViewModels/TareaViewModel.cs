using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_William24A.Models;


namespace MVC.ViewModels
{
    public class TareaViewModel
    {
        
        public int Id {get;set;}
        public int? IdTablero {get;set;}
        public string? Nombre {get;set;}
        public string? Descripcion  {get;set;}
        public string? Color {get;set;}
        public EstadoTarea Estado {get;set;}
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