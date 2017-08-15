using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RentLodge.Data;
using RentLodge.Models;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RentLodge.Controllers
{
    public class AddressController : Controller
    {
        public ApplicationDbContext db = new ApplicationDbContext();
        // GET: /<controller>/
        public IActionResult Index() => View();

        public IActionResult Create()
        {
            IEnumerable<Country> allCountries = db.Countries.ToList();
            ViewBag.Countries = new SelectList(allCountries, "Id", "Name");
                return View();
        }

        [HttpPost]
        public IActionResult Create(string countryId, string city, string street, string apartmentNumber)
        {
            Address newAddress = new Address(countryId, city, street, apartmentNumber);
            return View();
        }
    }
}
