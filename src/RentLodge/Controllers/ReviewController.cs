using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RentLodge.Models;
using System.Security.Claims;
using RentLodge.Data;

namespace RentLodge.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ReviewController(ApplicationDbContext db)
        {
            _db = db;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        public IActionResult Create(int star, string reviewBody, int reservationId, int apartmentId)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
                
            Review newReview = new Review { ApartmentId = apartmentId, GuestId = userId, Rating = star, ReviewBody = reviewBody, Date = DateTime.Now};
            this._db.Reviews.Add(newReview);
            this._db.SaveChanges();

            return RedirectToAction("Details", "Apartment", new { id = apartmentId });
        }
    }
}
