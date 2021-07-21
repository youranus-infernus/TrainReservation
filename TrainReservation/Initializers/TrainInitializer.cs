using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainReservation.Data;
using TrainReservation.Models;

namespace TrainReservation.Initializers
{
    public class TrainInitializer
    {
        public static async Task InitializeAsync(ApplicationDbContext context)
        {
            if (!context.Trains.Any())
            {
                await context.Trains.AddRangeAsync(
                    new Train
                    {
                        Name = "IC6608",
                        RailCars = new List<RailCar>
                        {
                            new RailCar()
                            {
                                RailcarNumber = 1,
                                Seats = new List<Seat>
                                {
                                    new Seat
                                    {
                                        Price = 20,
                                        SeatNumber = 1
                                    },
                                    new Seat
                                    {
                                        Price = 20,
                                        SeatNumber = 2
                                    },
                                    new Seat
                                    {
                                        Price = 15,
                                        SeatNumber = 3
                                    },
                                    new Seat
                                    {
                                        Price = 15,
                                        SeatNumber = 4
                                    },
                                }
                            },
                            new RailCar()
                            {
                                RailcarNumber = 2,
                                Seats = new List<Seat>
                                {
                                    new Seat
                                    {
                                        Price = 20,
                                        SeatNumber = 5
                                    },
                                    new Seat
                                    {
                                        Price = 20,
                                        SeatNumber = 6
                                    },
                                    new Seat
                                    {
                                        Price = 15,
                                        SeatNumber = 7
                                    },
                                    new Seat
                                    {
                                        Price = 15,
                                        SeatNumber = 8
                                    },
                                }
                            }
                        }
                    },
                    new Train
                    {
                        Name = "PP1007",
                        RailCars = new List<RailCar>
                        {
                            new RailCar()
                            {
                                RailcarNumber = 1,
                                Seats = new List<Seat>
                                {
                                    new Seat
                                    {
                                        Price = 20,
                                        SeatNumber = 1
                                    },
                                    new Seat
                                    {
                                        Price = 20,
                                        SeatNumber = 2
                                    },
                                    new Seat
                                    {
                                        Price = 15,
                                        SeatNumber = 3
                                    },
                                    new Seat
                                    {
                                        Price = 15,
                                        SeatNumber = 4
                                    },
                                }
                            },
                            new RailCar()
                            {
                                RailcarNumber = 2,
                                Seats = new List<Seat>
                                {
                                    new Seat
                                    {
                                        Price = 20,
                                        SeatNumber = 5
                                    },
                                    new Seat
                                    {
                                        Price = 20,
                                        SeatNumber = 6
                                    },
                                    new Seat
                                    {
                                        Price = 15,
                                        SeatNumber = 7
                                    },
                                    new Seat
                                    {
                                        Price = 15,
                                        SeatNumber = 8
                                    },
                                }
                            }
                        }
                    }
                ) ;
                context.SaveChanges();
            }
        }
    }
}
