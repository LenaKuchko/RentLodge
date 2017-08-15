using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RentLodge.Controllers
{
    public class ApartmentController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index() => View();

        public IActionResult Create() => View();
    }
}
