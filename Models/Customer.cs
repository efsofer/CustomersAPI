using NetTopologySuite.Geometries;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;



namespace CustomersAPI.Models
{
    [Table("Customers", Schema = "Sales")]
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerID { get; set; }

        [Required]
        [MaxLength(100)]
        public string CustomerName { get; set; }

        //[Column(TypeName = "geography")]
        //public Point DeliveryLocation { get; set; }  // ✅ Ensure it's mapped correctly

        [Required]
        public int BillToCustomerID { get; set; }

        [Required]
        public int CustomerCategoryID { get; set; }

        public int? BuyingGroupID { get; set; }

        [Required]
        public int PrimaryContactPersonID { get; set; }

        public int? AlternateContactPersonID { get; set; }

        [Required]
        public int DeliveryMethodID { get; set; }

        [Required]
        public int DeliveryCityID { get; set; }

        [Required]
        public int PostalCityID { get; set; }

        public decimal? CreditLimit { get; set; }

        [Required]
        public DateTime AccountOpenedDate { get; set; }

        [Required]
        public decimal StandardDiscountPercentage { get; set; }

        [Required]
        public bool IsStatementSent { get; set; }

        [Required]
        public bool IsOnCreditHold { get; set; }

        [Required]
        public int PaymentDays { get; set; }

        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(20)]
        public string FaxNumber { get; set; }
        
        [MaxLength(5)]
        public string? DeliveryRun { get; set; }

        [MaxLength(5)]
        public string? RunPosition { get; set; }

        [Required]
        [MaxLength(256)]
        public string WebsiteURL { get; set; }

        [Required]
        [MaxLength(60)]
        public string DeliveryAddressLine1 { get; set; }

        [MaxLength(60)]
        public string? DeliveryAddressLine2 { get; set; }

        [Required]
        [MaxLength(10)]
        public string DeliveryPostalCode { get; set; }

        [Column(TypeName = "geography")]
        [JsonIgnore] // Prevents serialization issues when converting to JSON
        public Point? DeliveryLocation { get; set; }

        [NotMapped]
        public double[] Coordinates
        {
            get => DeliveryLocation != null ? new double[] { DeliveryLocation.X, DeliveryLocation.Y } : new double[] { 0.0, 0.0 };
            set => DeliveryLocation = value != null && value.Length == 2 ? new Point(value[0], value[1]) { SRID = 4326 } : null;
        }

        [Required]
        [MaxLength(60)]
        public string PostalAddressLine1 { get; set; }

        [MaxLength(60)]
        public string? PostalAddressLine2 { get; set; }


        [Required]
        [MaxLength(10)]
        public string PostalPostalCode { get; set; }

        [Required]
        public int LastEditedBy { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime ValidFrom { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime ValidTo { get; set; }

        [JsonConstructor]
        public Customer(string customerName, double[] coordinates, int billToCustomerID, int customerCategoryID, int primaryContactPersonID, int deliveryMethodID, string deliveryPostalCode, int deliveryCityID, int postalCityID)
        {
            CustomerName = customerName;
            BillToCustomerID = billToCustomerID;
            CustomerCategoryID = customerCategoryID;
            PrimaryContactPersonID = primaryContactPersonID;
            DeliveryMethodID = deliveryMethodID;
            DeliveryCityID = deliveryCityID;
            PostalCityID = postalCityID;
            DeliveryPostalCode = deliveryPostalCode;
            DeliveryLocation = new Point(coordinates[0], coordinates[1]) { SRID = 4326 };
        }

        public Customer() { } // ✅ קונסטרקטור ברירת מחדל
    }

}
