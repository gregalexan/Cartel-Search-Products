using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Cartel_Search_Products.Models
{
    // Equivalent ReviewService.java 
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

        // Post Review to Database
        [HttpPost]
        public void postReview(Review review)
        {
            Console.WriteLine($"ProductID: {review.productID}");
            Console.WriteLine($"CompanyName: {review.companyName}");
            Console.WriteLine($"Stars: {review.stars}");
            Console.WriteLine($"Review: {review.review}");

            using (_connection)
            {
                _connection.Open();

                // Query to post the review
                string query = "INSERT INTO reviews (stars, review, productID, username)" +
                    "VALUES (@stars, @review, @productID, @username);";

                using (MySqlCommand command = new MySqlCommand(query, _connection))
                {
                    command.Parameters.AddWithValue("@stars", review.stars);
                    command.Parameters.AddWithValue("@review", review.review);
                    command.Parameters.AddWithValue("@productID", review.productID);
                    command.Parameters.AddWithValue("@username", review.companyName);

                    command.ExecuteNonQuery();
                }
            }
        }

    }
}
