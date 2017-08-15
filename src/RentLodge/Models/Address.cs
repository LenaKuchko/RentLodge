using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentLodge.Models
{
    [Table("Addresses")]
    public class Address
    {
        [Key]
        public string Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string ApartmentNumber { get; set; }
        public string CountryId { get; set; }
        public Country Country { get; set; }

        public Address(string countryId, string city, string street, string apartmentNumber)
        {
            //Country = country;
            CountryId = countryId;
            City = city;
            Street = street;
            ApartmentNumber = apartmentNumber;
        }
    }
}