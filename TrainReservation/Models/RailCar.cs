using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrainReservation.Models
{
    public class RailCar
    {
        [Key]
        public int Id { get; set; }
        public int RailcarNumber { get; set; }
        public Train Train { get; set; }
        public int TrainId { get; set; }
        public List<Seat> Seats { get; set; }
    }
}
