using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RentLodge.Models
{
    [Table("Photos")]
    public class Photo
    {
        [Key]
        public int Id { get; set; }
        public byte[] Image { get; set; }
        public int ApartmentId { get; set; }
        public virtual Apartment apartment { get; set; }
        public Photo() { }
    }
}
