using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RentLodge.Models
{
    [Table("Reviews")]
    public class Review
    {
        [Key]
        public int Id { get; set; }
        public string GuestId { get; set; }
        public int ApartmentId { get; set; }
        public virtual Apartment Apartment { get; set; }
        public int Rating { get; set; }
        public string ReviewBody { get; set; }

    }
}
