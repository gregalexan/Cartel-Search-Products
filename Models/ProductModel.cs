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
        public void SetProductRating(Product product)
        {
            // For now 
            if (product.price > 12)
            {
                product.rating = 5;
            }
            else
            {
                product.rating = 4;
            }
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
                    // Fetch all products
                    query = "SELECT * FROM product";
                    command = new MySqlCommand(query, _connection);
                }
                else if (isCategory)
                {
                    // Search by category
                    query = "SELECT * FROM product WHERE category = @category";
                    command = new MySqlCommand(query, _connection);
                    command.Parameters.AddWithValue("@category", keywords[0]);
                }
                else
                {
                    // Build query for keyword search
                    var queryBuilder = new StringBuilder("SELECT * FROM product WHERE ");
                    var parameters = new List<MySqlParameter>();

                    for (int i = 0; i < keywords.Count; i++)
                    {
                        if (i > 0)
                        {
                            queryBuilder.Append(" OR ");
                        }

                        string paramNameBase = $"@keyword{i}";
                        queryBuilder.Append($"productName LIKE {paramNameBase}p OR ")
                                  .Append($"category LIKE {paramNameBase}c OR ")
                                  .Append($"description LIKE {paramNameBase}d OR ")
                                  .Append($"supplier LIKE {paramNameBase}s");

                        string wildcardKeyword = $"%{keywords[i]}%";
                        parameters.Add(new MySqlParameter($"{paramNameBase}p", wildcardKeyword));
                        parameters.Add(new MySqlParameter($"{paramNameBase}c", wildcardKeyword));
                        parameters.Add(new MySqlParameter($"{paramNameBase}d", wildcardKeyword));
                        parameters.Add(new MySqlParameter($"{paramNameBase}s", wildcardKeyword));
                    }

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
                        supplier = reader.GetString("supplier")
                    };

                    // Set the rating using the existing method
                    SetProductRating(product);
                    products.Add(product);
                }
            }

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
