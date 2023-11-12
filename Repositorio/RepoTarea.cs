
using System.Data.SQLite;
using System.Data.SqlClient;


namespace tl2_tp10_2023_William24A.Models
{
    public class RepoTareaC : IDTareaRepositorio
    {
        private string cadenaConexion ="Data Source=DB/kanban.db;Cache=Shared";
        public void AsignarUsuTarea(int idUsuario, int idTarea)
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = $"UPDATE Tarea SET id_usuario_asignado = @idUsuarioAsignado WHERE id = @idTarea;";
            command.Parameters.Add(new SQLiteParameter("@idUsuarioAsignado", idUsuario));
            command.Parameters.Add(new SQLiteParameter("@idTarea",idTarea));
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public Tarea BuscarPorId(int id)
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            var tarea = new Tarea();
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Tarea WHERE id = @idTarea";
            command.Parameters.Add(new SQLiteParameter("@idTarea", id));
            connection.Open();
            using(SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    tarea.Id = Convert.ToInt32(reader["id"]);
                    tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                    tarea.Nombre = reader["nombre"].ToString();
                    tarea.Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]); 
                    tarea.Descripcion = reader["descripcion"].ToString();
                    tarea.Color = reader["color"].ToString();
                    tarea.IdUsuarioAsignado1 = Convert.ToInt32(reader["id_usuario_asignado"]);
                }
            }
            connection.Close();

            return tarea;
        }

        public List<Tarea> BuscarTareasTablero(int idTablero)
        {
            var queryString = $"SELECT * FROM Tarea WHERE id_tablero = @idTablero;";
            List<Tarea> tareas = new List<Tarea>();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@idTablero", idTablero));
                connection.Open();
            
                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                            var tarea = new Tarea();
                            tarea.Id = Convert.ToInt32(reader["id"]);
                            tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                            tarea.Nombre = reader["nombre"].ToString();
                            tarea.Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]); 
                            tarea.Descripcion = reader["descripcion"].ToString();
                            tarea.Color = reader["color"].ToString();
                            tarea.IdUsuarioAsignado1 = reader["id_usuario_asignado"] as int?;
                            tareas.Add(tarea);
                    }
                }
                connection.Close();
            }
            return tareas;
        }

        public List<Tarea> BuscarTodasTarea(int idUsuario)
        {
            var queryString = $"SELECT * FROM Tarea WHERE id_usuario_asignado = @idUsuario;";
            List<Tarea> tareas = new List<Tarea>();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@idUsuario", idUsuario));
                connection.Open();
            
                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                            var tarea = new Tarea();
                            tarea.Id = Convert.ToInt32(reader["id"]);
                            tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                            tarea.Nombre = reader["nombre"].ToString();
                            tarea.Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]); 
                            tarea.Descripcion = reader["descripcion"].ToString();
                            tarea.Color = reader["color"].ToString();
                            tarea.IdUsuarioAsignado1 = Convert.ToInt32(reader["id_usuario_asignado"]);
                            tareas.Add(tarea);
                    }
                }
                connection.Close();
            }
            return tareas;
        }

        public Tarea CreaTarea(Tarea tarea) //Consultar si es que esta modificacion esta bien
        {
            var query = $"INSERT INTO Tarea(id_tablero,nombre, estado, descripcion,color, id_usuario_asignado) VALUES(@idTablero, @nombre_tarea, @estado, @descripcion, @color, @idusuario );";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("@idTablero", tarea.IdTablero));
                command.Parameters.Add(new SQLiteParameter("@nombre_tarea", tarea.Nombre));
                command.Parameters.Add(new SQLiteParameter("@estado", tarea.Estado));
                command.Parameters.Add(new SQLiteParameter("@descripcion", tarea.Descripcion));
                command.Parameters.Add(new SQLiteParameter("@color", tarea.Color));
                command.Parameters.Add(new SQLiteParameter("@idusuario", tarea.IdUsuarioAsignado1));
                command.ExecuteNonQuery();
                connection.Close();
            }
            return tarea;
        }

        public void DeleteTarea(int idTarea)
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = $"DELETE FROM Tarea WHERE id = @idTarea;";
            command.Parameters.Add(new SQLiteParameter("@idTarea", idTarea));
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void Modificar(int id, Tarea tarea)
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = $"UPDATE Tarea SET nombre = @nombre, id_tablero = @idTablero, estado = @estado, descripcion = @descripcion, color = @color, id_usuario_asignado = @idusuario  WHERE id = @id;";
            command.Parameters.Add(new SQLiteParameter("@id", id));
            command.Parameters.Add(new SQLiteParameter("@nombre", tarea.Nombre));
            command.Parameters.Add(new SQLiteParameter("@idTablero", tarea.IdTablero));
            command.Parameters.Add(new SQLiteParameter("@estado", Convert.ToInt32(tarea.Estado)));
            command.Parameters.Add(new SQLiteParameter("@descripcion", tarea.Descripcion));
            command.Parameters.Add(new SQLiteParameter("@color", tarea.Color));
            command.Parameters.Add(new SQLiteParameter("@idusuario", tarea.IdUsuarioAsignado1));

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}