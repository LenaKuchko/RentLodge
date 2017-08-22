using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RentLodge.Data;
using RentLodge.Models;

namespace RentLodge.Controllers
{
    public class CountryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CountryController(ApplicationDbContext db)
        {
            _db = db;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(this._db.Countries.ToList());
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(string name)
        {
            Country newCountry = new Country { Name = name };
            this._db.Countries.Add(newCountry);
            this._db.SaveChanges();
            return RedirectToAction("Index");
        }
       
    }
}
