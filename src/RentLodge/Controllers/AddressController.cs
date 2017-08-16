using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RentLodge.Data;
using RentLodge.Models;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using RentLodge.Models.ApartnmentViewModels;

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

        public IActionResult Details(int id)
        {
            Address addressToDisplay = db.Addresses.FirstOrDefault(address => address.Id == id);
            return PartialView(addressToDisplay);
        }

        
        //[HttpPost]
        //public IActionResult Create(string countryId, string city, string street, string apartmentNumber)
        //{
        //    Address newAddress = new Address(countryId, city, street, apartmentNumber);

        //    return RedirectToAction("Create", "Apartment");
        //}
        //[HttpPost]
        //public IActionResult Create(ApartmentViewModel model)
        //{
        //    Address newAddress = new Address(model.CountryId, model.City, model.Street, model.ApartmentNumber);
        //    return RedirectToAction("Create", "Apartment");
        //}
    }
}
