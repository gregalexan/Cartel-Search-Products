using System.ComponentModel.DataAnnotations;

namespace Cartel_Search_Products.Models
{
    /* Represents the user */
    public class User
    {
        [Required]
        public string name { get; set; }
        [Required]
        public string email { get; set; }
        [Key]
        public string ssn { get; set; }
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }

        public DateTime joined { get; set; }
        public string image { get; set; }
        public string city { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public int zip {  get; set; }
    }
}
