using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RentLodge.Models;

namespace RentLodge.Controllers
{
    public class ReviewController : Controller
    {
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
        public IActionResult Create(Review review)
        {
            return RedirectToAction("Details", "Reservation");
        }
    }
}
