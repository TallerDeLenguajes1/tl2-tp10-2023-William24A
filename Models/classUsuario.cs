using MVC.ViewModels;

namespace tl2_tp10_2023_William24A.Models
{
    public enum Tipo
    {
        admin,
        operador
    }
    public class Usuario
    {
        private int id;
        private string? nombreUsuario;
        private string? contrasenia;
        private Tipo tipo;

        public int Id { get => id; set => id = value; }
        public string? NombreUsuario { get => nombreUsuario; set => nombreUsuario = value; }
        public string? Contrasenia { get => contrasenia; set => contrasenia = value; }
        public Tipo Tipo { get => tipo; set => tipo = value; }

        public Usuario()
        {

        }
        public Usuario(LoginViewModel usuar)
        {
            NombreUsuario = usuar.NombreUsuario;
            Contrasenia = usuar.Contrasenia;
        }
        public Usuario(CrearUsuarioViewModel usuar)
        {
            NombreUsuario = usuar.NombreUsuario;
            Contrasenia = usuar.Contrasenia;
            Tipo = usuar.Tipo;
        }
        public Usuario(UsuarioViewModel usuar)
        {
            Id = usuar.Id;
            NombreUsuario = usuar.NombreUsuario;
            Tipo = usuar.Tipo;
        }
        public Usuario(ActualizarUsuarioViewModel usuar)
        {
            Id = usuar.Id;
            NombreUsuario = usuar.NombreUsuario;
            Tipo = usuar.Tipo;
        }
    }
}