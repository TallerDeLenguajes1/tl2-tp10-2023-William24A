
namespace tl2_tp10_2023_William24A.Models
{
    public interface IDTareaRepositorio
    {
        public Tarea CreaTarea(Tarea tarea); 
        public void Modificar(int id, Tarea tarea);
        public Tarea BuscarPorId(int id);
        public List<Tarea> BuscarTodasTarea(int idUsuario);
        public List<Tarea> BuscarTareasTablero(int idTablero);
        public void DeleteTarea(int idTarea);
        public void AsignarUsuTarea(int idUsuario, int idTarea);
    }
}

/*
● Crear una nueva tarea en un tablero. (recibe un idTablero, devuelve un objeto Tarea)
● Modificar una tarea existente. (recibe un id y un objeto Tarea)
● Obtener detalles de una tarea por su ID. (devuelve un objeto Tarea)
● Listar todas las tareas asignadas a un usuario específico.(recibe un idUsuario,
devuelve un list de tareas)
● Listar todas las tareas de un tablero específico. (recibe un idTablero, devuelve un list
de tareas)
● Eliminar una tarea (recibe un IdTarea)
● Asignar Usuario a Tarea (recibe idUsuario y un idTarea)

*/