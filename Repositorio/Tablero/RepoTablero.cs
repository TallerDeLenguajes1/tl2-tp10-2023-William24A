using System.Data.SQLite;

namespace tl2_tp10_2023_William24A.Models
{
    public class RepoTableroC : IDtableroRepositorio
    {
         private readonly string cadenaConexion;
        public RepoTableroC(string cadenaConexion)
        {
            this.cadenaConexion = cadenaConexion;
        }
        public Tablero CrearTablero(Tablero tablero) //modificacion preguntar
        {
            string query = $"INSERT INTO Tablero(id_usuario_propietario,nombre,descripcion) VALUES(@id_usuario, @nombre,@descripcion);";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);
                command.Parameters.Add(new SQLiteParameter("id_usuario", tablero.Id_usuario_propietario));
                command.Parameters.Add(new SQLiteParameter("@nombre", tablero.Nombre));
                command.Parameters.Add(new SQLiteParameter("@descripcion", tablero.Descripcion));
                command.ExecuteNonQuery();
                connection.Close();
            }
            return tablero;
        }

        public void DeleteTablero(int idTablero)
        {
             using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();

                using (SQLiteCommand pragmaCommand = new SQLiteCommand("PRAGMA foreign_keys = 1;", connection))
                {
                    pragmaCommand.ExecuteNonQuery();
                }

                using (SQLiteCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"DELETE FROM Tablero WHERE id = @id;";
                    command.Parameters.Add(new SQLiteParameter("@id", idTablero));
                    int rowsAffected = command.ExecuteNonQuery();
                    connection.Close();

                    if (rowsAffected == 0)
                    {
                        throw new Exception("Tablero a eliminar no existe");
                    }
                }
                
            }
        }

        public List<Tablero> ListarTableros()
        {
            var queryString = $"SELECT * FROM Tablero;";
            List<Tablero> tableros = new List<Tablero>();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                connection.Open();
            
                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tablero = new Tablero();
                        tablero.Id = Convert.ToInt32(reader["id"]);
                        tablero.Id_usuario_propietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                        tablero.Nombre = reader["nombre"].ToString();
                        tablero.Descripcion = reader["descripcion"].ToString();
                        tableros.Add(tablero);
                    }
                }
                connection.Close();
            }
            if(tableros == null)
            {
                throw new Exception("Tableros no encontrados.");

            }
            return tableros;
        }

        public List<Tablero> ListarTablerosUsuario(int idUsuario)
        {
            var queryString = $"SELECT * FROM Tablero WHERE id_usuario_propietario = @idUsuario;";
            List<Tablero> tableros = new List<Tablero>();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@idUsuario", idUsuario));
                connection.Open();
            
                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                            var tablero = new Tablero();
                            tablero.Id = Convert.ToInt32(reader["id"]);
                            tablero.Id_usuario_propietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                            tablero.Nombre = reader["nombre"].ToString();
                            tablero.Descripcion = reader["descripcion"].ToString();
                            tableros.Add(tablero);
                    }
                }
                connection.Close();
            }
            return tableros;
        }

        public void ModificarTablero(int id, Tablero tablero)
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = $"UPDATE Tablero SET id_usuario_propietario = @idUsuario, nombre = @nombre, descripcion = @descripcion  WHERE id = @id;";
            command.Parameters.Add(new SQLiteParameter("@id", id));
            command.Parameters.Add(new SQLiteParameter("@idUsuario", tablero.Id_usuario_propietario));
            command.Parameters.Add(new SQLiteParameter("@nombre", tablero.Nombre));
            command.Parameters.Add(new SQLiteParameter("@descripcion", tablero.Descripcion));

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public Tablero ObtenerTableroID(int id)
        {
             SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            var tablero = new Tablero();
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Tablero WHERE id = @idTablero";
            command.Parameters.Add(new SQLiteParameter("@idTablero", id));
            connection.Open();
            using(SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    tablero.Id = Convert.ToInt32(reader["id"]);
                    tablero.Id_usuario_propietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                    tablero.Nombre = reader["nombre"].ToString();
                    tablero.Descripcion = reader["descripcion"].ToString();
                }
            }
            connection.Close();

            return tablero;
        }
    }

}