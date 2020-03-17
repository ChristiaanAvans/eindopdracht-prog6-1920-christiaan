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
    public class HomeController : Controller
    {
        public IAnimalRepository AnimalRepo { get; set; }
        public IAccessoireRepository AccessoireRepo { get; set; }
        public IBookingRepository BookingRepo { get; set; }

        public HomeController()
        {
            AnimalRepo = new AnimalRepository();
            AccessoireRepo = new AccessoireRepository();
            BookingRepo = new BookingRepository();
        }

        public HomeController(IAnimalRepository animalRepo, IAccessoireRepository accessoireRepo, IBookingRepository bookingRepo)
        {
            AnimalRepo = animalRepo;
            AccessoireRepo = accessoireRepo;
            BookingRepo = bookingRepo;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var VM = new BookingVM()
            {
                Booking = new Booking(),
                Accessoires = new List<Accessoire>()
            };
            TempData["Booking"] = VM;

            return View(new DateBookingVM());
        }

        [HttpPost]
        public ActionResult Index(DateBookingVM dateBookingVM)
        {
            var tempBooking = (BookingVM)TempData["Booking"];
            tempBooking.Booking.BookingDate = dateBookingVM.BookingDate;
            TempData["Booking"] = tempBooking;

            if(ModelState.IsValid)
                return RedirectToAction("FirstStep");
            return View(dateBookingVM);
        }

        [HttpGet]
        public ActionResult FirstStep()
        {
            var animalBookingVM = new AnimalBookingVM() { Animals = AnimalRepo.GetAnimals(), UnavailableAnimals = new List<Animal>() };
            var bookingVM = (BookingVM)TempData["Booking"];

            foreach(var animal in animalBookingVM.Animals)
                foreach(var booking in animal.Bookings)
                    if (booking.BookingDate == bookingVM.Booking.BookingDate)
                        animalBookingVM.UnavailableAnimals.Add(animal);
            animalBookingVM.BookingDate = bookingVM.Booking.BookingDate;

            TempData["Booking"] = bookingVM;

            return View(animalBookingVM);
        }

        [HttpPost]
        public ActionResult FirstStep(AnimalBookingVM animalBookingVM)
        {
            ModelState.Remove("Animals");
            ModelState.Remove("UnavailableAnimals");

            var tempBooking = (BookingVM)TempData["Booking"];
            animalBookingVM.BookingDate = tempBooking.Booking.BookingDate;

            if (!ModelState.IsValid)
            {
                animalBookingVM.Animals = AnimalRepo.GetAnimals();
                animalBookingVM.UnavailableAnimals = new List<Animal>();
                foreach (var animal in animalBookingVM.Animals)
                    foreach (var booking in animal.Bookings)
                        if (booking.BookingDate == tempBooking.Booking.BookingDate)
                            animalBookingVM.UnavailableAnimals.Add(animal);
                animalBookingVM.BookingDate = tempBooking.Booking.BookingDate;
                TempData["Booking"] = tempBooking;
                return View(animalBookingVM);
            }

            
            foreach (var animalId in animalBookingVM.AnimalIds)
                tempBooking.Booking.Animals.Add(AnimalRepo.GetAnimal(animalId));
            TempData["Booking"] = tempBooking;

            return RedirectToAction("SecondStep");
        }

        [HttpGet]
        public ActionResult SecondStep()
        {
            var bookingVM = (BookingVM)TempData["Booking"];

            foreach (var animal in bookingVM.Booking.Animals)
                foreach (var accessoire in AnimalRepo.GetAnimal(animal.Id).Accessoires)
                    bookingVM.Accessoires.Add(accessoire);
            TempData["Booking"] = bookingVM;

            return View(bookingVM);
        }

        [HttpPost]
        public ActionResult SecondStep(BookingVM bookingVM)
        {
            var tempBooking = (BookingVM)TempData["Booking"];
            if(bookingVM.AccessoireIds != null)
                foreach (var accessoireId in bookingVM.AccessoireIds)
                    tempBooking.Booking.Accessoires.Add(AccessoireRepo.GetAccessoire(accessoireId));
            tempBooking.AccessoireIds = bookingVM.AccessoireIds;
            TempData["Booking"] = tempBooking;

            return RedirectToAction("ThirdStep");
        }

        [HttpGet]
        public ActionResult ThirdStep()
        {
            var tempBooking = (BookingVM)TempData["Booking"];
            TempData["Booking"] = tempBooking;

            return View(new ContactInfoBookingVM() { Booking = tempBooking.Booking });
        }

        [HttpPost]
        public ActionResult ThirdStep(ContactInfoBookingVM ciBookingVM)
        {
            if (!ModelState.IsValid)
            {
                var bookingVM = (BookingVM)TempData["Booking"];
                TempData["Booking"] = bookingVM;
                ciBookingVM.Booking = bookingVM.Booking;

                return View(ciBookingVM);
            }

            var tempBooking = (BookingVM)TempData["Booking"];
            tempBooking.Booking.FirstName = ciBookingVM.FirstName;
            tempBooking.Booking.LastName = ciBookingVM.LastName;
            tempBooking.Booking.Insertion = ciBookingVM.Insertion;
            tempBooking.Booking.Address = ciBookingVM.Address;
            tempBooking.Booking.Mail = ciBookingVM.Mail;
            tempBooking.Booking.PhoneNumber = ciBookingVM.PhoneNumber;

            TempData["Booking"] = tempBooking;
            return RedirectToAction("ForthStep");
        }

        [HttpGet]
        public ActionResult ForthStep()
        {
            var tempBooking = (BookingVM)TempData["Booking"];
            TempData["Booking"] = tempBooking;

            tempBooking.CalculateDiscounts(AnimalRepo.GetAnimalTypes(), new Random().Next(0, 6));
            CalculatePrice(tempBooking);

            return View(tempBooking);
        }

        [HttpPost]
        public ActionResult ForthStep(BookingVM bookingVM)
        {
            var tempBooking = (BookingVM)TempData["Booking"];
            if(BookingRepo.AddBooking(tempBooking.Booking))
                return RedirectToAction("Index");

            TempData["Booking"] = tempBooking;
            return View(tempBooking);
        }

        private void CalculatePrice(BookingVM tempBooking)
        {
            tempBooking.TotalPrice = 0;
            foreach (var animal in tempBooking.Booking.Animals)
                tempBooking.TotalPrice += animal.Price;
            foreach (var accessoire in tempBooking.Booking.Accessoires)
                tempBooking.TotalPrice += accessoire.Price;
            tempBooking.TotalPrice = tempBooking.TotalPrice / 100 * (100 - tempBooking.TotalDiscount);
        }
    }
}