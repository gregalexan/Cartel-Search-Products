using System.Text;
using MySqlConnector;

namespace Cartel_Search_Products.Models
{
    // Equivalent ProductService.java 
    public class ProductModel
    {
        public readonly MySqlConnection _connection;
        public ProductModel(MySqlConnection connection)
        {
            _connection = connection;
        }
        // Set product rating for now
        public void SetProductRating(Product product, List<Review> reviews)
        {
            //ReviewModel rm = new ReviewModel(_connection);
            //List<Review> reviews = rm.getProductReviews(product);
            int average = 0;
            int total_stars = 0;
            foreach (Review review in reviews)
            {
                total_stars += review.stars;
            }
            if (reviews.Count > 0)
            {
                average = (int)Math.Ceiling((double)total_stars / reviews.Count);
            }
            else
            {
                average = 0;
            }
            product.rating = average;
        }

        // Get Products from database based on keywords and categories
        public List<Product> viewProducts(List<string> keywords, bool isCategory)
        {
            var products = new List<Product>();

            using (_connection)
            {
                _connection.Open();
                MySqlCommand command;
                string query;

                if (keywords.Count == 1 && keywords[0].Equals("all"))
                {
                    // Fetch all products with average rating
                    query = @"
                        SELECT 
                            product.productID,
                            product.productName,
                            product.image,
                            product.category,
                            product.description,
                            product.price,
                            product.stock,
                            product.supplier,
                            COALESCE(AVG(reviews.stars), 0) AS averageRating
                        FROM 
                            product
                        LEFT JOIN 
                            reviews ON product.productID = reviews.productID
                        GROUP BY 
                            product.productID, 
                            product.productName, 
                            product.image, 
                            product.category, 
                            product.description, 
                            product.price, 
                            product.stock, 
                            product.supplier;
                    ";
                    command = new MySqlCommand(query, _connection);
                }
                else if (isCategory)
                {
                    // Search by category with average rating
                    query = @"
                        SELECT 
                            product.productID,
                            product.productName,
                            product.image,
                            product.category,
                            product.description,
                            product.price,
                            product.stock,
                            product.supplier,
                            COALESCE(AVG(reviews.stars), 0) AS averageRating
                        FROM 
                            product
                        LEFT JOIN 
                            reviews ON product.productID = reviews.productID
                        WHERE 
                            product.category = @category
                        GROUP BY 
                            product.productID, 
                            product.productName, 
                            product.image, 
                            product.category, 
                            product.description, 
                            product.price, 
                            product.stock, 
                            product.supplier;
                    ";
                    command = new MySqlCommand(query, _connection);
                    command.Parameters.AddWithValue("@category", keywords[0]);
                }
                else
                {
                    // Build query for keyword search with average rating
                    var queryBuilder = new StringBuilder(@"
                        SELECT 
                            product.productID,
                            product.productName,
                            product.image,
                            product.category,
                            product.description,
                            product.price,
                            product.stock,
                            product.supplier,
                            COALESCE(AVG(reviews.stars), 0) AS averageRating
                        FROM 
                            product
                        LEFT JOIN 
                            reviews ON product.productID = reviews.productID
                        WHERE 
                    ");

                    var parameters = new List<MySqlParameter>();

                    for (int i = 0; i < keywords.Count; i++)
                    {
                        if (i > 0)
                        {
                            queryBuilder.Append(" OR ");
                        }

                        string paramNameBase = $"@keyword{i}";
                        queryBuilder.Append($"product.productName LIKE {paramNameBase}p OR ")
                                    .Append($"product.category LIKE {paramNameBase}c OR ")
                                    .Append($"product.description LIKE {paramNameBase}d OR ")
                                    .Append($"product.supplier LIKE {paramNameBase}s");

                        string wildcardKeyword = $"%{keywords[i]}%";
                        parameters.Add(new MySqlParameter($"{paramNameBase}p", wildcardKeyword));
                        parameters.Add(new MySqlParameter($"{paramNameBase}c", wildcardKeyword));
                        parameters.Add(new MySqlParameter($"{paramNameBase}d", wildcardKeyword));
                        parameters.Add(new MySqlParameter($"{paramNameBase}s", wildcardKeyword));
                    }

                    queryBuilder.Append(@"
                        GROUP BY 
                            product.productID, 
                            product.productName, 
                            product.image, 
                            product.category, 
                            product.description, 
                            product.price, 
                            product.stock, 
                            product.supplier;
                    ");

                    query = queryBuilder.ToString();
                    command = new MySqlCommand(query, _connection);
                    command.Parameters.AddRange(parameters.ToArray());
                }

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var product = new Product
                    {
                        ProductID = reader.GetInt32("productID"),
                        productName = reader.GetString("productName"),
                        image = reader.GetString("image"),
                        category = reader.GetString("category"),
                        description = reader.GetString("description"),
                        price = reader.GetDouble("price"),
                        stock = reader.GetInt32("stock"),
                        supplier = reader.GetString("supplier"),
                        rating = reader.GetInt32("averageRating") // Populate the rating directly from query
                    };

                    products.Add(product);
                }
            }

            _connection.Close();
            return products;
        }

        // Sort products
        public List<Product> SortProducts(List<Product> products, string sort)
        {
            if (sort == "price")
            {
                // Sort by price in ascending order
                products.Sort((a, b) => a.price.CompareTo(b.price));
            }
            else if (sort == "rating")
            {
                // Sort by rating in descending order
                products.Sort((a, b) => b.rating.CompareTo(a.rating));
            }

            return products;
        }
    }
}
