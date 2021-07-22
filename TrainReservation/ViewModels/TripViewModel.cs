using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainReservation.Models;

namespace TrainReservation.ViewModels
{
    public class TripViewModel
    { 
        public int TripId { get; set; }
        public int TrainId { get; set; }
        public SelectList Trains { get; set; }
        public int StationFromId { get; set; }
        public int StationToId { get; set; }
        public SelectList Stations { get; set; }
    }
}
