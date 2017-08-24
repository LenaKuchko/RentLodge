using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RentLodge.Models
{
    [Table("Reservations")]
    public class Reservation
    {
        [Key]
        public int Id { get; set; }
        public string OwnerId { get; set; }
        public virtual ApplicationUser Owner { get; set; }
        public string GuestId { get; set; }
        public virtual ApplicationUser Guest { get; set; }
        public int ApartmentId { get; set; }
        public virtual Apartment Apartment { get; set; }
        public bool Peyment { get; set; }
        public DateTime MoveIn { get; set; }
        public DateTime MoveOut { get; set; }
        public int GuestsNumber { get; set; }
        public int Days { get; set; }
        public float RentalSum { get; set; }
    

        public float CalcRentalSum(Apartment apartment)
        {
            return this.Days * apartment.Price;
        }
    }
}
