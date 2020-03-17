using FarmManager.Models.Domain;
using FarmManager.Models.Repositories;
using FarmManager.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FarmManager.Controllers
{
    public class BookingController : Controller
    {
        public BookingRepository BookingRepo { get; set; }
        public AnimalRepository AnimalRepo { get; set; }

        public BookingController()
        {
            BookingRepo = new BookingRepository();
            AnimalRepo = new AnimalRepository();
        }
        public BookingController(BookingRepository bRepo, AnimalRepository aRepo)
        {
            BookingRepo = bRepo;
            AnimalRepo = aRepo;
        }

        public ActionResult Index()
        {
            return View(BookingRepo.GetBookings());
        }

        [HttpGet]
        public ActionResult Details(int bookingId)
        {
            var bookingVM = new BookingVM() { Booking = BookingRepo.GetBooking(bookingId) };
            bookingVM.CalculateDiscounts(AnimalRepo.GetAnimalTypes(), new Random().Next(0, 6));
            bookingVM.TotalPrice = CalculatePrice(bookingVM);

            return View(bookingVM);
        }

        [HttpGet]
        public ActionResult Delete(int bookingId)
        {
            return View(BookingRepo.GetBooking(bookingId));
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int bookingId)
        {
            if (BookingRepo.DeleteBooking(bookingId))
                return RedirectToAction("Index");
            return View(BookingRepo.GetBooking(bookingId));
        }

        private decimal CalculatePrice(BookingVM bookingVM)
        {
            var total = new decimal();
            foreach (var animal in bookingVM.Booking.Animals)
                total += animal.Price;
            foreach (var accessoire in bookingVM.Booking.Accessoires)
                total += accessoire.Price;
            total = total / 100 * (100 - bookingVM.TotalDiscount);
            return total;
        }
    }
}