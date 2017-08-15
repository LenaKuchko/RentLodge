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
        public string Id { get; set; }
        public int Bedrooms { get; set; }
        public int Bethrooms { get; set; }
        public string Floor { get; set; }
        public string AditionalInfo { get; set; }
        public int Guests { get; set; }
    }
}
