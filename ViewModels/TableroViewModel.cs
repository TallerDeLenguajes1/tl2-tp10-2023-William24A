using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_William24A.Models;


namespace MVC.ViewModels
{
    public class TableroViewModel
    {
        
        public int Id {get;set;}
        public int Id_usuario_propietario {get;set;}
        public string? Nombre {get;set;}
        public string? Descripcion {get;set;}
        
        public TableroViewModel()
        {

        }
        public TableroViewModel(Tablero tablero)
        {
            Id = tablero.Id;
            Id_usuario_propietario = tablero.Id_usuario_propietario;
            Nombre = tablero.Nombre;
            Descripcion = tablero.Descripcion;
        }        
    }
}