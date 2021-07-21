using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrainReservation.Models
{
    public class Seat
    {
        [Key]
        public int Id { get; set; }
        public int SeatNumber { get; set; }
        public float Price { get; set; }
        public RailCar RailCar { get; set; }
        public int RailCarId { get; set; }
    }
}
