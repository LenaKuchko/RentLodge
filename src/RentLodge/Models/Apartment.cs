using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RentLodge.Models
{
    [Table("Apartments")]
    public class Apartment
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Available { get; set; }
        public float Rating { get; set; }
        public int Price { get; set; }
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }
        public int DescriptionId { get; set; }
        public virtual Description Description { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }

        public Apartment() { }
        public Apartment(
            
            string title,
            int price,
            float rating = 0,
            bool available = true
            )
        {
            //Address = new Address(countryId, city, street, apartmentNumber);
            //AddressId = Address.Save();
            //Description  = new Description(bedrooms, bathrooms, floor, aditionalInfo, guests);
            //DescriptionId = Description.Save();
            Title = title;
            //AddressId = addressId;
            //DescriptionId = descriptionId;
            //UserId = userId;
            Price = price;
            Rating = rating;
            Available = available;
        }

        public List<object> GetCoords(string search)
        {
            List<object> coords = new List<object>();
            var client = new RestClient("https://maps.googleapis.com/maps/api/geocode");
            var request = new RestRequest("json?address=" + search + "&key=" + EnvironmentVariables.GeocoderKey);

            var response = new RestResponse();

            Task.Run(async () =>
                {
                    response = await GetResponseContentAsync(client, request) as RestResponse;
                }).Wait();

            JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(response.Content);

            coords.Add(JsonConvert.DeserializeObject<double>(jsonResponse["results"][0]["geometry"]["location"]["lat"].ToString()));
            coords.Add(JsonConvert.DeserializeObject<double>(jsonResponse["results"][0]["geometry"]["location"]["lng"].ToString()));
            coords.Add(this.Price);

            return coords;


            //HttpClient client = new HttpClient();

            //string url = "https://maps.googleapis.com/maps/api/geocode/json?address=1600+Amphitheatre+Parkway,+Mountain+View,+CA&key=AIzaSyAOo96lCYLFG7nXjgxzD_YuljYOu850JcU";

            //var response = await client.GetAsync(url);
            //string result = await response.Content.ReadAsStringAsync();

            //JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(result);

            //var lat = JsonConvert.DeserializeObject<string>(jsonResponse["location"].ToString());
            //var client = new RestClient("https://maps.googleapis.com/maps/api/geocode/json?address=1600+Amphitheatre+Parkway,+Mountain+View,+CA&key=AIzaSyAOo96lCYLFG7nXjgxzD_YuljYOu850JcU");
            //var request = new RestRequest();
            ////client.Authenticator = new HttpBasicAuthenticator("AIzaSyAOo96lCYLFG7nXjgxzD_YuljYOu850JcU", "");

            //var response = new RestResponse();

            //Task.Run(async () =>
            //{
            //    response = await GetResponseContentAsync(client, request) as RestResponse;
            //}).Wait();

            //JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(response.Content);

            //string latitude = "";
            //string longitude = "";

        }

        public static Task<IRestResponse> GetResponseContentAsync(RestClient theClient, RestRequest theRequest)
        {
            var tcs = new TaskCompletionSource<IRestResponse>();
            theClient.ExecuteAsync(theRequest, response => {
                tcs.SetResult(response);
            });
            return tcs.Task;
        }

        public static List<object> GetMarkers(List<Apartment> apartments)
        {
            List<object> markers = new List<object>();

            for (int i = 0; i < apartments.Count; i++)
            { 
                markers.Add(apartments[i].GetCoords(apartments[i].GetAddress()));
            }
            return markers;
        }

        public string GetAddress()
        {
            return this.Address.Street + this.Address.City + this.Address.Country;
        }
        
    }
}
