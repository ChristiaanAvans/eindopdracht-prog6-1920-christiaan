using FarmManager.Models.Domain;
using FarmManager.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FarmManager.Models.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        public FarmContext Context { get; set; }

        public BookingRepository()
        {
            Context = new FarmContext();
        }

        public List<Booking> GetBookings()
        {
            return Context.Bookings.Include("Animals").Include("Accessoires").ToList();
        }

        public Booking GetBooking(int bookingId)
        {
            return Context.Bookings.Include("Animals").Include("Accessoires").Where(b => b.Id == bookingId).FirstOrDefault();
        }

        public bool AddBooking(Booking booking)
        {
            try
            {
                Animal[] animals = new Animal[booking.Animals.Count()];
                booking.Animals.CopyTo(animals, 0);
                booking.Animals.Clear();
                foreach (var animal in animals)
                    booking.Animals.Add(Context.Animals.Find(animal.Id));

                Accessoire[] accessoires = new Accessoire[booking.Accessoires.Count()];
                booking.Accessoires.CopyTo(accessoires, 0);
                booking.Accessoires.Clear();
                foreach (var accessoire in accessoires)
                    booking.Accessoires.Add(Context.Accessoires.Find(accessoire.Id));

                Context.Bookings.Add(booking);

                Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteBooking(int bookingId)
        {
            try
            {
                Context.Bookings.Remove(Context.Bookings.Find(bookingId));
                Context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}