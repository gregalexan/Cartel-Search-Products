using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Cartel_Search_Products.Models
{
    public class ReviewModel
    {
        private readonly MySqlConnection _connection;

        // Inject MySqlConnection
        public ReviewModel(MySqlConnection connection)
        {
            _connection = connection;
        }

        // Get the reviews of a product
        public List<Review> getProductReviews(Product product)
        {
            List<Review> reviews = new List<Review>();

            using (_connection)
            {
                _connection.Open();
                
                // Define the query with placeholders for parameters
                string query = @"
                        SELECT reviews.username, reviews.review, reviews.stars, user.name, user.image 
                        FROM product
                        JOIN reviews ON product.productID = reviews.productID
                        JOIN user ON user.username = reviews.username
                        WHERE reviews.productID = @ProductId";

                using (MySqlCommand command = new MySqlCommand(query, _connection))
                {
                    // Add the product ID as a parameter to prevent SQL injection
                    command.Parameters.AddWithValue("@ProductId", product.ProductID);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Populate the Review object from the result set
                            reviews.Add(new Review
                            {
                                review = reader["review"].ToString(),
                                companyName = reader["name"].ToString(),
                                companyImage = reader["image"].ToString(),
                                productID = product.ProductID,
                                stars = Convert.ToInt32(reader["stars"])
                            });
                        }
                    }
                }
            }
            return reviews;
        }

    }
}
