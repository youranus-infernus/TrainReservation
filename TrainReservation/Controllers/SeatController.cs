using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainReservation.Data;
using TrainReservation.Models;

namespace TrainReservation.Controllers
{
    public class SeatController : Controller
    {
        private readonly ApplicationDbContext _db;

        public SeatController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index(int? RailCarId, string TrainName)
        {
            IList<Seat> Seats = _db.Seats.Include(s => s.RailCar).Where(t => t.RailCarId == RailCarId).ToList();
            ViewBag.TrainName = TrainName;
            ViewBag.RailCarNumber = _db.RailCars.Find(RailCarId).RailcarNumber;
            ViewBag.RailCarId = RailCarId;
            ViewBag.TrainId = _db.RailCars.Find(RailCarId).TrainId;
            return View(Seats);
        }

        //Get Create
        public IActionResult Create(int? RailCarId, string TrainName)
        {
            ViewBag.TrainName = TrainName;
            ViewBag.RailCarId = RailCarId;
            return View();
        }

        //Post Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seat seat)
        {
            if (ModelState.IsValid)
            {
                var RailCarId = seat.RailCarId;
                var TrainId = _db.RailCars.Find(RailCarId).TrainId;
                var TrainName = _db.Trains.Find(TrainId).Name;
                _db.Seats.Add(seat);
                _db.SaveChanges();
                return RedirectToAction("Index", new { RailCarId = RailCarId, TrainName = TrainName });
            }
            return View(seat);
        }

        //Get Delete
        public IActionResult Delete(int? id, int? RailCarId, string TrainName)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var seat = _db.Seats.Find(id);

            if (seat == null)
            {
                return NotFound();
            }

            ViewBag.TrainName = TrainName;
            ViewBag.RailCarId = RailCarId;

            return View(seat);
        }

        //Post Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var RailCarId = _db.Seats.Find(id).RailCarId;
            var TrainId = _db.RailCars.Find(RailCarId).TrainId;
            var TrainName = _db.Trains.Find(TrainId).Name;
            var seat = _db.Seats.Find(id);
            if (seat == null)
            {
                return NotFound();
            }

            _db.Seats.Remove(seat);
            _db.SaveChanges();
            return RedirectToAction("Index", new { RailCarId = RailCarId, TrainName = TrainName });
        }

        //Get Update
        public IActionResult Update(int? id, int? RailCarId, string TrainName)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var seat = _db.Seats.Find(id);

            if (seat == null)
            {
                return NotFound();
            }

            ViewBag.TrainName = TrainName;
            ViewBag.RailCarId = RailCarId;

            return View(seat);
        }

        //Post Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Seat seat)
        {
            if (ModelState.IsValid)
            {
                var RailCarId = seat.RailCarId;
                var TrainId = _db.RailCars.Find(RailCarId).TrainId;
                var TrainName = _db.Trains.Find(TrainId).Name;
                _db.Seats.Update(seat);
                _db.SaveChanges();
                return RedirectToAction("Index", new { RailCarId = RailCarId, TrainName = TrainName });
            }
            return View(seat);
        }
    }
}
