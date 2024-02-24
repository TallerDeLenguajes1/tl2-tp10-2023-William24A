using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_William24A.Models;


namespace MVC.ViewModels
{
     public class TareaViewModel
    {
        public int Id {get;set;}
        public int? Id_tablero {get;set;}
        public string Nombre {get;set;}
        public string Descripcion {get;set;}
        public string Color {get;set;}
        public EstadoTarea Estado {get;set;}
        public int? IdUsuarioAsignado {get;set;}
        public string NombreUsuarioAsignado {get;set;}
        public string NombreTablero {get;set;}
        public bool Modificable {get;set;}


        public TareaViewModel(Tarea tarea)
        {
            Id = tarea.Id;
            Id_tablero = tarea.IdTablero;
            Nombre = tarea.Nombre;
            Descripcion = tarea.Descripcion;
            Color = tarea.Color;
            Estado = tarea.Estado;
            IdUsuarioAsignado = tarea.IdUsuarioAsignado1;
        }

        public TareaViewModel()
        {
        }
    }
}