using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using TrainReservation.Areas.Identity.Data;
using TrainReservation.Data;
using TrainReservation.Models;
using TrainReservation.ViewModels;

namespace TrainReservation.Controllers
{
    [Authorize]
    public class ReservationController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ReservationController(ApplicationDbContext db)
        {
            _db = db;
        }
        //Get Booking
        public IActionResult Booking(int TrainId)
        {
            ViewBag.TrainId = TrainId;
            var RailcarList = _db.RailCars.Include(s => s.Seats).Where(r => r.TrainId == TrainId).ToList();

            var seats = _db.Seats.Where(t => t.IsOccupied == true && t.IsDisabled == false).ToList();

            foreach (var seat in seats)
                seat.IsOccupied = false;

            _db.SaveChanges();
            return View(RailcarList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Booking(List<RailCar> RailcarList, int TrainId)
        {
            if (RailcarList != null && RailcarList.Count > 0)
            {
                _db.Trains.Find(TrainId).RailCars = RailcarList;
                _db.SaveChanges();
            }
            return RedirectToAction("PessengerData", new {TrainId = TrainId });
        }

        public IActionResult PessengerData(int TrainId)
        {
            List<ReservationViewModel> reservationViewModels = new List<ReservationViewModel>();
            Train train = _db.Trains.Include(t => t.RailCars).ThenInclude(r => r.Seats).First(t => t.Id == TrainId);
            var discounts = _db.Discounts.ToList();

            foreach (var railcar in train.RailCars)
            {
                foreach (var s in railcar.Seats)
                {
                    if (s.IsOccupied && !s.IsDisabled)
                    {
                        ClaimsPrincipal claimsPrincipal = this.User;
                        var currentUserID = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier).Value;
                        TrainReservationUser currentUser = _db.Users.Find(currentUserID);

                        reservationViewModels.Add(new ReservationViewModel
                        {
                            Discounts = new SelectList(discounts, "Id", "Name", "Percent"),
                            Reservation = new Reservation
                            {
                                Seat = s,
                                SeatId = s.Id,
                                TrainReservationUser = currentUser,
                                TrainReservationUserId = currentUser.Id
                            }
                        });
                    }
                }
            }
            _db.SaveChanges();
            ViewBag.TrainId = TrainId;

            return View(reservationViewModels);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PessengerData(List<ReservationViewModel> reservationViewModels)
        {
            Generator generator = new Generator();
            List<int> reservations = new List<int>();
            var SessionKey = generator.Generate();

            if (ModelState.IsValid)
            {
                foreach(var reservationViewModel in reservationViewModels)
                {
                    Seat seat = _db.Seats.Find(reservationViewModel.Reservation.SeatId);
                    Discount discount = _db.Discounts.Find(reservationViewModel.Reservation.DiscountId);
                    TrainReservationUser user = _db.Users.Find(reservationViewModel.Reservation.TrainReservationUserId);

                    Reservation reservation = new Reservation
                    {
                        Discount = discount,
                        Seat = seat,
                        TrainReservationUser = user,
                        PassengerName = reservationViewModel.Reservation.PassengerName,
                        PassengerSurname = reservationViewModel.Reservation.PassengerSurname,
                        SessionKey = SessionKey
                    };

                    _db.Reservations.Add(reservation);
                    _db.SaveChanges();
                    reservations.Add(_db.Reservations.First(r => r.SeatId == seat.Id).Id);
                }
                return RedirectToAction("Payment", new { SessionKey = SessionKey});
            }
            return View();
        }

        public IActionResult Payment(string SessionKey)
        {
            ClaimsPrincipal claimsPrincipal = this.User;
            var currentUserID = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier).Value;
            TrainReservationUser currentUser = _db.Users.Find(currentUserID);

      
            var reservationIds = TempData["reservations"];

            int[] list = (int[])reservationIds;

            
            var reservations = _db.Reservations.
            Include(r => r.Seat).ThenInclude(s => s.RailCar).ThenInclude(r => r.Train).Include(i => i.Discount).
            Where(t => t.SessionKey == SessionKey).ToList();
            
            Trip trip = null;

            if (reservations.Count > 0)
            {
                int trainId = reservations.First().Seat.RailCar.TrainId;
                trip = _db.Trips.Include(s => s.SelectedStationFrom).Include(s => s.SelectedStationTo).Include(v => v.VisitingStations).ThenInclude(s => s.Station).First(t => t.TrainId == trainId);
            }

            foreach(var reservation in reservations)
            {
                reservation.Seat.IsDisabled = true;
            }

            _db.SaveChanges();
            ViewBag.trip = trip;
            ViewBag.Stations = _db.Stations;

            return View(reservations);
        }

       
        public IActionResult MyTickets()
        {
            ClaimsPrincipal claimsPrincipal = this.User;
            var currentUserID = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier).Value;

            var thisUserReservations = _db.Reservations.Include(r => r.Seat).Include(r => r.Discount).Where(r => r.TrainReservationUserId == currentUserID).ToList();

            return View(thisUserReservations);
        }

    }
}
