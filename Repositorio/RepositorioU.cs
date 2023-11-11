
namespace tl2_tp10_2023_William24A.Models
{
    public interface IDUsuarioRepository
    {
        public List<Usuario> GetAll();
        public Usuario GetById(int id);
        public void Create(Usuario usuario);
        public void Remove(int id);
        public void Update(int idUsuario, Usuario usuario);
    }
}

/*
Crear un nuevo usuario. (recibe un objeto Usuario)
● Modificar un usuario existente. (recibe un Id y un objeto Usuario)
● Listar todos los usuarios registrados. (devuelve un List de Usuarios)
● Obtener detalles de un usuario por su ID. (recibe un Id y devuelve un Usuario)
● Eliminar un usuario por ID

*/