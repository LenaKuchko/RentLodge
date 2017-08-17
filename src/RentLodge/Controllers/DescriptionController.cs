using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RentLodge.Models;
using RentLodge.Data;

namespace RentLodge.Controllers
{
    public class DescriptionController : Controller
    {
        public ApplicationDbContext db = new ApplicationDbContext();

        // GET: /<controller>/
        public IActionResult Index() => View();

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(int bedrooms, int bathrooms, string floor, string aditionalInfo, int guests)
        {
            Description newDescription = new Description(bedrooms, bathrooms, floor, aditionalInfo, guests);
            return View(newDescription);
        }

        public IActionResult Details(int id)
        {
            Description descriptionToDisplay = db.Descriptions.FirstOrDefault(description => description.Id == id);
            return PartialView(descriptionToDisplay);
        }

        public IActionResult Edit(int id)
        {
            Description descriptionToEdit = db.Descriptions.FirstOrDefault(description => description.Id == id);
            return PartialView(descriptionToEdit);
        }
    }
}
