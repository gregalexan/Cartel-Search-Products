using MySqlConnector;
using System.Data;

namespace Cartel_Search_Products.Models
{
    // Equivalent UserService.java
    public class UserModel
    {
        private readonly MySqlConnection _connection;

        public UserModel(MySqlConnection connection)
        {
            _connection = connection;
        }

        // Authenticate the user 
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

        // Register the user
        public bool Register(User user)
        {
            const string checkQuery = "SELECT * FROM user WHERE username = @username OR email = @Email OR ssn = @SSN;";
            const string insertQuery = "INSERT INTO user (name, email, ssn, username, password, phone, city, address, zip, image, joined) " +
                                       "VALUES (@Name, @Email, @SSN, @Username, @Password, @Phone, @City, @Address, @Zip, @Image, @Joined);";

            using (_connection)
            {
                try
                {
                    _connection.Open();

                    // Check if the user already exists
                    using (var checkCmd = new MySqlCommand(checkQuery, _connection))
                    {
                        checkCmd.Parameters.AddWithValue("@Username", user.username);
                        checkCmd.Parameters.AddWithValue("@Email", user.email);
                        checkCmd.Parameters.AddWithValue("@SSN", user.ssn);

                        using var reader = checkCmd.ExecuteReader();
                        if (reader.HasRows) // If user already exists
                        {
                            throw new Exception("Sorry, username, email, or ssn already registered");
                        }
                    }

                    // Insert the new user
                    using (var insertCmd = new MySqlCommand(insertQuery, _connection))
                    {
                        user.joined = DateTime.Now;

                        insertCmd.Parameters.AddWithValue("@Name", user.name);
                        insertCmd.Parameters.AddWithValue("@Email", user.email);
                        insertCmd.Parameters.AddWithValue("@SSN", user.ssn);
                        insertCmd.Parameters.AddWithValue("@Username", user.username);
                        insertCmd.Parameters.AddWithValue("@Password", user.password);
                        insertCmd.Parameters.AddWithValue("@Phone", user.phone ?? string.Empty);
                        insertCmd.Parameters.AddWithValue("@City", user.city ?? string.Empty);
                        insertCmd.Parameters.AddWithValue("@Address", user.address ?? string.Empty);
                        insertCmd.Parameters.AddWithValue("@Zip", user.zip);
                        insertCmd.Parameters.AddWithValue("@Image", user.image ?? string.Empty);
                        insertCmd.Parameters.AddWithValue("@Joined", user.joined);

                        insertCmd.ExecuteNonQuery();
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}