using MVC.ViewModels;
namespace tl2_tp10_2023_William24A.Models
{
    public enum EstadoTarea
    {
         Ideas, 
         ToDo, 
         Doing, 
         Review, 
         Done
    }
    public class Tarea
    {
        private int id;
        private int? idTablero;
        private string? nombre;
        private string? descripcion;
        private string? color;
        private EstadoTarea estado;
        private int? idUsuarioAsignado;

        public int Id { get => id; set => id = value; }
        public int? IdTablero { get => idTablero; set => idTablero = value; }
        public string? Nombre { get => nombre; set => nombre = value; }
        public string? Descripcion { get => descripcion; set => descripcion = value; }
        public string? Color { get => color; set => color = value; }
        public EstadoTarea Estado { get => estado; set => estado = value; }
        public int? IdUsuarioAsignado1 { get => idUsuarioAsignado; set => idUsuarioAsignado = value; }

        public Tarea()
        {

        }

        public Tarea(CrearTareaViewModel tareaVM)
        {
            IdTablero = tareaVM.IdTablero;
            Nombre = tareaVM.Nombre;
            Descripcion = tareaVM.Descripcion;
            Color = tareaVM.Color;
            Estado = tareaVM.Estado;
            IdUsuarioAsignado1 = tareaVM.IdUsuarioAsignado1;
        }
        public Tarea(TareaViewModel tareaVM)
        {
            Id = tareaVM.Id;
            IdTablero = tareaVM.Id_tablero;
            Nombre = tareaVM.Nombre;
            Descripcion = tareaVM.Descripcion;
            Color = tareaVM.Color;
            Estado = tareaVM.Estado;
            IdUsuarioAsignado1 = tareaVM.IdUsuarioAsignado;
        }
        public Tarea(ActualizarTareaViewModel tareaVM)
        {
            Id = tareaVM.Id;
            IdTablero = tareaVM.IdTablero;
            Nombre = tareaVM.Nombre;
            Descripcion = tareaVM.Descripcion;
            Color = tareaVM.Color;
            Estado = tareaVM.Estado;
            IdUsuarioAsignado1 = tareaVM.IdUsuarioAsignado1;
        }
    }
    
}