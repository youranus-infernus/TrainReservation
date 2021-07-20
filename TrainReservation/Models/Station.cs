using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrainReservation.Models
{
    public class Station
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Station Name")]
        public string Name { get; set; }
    }
}
