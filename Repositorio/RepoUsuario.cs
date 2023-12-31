using System.Data.SQLite;

namespace tl2_tp10_2023_William24A.Models
{
    public class RepoUsuarioC : IDUsuarioRepository
    {
        private readonly string cadenaConexion;
        public RepoUsuarioC(string cadenaConexion)
        {
            this.cadenaConexion = cadenaConexion;
        }
        public void Create(Usuario usuario)
        {
            var query = $"INSERT INTO Usuario (nombre_de_usuario, contrasenia, tipo) VALUES (@name,@contrasenia,@tipo);";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {

                connection.Open();
                var command = new SQLiteCommand(query, connection);

                command.Parameters.Add(new SQLiteParameter("@name", usuario.NombreUsuario));
                command.Parameters.Add(new SQLiteParameter("@contrasenia", usuario.Contrasenia));
                command.Parameters.Add(new SQLiteParameter("@tipo", usuario.Tipo));
                command.ExecuteNonQuery();

                connection.Close(); 
            }
        }

        public List<Usuario> GetAll()
        {
            var queryString = $"SELECT * FROM Usuario;";
            List<Usuario> usuarios = new List<Usuario>();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                connection.Open();
            
                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var usuario = new Usuario();
                        usuario.Id = Convert.ToInt32(reader["id"]);
                        usuario.NombreUsuario = reader["nombre_de_usuario"].ToString();
                        usuario.Contrasenia = reader["contrasenia"].ToString();
                        usuario.Tipo = (Tipo)Convert.ToInt32(reader["tipo"]);
                        usuarios.Add(usuario);
                    }
                }
                connection.Close();
            }
            if(usuarios == null)
                    throw new Exception("Usuarios no encontrados.");
            return usuarios;
        }

        public Usuario GetById(int id)
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            var usuario = new Usuario();
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Usuario WHERE id = @idUsuario";
            command.Parameters.Add(new SQLiteParameter("@idUsuario", id));
            connection.Open();
            using(SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    usuario.Id = Convert.ToInt32(reader["id"]);
                    usuario.NombreUsuario = reader["nombre_de_usuario"].ToString();
                    usuario.Contrasenia = reader["contrasenia"].ToString();
                    usuario.Tipo = (Tipo)Convert.ToInt32(reader["tipo"]);
                }
            }
            connection.Close();
            if(usuario == null)
                    throw new Exception("Usuario no encontrados.");
            return usuario;
        }

        public Usuario login(string nombre, string password)
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            Usuario usuario = null;
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Usuario WHERE nombre_de_usuario = @nombre AND contrasenia = @contrasenia ";
            command.Parameters.Add(new SQLiteParameter("@nombre", nombre));
            command.Parameters.Add(new SQLiteParameter("@contrasenia", password));
            connection.Open();
            using(SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    usuario = new Usuario();
                    usuario.Id = Convert.ToInt32(reader["id"]);
                    usuario.NombreUsuario = reader["nombre_de_usuario"].ToString();
                    usuario.Contrasenia = reader["contrasenia"].ToString();
                    usuario.Tipo = (Tipo)Convert.ToInt32(reader["tipo"]);
                }
            }
            connection.Close();
            if(usuario == null) throw new Exception("usuario no registrado");
            return usuario;
        }

        public void Remove(int id)
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = $"DELETE FROM Usuario WHERE id = @idUsuario;";
            command.Parameters.Add(new SQLiteParameter("@idUsuario", id));
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void Update(int idUsuario, Usuario usuario)
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = $"UPDATE Usuario SET nombre_de_usuario = @nombre, tipo = @tipo WHERE id = @idUsuario;";
            command.Parameters.Add(new SQLiteParameter("@idUsuario", idUsuario));
            command.Parameters.Add(new SQLiteParameter("@nombre",usuario.NombreUsuario));
            command.Parameters.Add(new SQLiteParameter("@tipo",usuario.Tipo));
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
