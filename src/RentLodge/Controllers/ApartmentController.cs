using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RentLodge.Models;
using RentLodge.Models.ApartnmentViewModels;
using RentLodge.Data;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace RentLodge.Controllers
{
    public class ApartmentController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ApartmentController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(ApartmentViewModel model)
        {
            
            ApplicationDbContext db = new ApplicationDbContext();
            Address Address = new Address(model.CountryId, model.City, model.Street, model.ApartmentNumber);
            Description Description = new Description(model.Bedrooms, model.Bathrooms, model.Floor, model.AditionalInfo, model.Guests);

            Apartment newApartment = new Apartment(
                //model.City,
                //model.Street,
                //model.ApartmentNumber,
                //model.CountryId,
                //model.Bedrooms,
                //model.Bathrooms,
                //model.Floor,
                model.Title,
                model.Price,
                model.AditionalInfo,
                model.Guests);

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //newApartment.User = this.
            newApartment.UserId = userId;
            db.Addresses.Add(Address);
            db.SaveChanges();
            db.Descriptions.Add(Description);
            db.SaveChanges();
            newApartment.AddressId = Address.Id;
            newApartment.DescriptionId = Description.Id;
            db.Apartments.Add(newApartment);
            db.SaveChanges();
            return View("Index");
        }


       

    }
}
