using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainReservation.Data;
using TrainReservation.Models;

namespace TrainReservation.Initializers
{
    public class DiscountInitializer
    {
        public static async Task InitializeAsync(ApplicationDbContext context)
        {
            if (!context.Discounts.Any())
            {
                await context.Discounts.AddRangeAsync(
                    new Discount
                    {
                        Name = "Students",
                        Percent = 51
                    },
                    new Discount
                    {
                        Name = "Normal",
                        Percent = 0
                    },
                    new Discount
                    {
                        Name = "Pensioners",
                        Percent = 51
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
