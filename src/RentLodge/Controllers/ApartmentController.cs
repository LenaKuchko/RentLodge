﻿using System;
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
using System.Data.SqlClient;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

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
            ApplicationDbContext db = new ApplicationDbContext();

            var moveIn = model.MoveIn.ToString("yyyy/MM/dd");
            var moveOut = model.MoveOut.ToString("yyyy/MM/dd");

            HttpContext.Session.SetString("MoveIn", moveIn);
            HttpContext.Session.SetString("MoveOut", moveOut);

            string city = "";
            string country = "";

            SqlParameter cityParam = new SqlParameter("@city", model.City);
            SqlParameter countryParam = new SqlParameter("@country", model.Country);
            SqlParameter moveInParam = new SqlParameter("@moveIn", moveIn);
            SqlParameter moveOutParam = new SqlParameter("@moveOut", moveOut);

            //тернарный
            if (cityParam.Value != null)
            {
                city = " AND ad.City = @city";
            }
            else
            {
                cityParam.Value = "";
            }

            if (countryParam.Value != null)
            {
                country = " AND Countries.Name = @country";
            }
            else
            {
                countryParam.Value = "";
            }

           
            var parameters = new SqlParameter[] { moveInParam, moveOutParam, cityParam, countryParam };

            string query = "SELECT ap.Id, ap.AddressId, ap.Available, ap.DescriptionId, ap.Price, ap.Rating, ap.Title, ap.UserId, " +
                "ad.Id AS AdId, ad.ApartmentNumber, ad.City, ad.CountryId, ad.Street FROM Apartments ap " +
                "JOIN Addresses ad ON (ap.AddressId = ad.Id) " +
                "JOIN Countries ON (ad.CountryId = Countries.Id) " +
            
                "WHERE ap.Id not in " +
                "(SELECT reservations.ApartmentId FROM Reservations reservations " +
                "WHERE (reservations.MoveIn <= @moveIn AND reservations.MoveOut >= @moveIn) " +
                "OR (reservations.MoveIn <= @moveOut AND reservations.MoveOut >= @moveOut) " +
                "OR (reservations.MoveIn >= @moveIn AND reservations.MoveIn <= @moveIn) GROUP BY reservations.ApartmentId)" + city + country;

            var apartments = db.Apartments.FromSql(query, parameters).Include(apart => apart.Address).ToList();
           
            foreach (var apartment in apartments)
            {
                apartment.Photos = db.Photos.Where(ph => ph.ApartmentId == apartment.Id).ToList();
            }

            var json = JsonConvert.SerializeObject(Apartment.GetMarkers(apartments));
            ViewBag.Markers = json;

            return View(apartments);
         }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(ApartmentViewModel model)
        {
            Address Address = new Address(model.CountryId, model.City, model.Street, model.ApartmentNumber);
            Description Description = new Description(model.Bedrooms, model.Bathrooms, model.Floor, model.AditionalInfo, model.Guests);

            Apartment newApartment = new Apartment(
                model.Title,
                model.Price,
                model.Rating,
                model.Available);

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            newApartment.UserId = userId;

            this._db.Addresses.Add(Address);
            this._db.SaveChanges();

            this._db.Descriptions.Add(Description);
            this._db.SaveChanges();

            newApartment.AddressId = Address.Id;
            newApartment.DescriptionId = Description.Id;

            this._db.Apartments.Add(newApartment);
            this._db.SaveChanges();
 
            return RedirectToAction("ApartmentsList");
        }

        public IActionResult ApartmentsList()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var ownerApartments = this._db.Apartments
                .Include(ap => ap.Photos)
                .Include(ap => ap.Address)
                .Where(apartment => apartment.UserId == userId).ToList();
            return View(ownerApartments);
        }
        public IActionResult Details(int id)
        {
            Apartment apartmentToDisplay = this._db.Apartments.Include(ap => ap.Address)
                .Include(ap => ap.Description)
                .Include(ap => ap.Photos)
                .Include(ap => ap.Reviews)
                .ThenInclude(r => r.Guest).FirstOrDefault(apartment => apartment.Id == id);

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

            this._db.Entry(apartment.Description).State = EntityState.Modified;
            this._db.SaveChanges();

            this._db.Entry(apartment.Address).State = EntityState.Modified;
            this._db.SaveChanges();


            return RedirectToAction("ApartmentsList");
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
                .Include(ap => ap.Address)
                .Include(ap => ap.Description)
                .Include(ap => ap.Photos)
                .FirstOrDefault(apartment => apartment.Id == id);

            Address addresToDelete = apartmentToDelete.Address;
            Description descriptionToDelete = apartmentToDelete.Description;
            var photos = apartmentToDelete.Photos.ToList();

            foreach(var photo in photos)
            {
                this._db.Remove(photo);
                this._db.SaveChanges();
            }
            
            this._db.Remove(addresToDelete);
            this._db.SaveChanges();
            this._db.Remove(descriptionToDelete);
            this._db.SaveChanges();
            
            return RedirectToAction("ApartmentsList");
        }

       
    }
}
