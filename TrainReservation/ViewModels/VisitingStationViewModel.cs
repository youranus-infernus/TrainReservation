using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainReservation.Models;

namespace TrainReservation.ViewModels
{
    public class VisitingStationViewModel
    {
        public int TripId { get; set; }
        public int StationId { get; set; }
        public Station Station { get; set; }
        public SelectList Stations { get; set; }
        public DateTime VisitTime { get; set; }
    }
}
