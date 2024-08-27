using Azure;
using Azure.Data.Tables;
using System.ComponentModel.DataAnnotations;
using ABC_Retail_POE.Models;

namespace ABC_Retail_POE.Models
{
    public class Order : ITableEntity
    {
        [Key]
        public string OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        //public DateTime ModifiedDate { get; set; }

        //Simple validation:
        //Introduce required attribute and stringlength validation for each of the below

        [Required(ErrorMessage = "Please enter the Credit Card Name!")]
        public string CreditCardName { get; set; } = null!; //no real specific length honestly

        [Required(ErrorMessage = "Please enter the Credit Card Number!")]
        public string CreditCardNumber { get; set; } = null!; //certain length - about 16

        [Required(ErrorMessage = "Please enter the Credit Card Expiry Date!")]
        public string CreditCardExpDate { get; set; } = null!; //certain length - 

        [Required(ErrorMessage = "Please enter the Credit Card CCV Code!")]
        public string CreditCardCCVcode { get; set; } = null!; //cetain length - about 7

        [Required(ErrorMessage = "Please enter a shipping address!")]
        public string ShippingAddress { get; set; } = null!;

        //FK's:
        [Required(ErrorMessage = "Please select a User!")]
        public string UserId { get; set; } = null!; // FK to the User who placed the order

        [Required(ErrorMessage = "Please select a Product!")]
        public string ProductId { get; set; } // FK to the Product being ordered

        public string PartitionKey { get; set; } = null!;
      
        public string RowKey { get; set; } = null!;
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}
