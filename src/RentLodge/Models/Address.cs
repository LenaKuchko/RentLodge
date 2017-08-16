using RentLodge.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentLodge.Models
{
    [Table("Addresses")]
    public class Address
    {
        [Key]
        public int Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string ApartmentNumber { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }

        public Address()
        {

        }
        public Address(int countryId, string city, string street, string apartmentNumber)
        {
            //Country = country;
            CountryId = countryId;
            City = city;
            Street = street;
            ApartmentNumber = apartmentNumber;
        }

        public int Save()
        {
           ApplicationDbContext db = new ApplicationDbContext();
           db.Add(this);
           db.SaveChanges();
           return this.Id; 
         }
    }
}