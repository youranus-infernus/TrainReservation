using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainReservation.Models;

namespace TrainReservation.ViewModels
{
    public class ReservationViewModel
    {
        public SelectList Discounts { get; set; }
        public Reservation Reservation { get; set; }

    }
}
