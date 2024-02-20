using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_William24A.Models;


namespace MVC.ViewModels
{
    public class MiTareaViewModel
    {
        public int? IdTablero {get;set;}
        public string NombreTablero {get;set;}
        public int Id {get;set;}
        public string? Nombre {get;set;}
        public string? Descripcion  {get;set;}
        public string? Color {get;set;}
        public EstadoTarea Estado {get;set;}
        
        
        public MiTareaViewModel()
        {
            
        }
        public MiTareaViewModel(Tarea tarea, Tablero tablero)
        {
            IdTablero = tarea.IdTablero;
            NombreTablero = tablero.Nombre;
            Id = tarea.Id;
            Nombre = tarea.Nombre;
            Descripcion = tarea.Descripcion;
            Color = tarea.Color;
            Estado = tarea.Estado;
        }        
    }
}