using UtilizarTablero;

namespace RepoTableroU
{
    public interface IDtableroRepositorio
    {
        public Tablero CrearTablero(Tablero tablero);
        public void ModificarTablero(int id, Tablero tablero);
        public Tablero ObtenerTableroID(int id);
        public List<Tablero> ListarTableros();
        public List<Tablero> ListarTablerosUsuario(int idUsuario);
        public void DeleteTablero(int idTablero);
    }
}

/*
Crear un nuevo tablero (devuelve un objeto Tablero)
● Modificar un tablero existente (recibe un id y un objeto Tablero)
● Obtener detalles de un tablero por su ID. (recibe un id y devuelve un Tablero)
● Listar todos los tableros existentes (devuelve un list de tableros)
● Listar todos los tableros de un usuario específico. (recibe un IdUsuario, devuelve un
list de tableros)
● Eliminar un tablero por ID
*/