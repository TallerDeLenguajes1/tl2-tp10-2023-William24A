using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_William24A.Models;


namespace MVC.ViewModels
{
    public class TableroViewModel
    {
        
        public int Id { get; set; }
        public int IdUsuarioPropietario { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string NombreUsuarioPropietario { get; set; }
        public bool Modificable { get ; set; }
        public TableroViewModel()
        {
        }
        public TableroViewModel(Tablero tablero)
        {
            Id = tablero.Id;
            IdUsuarioPropietario = tablero.Id_usuario_propietario;
            Nombre = tablero.Nombre;
            Descripcion = tablero.Descripcion;
        }
        public TableroViewModel(Tablero tablero, string nombrePropietario)
        {
            Id = tablero.Id;
            IdUsuarioPropietario = tablero.Id_usuario_propietario;
            Nombre = tablero.Nombre;
            Descripcion = tablero.Descripcion;
            NombreUsuarioPropietario = nombrePropietario;
        }        
    }
}