namespace UtilizarTablero
{
    public class Tablero
    {
        private int? id; 
        private int id_usuario_propietario;
        private string? nombre;
        private string? descripcion;

        public int? Id { get => id; set => id = value; }
        public int Id_usuario_propietario { get => id_usuario_propietario; set => id_usuario_propietario = value; }
        public string? Nombre { get => nombre; set => nombre = value; }
        public string? Descripcion { get => descripcion; set => descripcion = value; }
    }
}