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
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using RentLodge.Models.SearchViewModels;

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
        public IActionResult Index(SearchViewModel model)
        {
            HttpContext.Session.SetString("MoveIn", model.MoveIn.ToString());
            HttpContext.Session.SetString("MoveOut", model.MoveOut.ToString());

            return View(this._db.Apartments.Where(apartment => apartment.Address.City == model.Location).ToList());
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(ApartmentViewModel model)
        {
            
            ApplicationDbContext db = new ApplicationDbContext();
            Address Address = new Address(model.CountryId, model.City, model.Street, model.ApartmentNumber);
            Description Description = new Description(model.Bedrooms, model.Bathrooms, model.Floor, model.AditionalInfo, model.Guests);

            Apartment newApartment = new Apartment(
                model.Title,
                model.Price,
                model.Rating,
                model.Available);

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            newApartment.UserId = userId;

            db.Addresses.Add(Address);
            db.SaveChanges();

            db.Descriptions.Add(Description);
            db.SaveChanges();

            newApartment.AddressId = Address.Id;
            newApartment.DescriptionId = Description.Id;
            db.Apartments.Add(newApartment);
            db.SaveChanges();

            return View("Index", db.Apartments.ToList());
        }

        public IActionResult Details(int id)
        {
            Apartment apartmentToDisplay = this._db.Apartments.FirstOrDefault(apartment => apartment.Id == id);
            return View(apartmentToDisplay);
        }
       
        public IActionResult Edit(int id)
        {
            var apartmentToEdit = this._db.Apartments
                .Include(apartment => apartment.Address)
                .Include(apartment => apartment.Description)
                .FirstOrDefault(apartment => apartment.Id == id);

            IEnumerable<Country> allCountries = this._db.Countries.ToList();
            ViewBag.Countries = new SelectList(allCountries, "Id", "Name");
            return View(apartmentToEdit);
        }

        [HttpPost]
        public IActionResult Edit(Apartment apartment)
        {
            this._db.Entry(apartment).State = EntityState.Modified;
            this._db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            Apartment apartmentToDelete = this._db.Apartments.FirstOrDefault(apartment => apartment.Id == id);
            return View(apartmentToDelete);
        }
        
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            Apartment apartmentToDelete = this._db.Apartments
                .Include(apartment => apartment.Address)
                .Include(apartment => apartment.Description)
                .FirstOrDefault(apartment => apartment.Id == id);

            Address addresToDelete = apartmentToDelete.Address;
            Description descriptionToDelete = apartmentToDelete.Description;
            
            this._db.Remove(addresToDelete);
            this._db.SaveChanges();
            this._db.Remove(descriptionToDelete);
            this._db.SaveChanges();
            
            return RedirectToAction("Index");
        }
    }
}
