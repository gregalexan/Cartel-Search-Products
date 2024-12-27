using System.ComponentModel.DataAnnotations.Schema;

namespace Cartel_Search_Products.Models
{
    public class Review
    {
        public string review { get; set; }
        [NotMapped]
        public string companyName { get; set; }
        [NotMapped]
        public string companyImage { get; set; }
        public int productID { get; set; }
        public int stars { get; set; }
    }
}
