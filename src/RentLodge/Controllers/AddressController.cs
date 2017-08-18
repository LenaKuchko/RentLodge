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

        public IActionResult Edit(int id)
        {
            Address addressToEdit = db.Addresses.FirstOrDefault(address => address.Id == id);
            IEnumerable<Country> allCountries = db.Countries.ToList();
            ViewBag.Countries = new SelectList(allCountries, "Id", "Name");
            return PartialView(addressToEdit);
        }
    }
}
