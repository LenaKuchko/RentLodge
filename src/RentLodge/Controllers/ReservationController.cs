using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RentLodge.Data;
using RentLodge.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace RentLodge.Controllers
{
    public class ReservationController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReservationController(UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(int id)
        {
            var test = HttpContext.Session.GetString("name");
            Apartment apartmentToReserve = this._db.Apartments
                .Include(apartment => apartment.Address)
                .Include(apartment => apartment.Description)
                .FirstOrDefault(apartment => apartment.Id == id);
            return View();
        }
    }
}
