using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RentLodge.Models;

namespace RentLodge.Controllers
{
    public class DescriptionController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index() => View();

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(int bedrooms, int bathrooms, string floor, string aditionalInfo, int guests)
        {
            Description newDescription = new Description(bedrooms, bathrooms, floor, aditionalInfo, guests);
            return View(newDescription);
        }
    }
}
