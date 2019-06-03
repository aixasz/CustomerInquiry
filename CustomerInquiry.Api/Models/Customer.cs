using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerInquiry.Models
{
    public class Customer
    {
        [Key]
        [MaxLength(10)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerID { get; set; }
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [MaxLength(25)]
        public string Email { get; set; }
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        [Required]
        [StringLength(10)]
        [DataType(DataType.PhoneNumber)]
        public string Mobile { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}
