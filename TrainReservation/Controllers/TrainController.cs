using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainReservation.Data;
using TrainReservation.Models;

namespace TrainReservation.Controllers
{
    public class TrainController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TrainController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Train> trainsList = _db.Trains;
            return View(trainsList);
        }

        public IActionResult Create()
        {
            return View();
        }

        //Post Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Train train)
        {
            if (ModelState.IsValid)
            {
                _db.Trains.Add(train);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(train);
        }

        //Get Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var train = _db.Trains.Find(id);

            if(train == null)
            {
                return NotFound();
            }
            return View(train);
        }

        //Post Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var train = _db.Trains.Find(id);
            if(train == null)
            {
                return NotFound();
            }
            
            _db.Trains.Remove(train);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Get Update
        public IActionResult Update(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var train = _db.Trains.Find(id);

            if (train == null)
            {
                return NotFound();
            }
            return View(train);
        }

        //Post Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Train train)
        {
            if (ModelState.IsValid)
            {
                _db.Trains.Update(train);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(train);
        }

    }
}
