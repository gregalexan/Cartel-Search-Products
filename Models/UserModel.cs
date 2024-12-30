using MySqlConnector;
using System.Data;

namespace Cartel_Search_Products.Models
{
    public class UserModel
    {
        private readonly MySqlConnection _connection;

        public UserModel(MySqlConnection connection)
        {
            _connection = connection;
        }

        public User authenticate(string username, string password)
        {
            const string query = "SELECT * FROM user WHERE username = @username AND password = @password;";

            using (_connection)
            {
                try
                {
                    using var cmd = new MySqlCommand(query, _connection);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    _connection.Open();

                    using var reader = cmd.ExecuteReader();

                    if (!reader.Read())
                    {
                        throw new Exception("Wrong username or password");
                    }

                    return new User
                    {
                        name = reader.GetString("name"),
                        email = reader.GetString("email"),
                        ssn = reader.GetString("ssn"),
                        username = username,
                        password = password,
                        phone = reader.GetString("phone"),
                        city = reader.GetString("city"),
                        address = reader.GetString("address"),
                        zip = reader.GetInt32("zip"),
                        image = reader.GetString("image"),
                        joined = reader.GetDateTime("joined")
                    };
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}