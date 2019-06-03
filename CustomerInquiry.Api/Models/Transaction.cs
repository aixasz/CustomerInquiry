using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerInquiry.Models
{
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(10)]
        public int CustomerID { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        [MaxLength(3)]
        public string Currency { get; set; }
        [Required]
        public string Status { get; set; }
        
        public Customer Customer { get; set; }
    }
}
