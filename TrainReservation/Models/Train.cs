using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrainReservation.Models
{
    public class Train
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Train Name")]
        [Required]
        public string Name { get; set; }
    }
}
