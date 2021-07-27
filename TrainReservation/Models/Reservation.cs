using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TrainReservation.Areas.Identity.Data;

namespace TrainReservation.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }
        public Discount Discount { get; set; }
        public int DiscountId { get; set; }
        public Seat Seat { get; set; }
        public int SeatId { get; set; }
        public TrainReservationUser TrainReservationUser { get; set; }
        public string SessionKey { get; set; }
        public string TrainReservationUserId { get; set; }
        public string PassengerName { get; set; }
        public string PassengerSurname { get; set; }
    }
}
