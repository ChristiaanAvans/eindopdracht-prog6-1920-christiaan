using FarmManager.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FarmManager.Models.Repositories
{
    public interface IBookingRepository
    {
        FarmContext Context { get; set; }

        List<Booking> GetBookings();

        Booking GetBooking(int bookingId);

        bool AddBooking(Booking booking);

        bool DeleteBooking(int bookingId);
    }
}