﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentLodge.Models.SearchViewModels
{
    public class SearchViewModel
    {
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime MoveIn { get; set; }
        public DateTime MoveOut { get; set; }
    }
}
