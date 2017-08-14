using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RentLodge.Models
{
    [Table("Countries")]
    public class Country
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
