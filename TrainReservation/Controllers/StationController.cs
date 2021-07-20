using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainReservation.Data;
using TrainReservation.Models;

namespace TrainReservation.Controllers
{
    public class StationController : Controller
    {

        private readonly ApplicationDbContext _db;

        public StationController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Station> stationsList = _db.Stations;
            return View(stationsList);
        }
        
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Station station)
        {
            _db.Stations.Add(station);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Get Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var station = _db.Stations.Find(id);

            if (station == null)
            {
                return NotFound();
            }
            return View(station);
        }

        //Post Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var station = _db.Stations.Find(id);
            if (station == null)
            {
                return NotFound();
            }

            _db.Stations.Remove(station);
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
            var station = _db.Stations.Find(id);

            if (station == null)
            {
                return NotFound();
            }
            return View(station);
        }

        //Post Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Station station)
        {
            if (ModelState.IsValid)
            {
                _db.Stations.Update(station);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(station);
        }
    }
}
