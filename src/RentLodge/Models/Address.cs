using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentLodge.Models
{
    [Table("Addresses")]
    public class Address
    {
        [Key]
        public string Id { get; set; }
        public string Country { get; set; }
        public string CountryId { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Apatrtment { get; set; }
    }
}