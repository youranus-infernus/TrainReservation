using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainReservation.Data;
using TrainReservation.Models;
using TrainReservation.ViewModels;

namespace TrainReservation.Controllers
{
    public class VisitingStationController : Controller
    {
        private readonly ApplicationDbContext _db;

        public VisitingStationController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index(int? TripId, string TrainName)
        {
            IList<VisitingStation> visitingStations = _db.VisitingStations.Include(t => t.Station).Where(t => t.TripId == TripId).ToList();
            ViewBag.TripId = TripId;
            ViewBag.TrainName = TrainName;
            return View(visitingStations);
        }

        //Get Create
        public IActionResult Create(int? TripId)
        {
            IEnumerable<Station> stations = _db.Stations;
            VisitingStationViewModel visitingStationViewModel = new VisitingStationViewModel { Stations = new SelectList(stations, "Id", "Name"), TripId = (int)TripId };
            return View(visitingStationViewModel);
        }

        //Post Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(VisitingStationViewModel visitingStationViewModel)
        {
            if (ModelState.IsValid)
            {
                Station station = _db.Stations.Find(visitingStationViewModel.StationId);
                Trip trip = _db.Trips.Include(t => t.Train).First(t => t.Id == visitingStationViewModel.TripId);
                VisitingStation visitingStation = new VisitingStation { VisitTime = visitingStationViewModel.VisitTime, Station = station, Trip = trip };

                _db.VisitingStations.Add(visitingStation);
                _db.SaveChanges();
                return RedirectToAction("Index", new {TripId = trip.Id, TrainName = trip.Train.Name });
            }
            return View();
        }

        //Get Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            IList<VisitingStation> visitingStationsList = _db.VisitingStations.Include(t => t.Station).Include(t => t.Trip.Train).ToList();
            var visitingStation = visitingStationsList.First(t => t.Id == id);

            if (visitingStation == null)
            {
                return NotFound();
            }
            return View(visitingStation);
        }

        //Post Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var visitingStation = _db.VisitingStations.Find(id);
            var trip = _db.Trips.Include(t => t.Train).First(t => t.Id == visitingStation.TripId);
            if (visitingStation == null)
            {
                return NotFound();
            }

            _db.VisitingStations.Remove(visitingStation);
            _db.SaveChanges();
            return RedirectToAction("Index", new { TripId = visitingStation.TripId, TrainName = trip.Train.Name });
        }


    }
}
