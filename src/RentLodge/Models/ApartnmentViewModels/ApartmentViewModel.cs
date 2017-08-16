using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentLodge.Models.ApartnmentViewModels
{
    public class ApartmentViewModel
    {
        //public string Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string ApartmentNumber { get; set; }
        public int CountryId { get; set; }
        public string Title { get; set; }
        public bool Available { get; set; }
        public float Rating { get; set; }
        public int Price { get; set; }
        //public string AddressId { get; set; }
        //public Address Address { get; set; }
        //public string DescriptionId { get; set; }
        //public Description Description { get; set; }
        //public string UserId { get; set; }
        //public ApplicationUser User { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public string Floor { get; set; }
        public string AditionalInfo { get; set; }
        public int Guests { get; set; }
    }
}
