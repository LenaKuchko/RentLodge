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
using System.Security.Claims;
using System.Globalization;

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

        public IActionResult Create()
        {
            var moveIn = HttpContext.Session.GetString("MoveIn");
            var moveOut = HttpContext.Session.GetString("MoveOut");
            ViewBag.moveIn = moveIn;
            ViewBag.moveOut = moveOut;
            return View();
        }

        [HttpPost]
        public IActionResult Create(int id)
        {
            var moveIn = HttpContext.Session.GetString("MoveIn");
            var moveOut = HttpContext.Session.GetString("MoveOut");

            Apartment apartmentToReserve = this._db.Apartments
                .Include(apartment => apartment.Address)
                .Include(apartment => apartment.Description)
                .FirstOrDefault(apartment => apartment.Id == id);

            var guestId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            Reservation newReservation = new Reservation {
                OwnerId = apartmentToReserve.UserId,
                GuestId = guestId,
                ApartmentId = apartmentToReserve.Id,
                MoveIn = DateTime.ParseExact(moveIn),
                MoveOut = Convert.ToDateTime(moveOut)};

            this._db.Reservation.Add(newReservation);
            this._db.SaveChanges();
            return View();
        }
    }
}
