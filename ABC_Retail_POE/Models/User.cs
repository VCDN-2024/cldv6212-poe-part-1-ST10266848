using Azure;
using Azure.Data.Tables;
using System.ComponentModel.DataAnnotations;

namespace ABC_Retail_POE.Models
{
    public class User : ITableEntity
    {
        //User attributes:
        [Key]
        public string UserId { get; set; } = null!;

        //Stores whether user is an Admin or Customer
        //[Required(ErrorMessage = "Please select a User Role!")]
        //public string UserRole { get; set; } = null!;

        [Required(ErrorMessage = "Please enter your full name!")]
        public string UserFullName { get; set; } = null!;

        [Required(ErrorMessage = "Please enter your email!")]
        public string UserEmail { get; set; } = null!;

        [Required(ErrorMessage = "Please enter your password!")]
        public string UserPassword { get; set; } = null!;

        //ITableEntity implementation
        public string PartitionKey { get; set; } = null!;

        public string RowKey { get; set; } = null!;
        public ETag ETag { get; set; }
        public DateTimeOffset? Timestamp { get; set; }

    }
}
