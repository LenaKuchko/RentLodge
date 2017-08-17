using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentLodge.Models.ApartnmentViewModels
{
    public class ApartmentViewModel
    {
        public int ApartmentId { get; set; }
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

        public ApartmentViewModel()
        {

        }

        public ApartmentViewModel(
                int apartmentId, 
                string city, 
                string street, 
                string apartmentNumber,
                int countryId,
                string title,
                int price,
                int bedrooms,
                int bathrooms,
                string floor,
                string aditionalInfo,
                int guests,
                float rating = 0,
                bool available = true)
        {
            ApartmentId = apartmentId;
            City = city;
            Street = street;
            ApartmentNumber = apartmentNumber;
            CountryId = countryId;
            Title = title;
            Price = price;
            Bedrooms = bedrooms;
            Bathrooms = bathrooms;
            Floor = floor;
            AditionalInfo = aditionalInfo;
            Guests = guests;
            Rating = rating;
            Available = available;
        }
    }
}
