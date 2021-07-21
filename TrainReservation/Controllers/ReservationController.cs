using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainReservation.Controllers
{
    public class ReservationController : Controller
    {
        //Get Create
        public IActionResult Create(int TripId)
        {
            return View();
        }




    }
}
