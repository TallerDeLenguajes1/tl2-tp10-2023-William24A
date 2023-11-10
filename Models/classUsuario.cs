namespace UtilizarUsuario
{
    public class Usuario
    {
        private int? id;
        private string? nombreUsuario;

        public int? Id { get => id; set => id = value; }
        public string? NombreUsuario { get => nombreUsuario; set => nombreUsuario = value; }
    }
}