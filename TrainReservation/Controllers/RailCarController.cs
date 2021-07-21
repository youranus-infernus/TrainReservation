using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainReservation.Data;
using TrainReservation.Models;

namespace TrainReservation.Controllers
{
    public class RailCarController : Controller
    {
        private readonly ApplicationDbContext _db;

        public RailCarController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index(int? TrainId)
        {
            IList<RailCar> RailCars = _db.RailCars.Where(t => t.TrainId == TrainId).ToList();
            ViewBag.TrainName = _db.Trains.Find(TrainId).Name;
            ViewBag.TrainId = TrainId;
            return View(RailCars);
        }

        //Get Create
        public IActionResult Create(int? TrainId)
        {

            ViewBag.TrainId = TrainId;
            return View();
        }

        //Post Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RailCar railcar)
        {
            if (ModelState.IsValid)
            {
                _db.RailCars.Add(railcar);
                _db.SaveChanges();
                return RedirectToAction("Index", new { TrainId = railcar.TrainId });
            }
            return View(railcar);
        }

        //Get Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var railcar = _db.RailCars.Find(id);

            if (railcar == null)
            {
                return NotFound();
            }
            return View(railcar);
        }

        //Post Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var railcar = _db.RailCars.Find(id);
            if (railcar == null)
            {
                return NotFound();
            }

            _db.RailCars.Remove(railcar);
            _db.SaveChanges();
            return RedirectToAction("Index", new { TrainId = railcar.TrainId });
        }

        //Get Update
        public IActionResult Update(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var railcar = _db.RailCars.Find(id);
            ViewBag.TrainId = railcar.TrainId;

            if (railcar == null)
            {
                return NotFound();
            }
            return View(railcar);
        }

        //Post Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(RailCar railcar)
        {
            if (ModelState.IsValid)
            {
                _db.RailCars.Update(railcar);
                _db.SaveChanges();
                return RedirectToAction("Index", new { TrainId = railcar.TrainId });
            }
            return View(railcar);
        }
    }
}
