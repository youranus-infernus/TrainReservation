using Microsoft.AspNetCore.Authorization;
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
    public class TripController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TripController(ApplicationDbContext db)
        {
            _db = db;
        }

        delegate bool IsDaysСoincide(DateTime datetime1, DateTime datetime2);

        //[Authorize(Roles = "admin, user")]
        public IActionResult Index(string StationFrom, string StationTo, DateTime SelectedDateTime)
        {
            var visitingStations = _db.VisitingStations.Include(t => t.Station).Include(t => t.Trip).ToList();
            var trips = _db.Trips.Include(t => t.Train);

            List<Trip> result = new List<Trip>();
            List<Trip> superresult = new List<Trip>();

            ViewBag.Stations = _db.Stations;

            IsDaysСoincide isDaysСoincide = (dateTime1, dateTime2) => (dateTime1.Year == dateTime2.Year &&
                                                                                         dateTime1.Month == dateTime2.Month &&
                                                                                         dateTime1.Day == dateTime2.Day &&
                                                                                         dateTime1.TimeOfDay > dateTime2.TimeOfDay);

            if (SelectedDateTime.Year != 0001)
            { 
                foreach (Trip trip in trips)
                {
                    foreach(VisitingStation visit in trip.VisitingStations)
                    {
                        if (isDaysСoincide(visit.VisitTime, SelectedDateTime))
                        {
                            result.Add(trip);
                            break;
                        }
                    }
                }
            }
            else
            {
                result = (from t in trips.AsEnumerable()
                             select t).ToList();
            }

            if (!String.IsNullOrEmpty(StationFrom) && !String.IsNullOrEmpty(StationTo))
            {
                /*
                var visitingStations2 = from visit in _db.VisitingStations.Include(t => t.Station).AsEnumerable()
                                        where visit.Station.Name.Contains(StationFrom) || visit.Station.Name.Contains(StationTo)
                                        select visit;
                */

                var selected = from trip in trips.AsEnumerable()
                               join v in visitingStations on trip.Id equals v.TripId
                               where (v.Station.Name.Contains(StationFrom))
                               select trip;
                
                var selected2 = from trip in selected
                                join v in visitingStations on trip.Id equals v.TripId
                               where (v.Station.Name.Contains(StationTo))
                               select trip;
                
                result = selected.ToList();
                
               

                if(result.Count() > 0)
                {
                    foreach (Trip trip in result)
                    {
                        var FromVisit = trip.VisitingStations.First(v => v.Station.Name.Contains(StationFrom));
                        var ToVisit = trip.VisitingStations.First(v => v.Station.Name.Contains(StationTo)); ;

                        if(FromVisit.VisitTime < ToVisit.VisitTime)
                        {
                            if(SelectedDateTime.Year != 0001)
                            {
                                if (isDaysСoincide(FromVisit.VisitTime, SelectedDateTime))
                                {
                                    superresult.Add(trip);
                                }
                            }
                            else
                            {
                                superresult.Add(trip);
                            }
                        }
                    }
                    return View(superresult.ToList());
                }
            }
            return View(result.ToList());
        }

        //Get Create
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            IEnumerable<Train> trainList = _db.Trains;
            TripViewModel addTripViewModel = new TripViewModel{Trains = new SelectList(trainList, "Id", "Name")};
            return View(addTripViewModel);
        }

        //Post Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TripViewModel addTripViewModel)
        {
            if (ModelState.IsValid)
            {
                Train train = _db.Trains.Find(addTripViewModel.TrainId);
                Trip trip = new Trip { Train = train, TrainId = addTripViewModel.TrainId };
                
                _db.Trips.Add(trip);
                _db.SaveChanges();
                return RedirectToAction("Index");
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
            IList<Trip> tripsList = _db.Trips.Include(t => t.Train).ToList();
            var trip = tripsList.First(t => t.Id == id);

            if (trip == null)
            {
                return NotFound();
            }
            return View(trip);
        }

        //Post Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var trip = _db.Trips.Find(id);
            if (trip == null)
            {
                return NotFound();
            }

            _db.Trips.Remove(trip);
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
            IEnumerable<Train> trainList = _db.Trains;

            IList<Trip> tripsList = _db.Trips.Include(t => t.Train).ToList();
            var tripId = tripsList.First(t => t.Id == id).Id;

            TripViewModel addTripViewModel = new TripViewModel { Trains = new SelectList(trainList, "Id", "Name"), TripId = tripId};

            if (addTripViewModel == null)
            {
                return NotFound();
            }
            return View(addTripViewModel);
        }

        //Post Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(TripViewModel addTripViewModel)
        {
            Train train = _db.Trains.Find(addTripViewModel.TrainId);
            IList<Trip> tripsList = _db.Trips.Include(t => t.Train).ToList();
            var trip = tripsList.First(t => t.Id == addTripViewModel.TripId);
            trip.Train = train;
            if (ModelState.IsValid)
            {
                _db.Trips.Update(trip);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(trip);
        }
    }
}
