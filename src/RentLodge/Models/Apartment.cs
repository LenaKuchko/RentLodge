using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RentLodge.Models
{
    [Table("Apartments")]
    public class Apartment
    {
        [Key]
        public string Id { get; set; }
        public string Title { get; set; }
        public bool Available { get; set; }
        public float Rating { get; set; }
        public string AddressId { get; set; }
        public Address Address { get; set; }
        public string DescriptionId { get; set; }
        public Description Description { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public Apartment(
            string title, 
            Address address, 
            Description description, 
            ApplicationUser user,
            float rating = 0,
            bool available = true
            )
        {
            Title = title;
            address = Address;
            description = Description;
            User = user;
            Rating = rating;
            Available = available;
        }
    }
}
