using System.ComponentModel.DataAnnotations;

namespace CommerceApp.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Price { get; set; }

        // This will store the URL to the image in blob storage
        public string ImageUrl { get; set; }
    }
}
