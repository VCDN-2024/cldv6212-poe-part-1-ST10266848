using Azure;
using Azure.Data.Tables;
using System.ComponentModel.DataAnnotations;

namespace ABC_Retail_POE.Models
{
    public class Product : ITableEntity
    {
        //Product Attributes:
        [Key]
        public string ProductId { get; set; } = null!;

        [Required(ErrorMessage = "Please select a product category!")]
        public string ProductCategory { get; set; } = null!;

        [Required(ErrorMessage = "Please enter the name of the product!")]
        public string ProductName { get; set; } = null!;

        [Required(ErrorMessage = "Please enter the description of the product!")]
        public string ProductDescription { get; set; } = null!;

        [Required(ErrorMessage = "Please enter the product's price!")]
        public double ProductPrice { get; set; }

        [Required(ErrorMessage = "Please upload a picture of the product!")]
        public string ImageUrl { get; set; } = null!;

        //ITableEntity implementation
        public string PartitionKey { get; set; } = null!;
        public string RowKey { get; set; } = null!;
        public ETag ETag { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
    }
}
