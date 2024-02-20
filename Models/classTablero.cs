using MVC.ViewModels;

namespace tl2_tp10_2023_William24A.Models
{
    public class Tablero
    {
        private int id; 
        private int id_usuario_propietario;
        private string? nombre;
        private string? descripcion;

        public int Id { get => id; set => id = value; }
        public int Id_usuario_propietario { get => id_usuario_propietario; set => id_usuario_propietario = value; }
        public string? Nombre { get => nombre; set => nombre = value; }
        public string? Descripcion { get => descripcion; set => descripcion = value; }

        public Tablero()
        {

        }

        public Tablero(CrearTableroViewModel tableroVM)
        {
            Id_usuario_propietario = tableroVM.Id_usuario_propietario;
            Nombre = tableroVM.Nombre;
            Descripcion = tableroVM.Descripcion;
        }
        public Tablero(TableroViewModel tableroVM)
        {
            Id = tableroVM.Id;
            Id_usuario_propietario = tableroVM.Id_usuario_propietario;
            Nombre = tableroVM.Nombre;
            Descripcion = tableroVM.Descripcion;
        }
        public Tablero(ActualizarTableroViewModel tableroVM)
        {
            Id = tableroVM.Id;
            Id_usuario_propietario = tableroVM.Id_usuario_propietario;
            Nombre = tableroVM.Nombre;
            Descripcion = tableroVM.Descripcion;
        }
    }
}