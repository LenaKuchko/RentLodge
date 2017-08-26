using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RentLodge.Data;
using RentLodge.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.IO;

namespace RentLodge.Controllers
{
    public class PhotoController : Controller
    {
        private ApplicationDbContext _db;

        public PhotoController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult AddPhoto(int id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddPhoto(IFormFile image, int id)
        {
            byte[] newImage = new byte[0];
            if (image != null)
            {
                using (Stream fileStream = image.OpenReadStream())
                using (MemoryStream ms = new MemoryStream())
                {
                    fileStream.CopyTo(ms);
                    newImage = ms.ToArray();
                }
            }
            Photo newPhoto = new Photo { ApartmentId = id, Image = newImage };
            
            this._db.Photos.Add(newPhoto);
            this._db.SaveChanges();
            
            return RedirectToAction("Index", new {apartmentId = id});
        }

        public IActionResult Index(int id)
        {
            this._db.Photos.Where(p => p.ApartmentId == id).ToList();
            return View();
        }
    }
}
