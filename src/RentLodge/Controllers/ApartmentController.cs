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
            ApplicationDbContext db = new ApplicationDbContext();
            return View(db.Apartments.ToList());
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
            var apartmentToEdit = this._db.Apartments.Include(apartment => apartment.Address).Include(apartment => apartment.Description).FirstOrDefault(apartment => apartment.Id == id);

            IEnumerable<Country> allCountries = this._db.Countries.ToList();
            ViewBag.Countries = new SelectList(allCountries, "Id", "Name");

            ApartmentViewModel model = new ApartmentViewModel(
                apartmentToEdit.Id,
                apartmentToEdit.Address.City,
                apartmentToEdit.Address.Street,
                apartmentToEdit.Address.ApartmentNumber,
                apartmentToEdit.Address.CountryId,
                apartmentToEdit.Title, 
                apartmentToEdit.Price,
                apartmentToEdit.Description.Bedrooms,
                apartmentToEdit.Description.Bethrooms,
                apartmentToEdit.Description.Floor,
                apartmentToEdit.Description.AditionalInfo,
                apartmentToEdit.Description.Guests,
                apartmentToEdit.Rating,
                apartmentToEdit.Available);

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(ApartmentViewModel model)
        {
            Apartment apartmentToEdit = this._db.Apartments.FirstOrDefault(apartment => apartment.Id == model.ApartmentId);
            //var id = apartmentToEdit.AddressId;
            Address addressToEdit = this._db.Addresses.FirstOrDefault(address => address.Id == apartmentToEdit.AddressId);
            Description descriptionToEdit = this._db.Descriptions.FirstOrDefault(description => description.Id == apartmentToEdit.DescriptionId);

            apartmentToEdit.Title = model.Title;
            apartmentToEdit.Available = model.Available;
            apartmentToEdit.Price = model.Price;
            this._db.Entry(apartmentToEdit).State = EntityState.Modified;
            this._db.SaveChanges();

            addressToEdit.City = model.City;
            addressToEdit.Street = model.Street;
            addressToEdit.ApartmentNumber = model.ApartmentNumber;

            this._db.SaveChanges();

            this._db.Descriptions.Update(descriptionToEdit);
            this._db.SaveChanges();

            this._db.Apartments.Update(apartmentToEdit);
            this._db.SaveChanges();

            return RedirectToAction("Index");
        }
        //[HttpPost]
        //public IActionResult Edit(int id)
        //{
        //    Apartment apartmentToEdit = this._db.Apartments.FirstOrDefault(apartment => apartment.Id == id);

        //    return View(apartmentToEdit);
        //}

    }
}
