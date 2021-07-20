using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrainReservation.Models
{
    public class VisitingStation
    {
        [Key]
        public int Id { get; set; }
        public int TripId { get; set; }
        public Trip Trip { get; set; }
        public int StationId { get; set; }
        public Station Station { get; set; }
        public DateTime VisitTime { get; set; }

    }
}
