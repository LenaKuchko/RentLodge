using RentLodge.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RentLodge.Models
{   [Table("Descriptions")]
    public class Description
    {
        [Key]
        public int Id { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public string Floor { get; set; }
        public string AditionalInfo { get; set; }
        public int Guests { get; set; }

        public Description()
        {

        }

        public Description(int bedrooms, int bathrooms, string floor, string aditionalInfo, int guests)
        {
            Bedrooms = bedrooms;
            Bathrooms = bathrooms;
            Floor = floor;
            AditionalInfo = aditionalInfo;
            Guests = guests;
        }

        public int Save()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            db.Descriptions.Add(this);
            db.SaveChanges();
            return this.Id;
        }
    }
}
