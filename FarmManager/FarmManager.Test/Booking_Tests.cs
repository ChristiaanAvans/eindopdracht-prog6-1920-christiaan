using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using FarmManager.Controllers;
using FarmManager.Models.Domain;
using FarmManager.Models.Repositories;
using FarmManager.Models.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FarmManager.Test
{
    [TestClass]
    public class Booking_Tests
    {
        [TestMethod]
        public void BookingInTheFuture()
        {
            //Arrange
            Mock<IAnimalRepository> animalRepo = new Mock<IAnimalRepository>();
            Mock<IAccessoireRepository> accessoireRepo = new Mock<IAccessoireRepository>();
            Mock<IBookingRepository> bookingRepo = new Mock<IBookingRepository>();
            var controller = new HomeController(animalRepo.Object, accessoireRepo.Object, bookingRepo.Object);
            
            var dateBookingVM = new DateBookingVM() { BookingDate = DateTime.Today.AddDays(1) };
            controller.TempData["Booking"] = new BookingVM() { Booking = new Booking() };

            //Act
            var result = controller.Index(dateBookingVM) as RedirectToRouteResult;

            //Assert
            Assert.IsTrue("FirstStep".Equals(result.RouteValues["action"]));
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void BookingInFutureFail()
        {
            //Arrange
            Mock<IAnimalRepository> animalRepo = new Mock<IAnimalRepository>();
            Mock<IAccessoireRepository> accessoireRepo = new Mock<IAccessoireRepository>();
            Mock<IBookingRepository> bookingRepo = new Mock<IBookingRepository>();
            var controller = new HomeController(animalRepo.Object, accessoireRepo.Object, bookingRepo.Object);

            var dateBookingVM = new DateBookingVM() { BookingDate = DateTime.Today.AddDays(-1) };
            controller.TempData["Booking"] = new BookingVM() { Booking = new Booking() };

            //Act
            controller.ModelState.AddModelError("BookingDate", "BookingDate cannot be in the past");
            var result = controller.Index(dateBookingVM) as RedirectToRouteResult;

            //Assert
            Assert.IsFalse("FirstStep".Equals(result.RouteValues["action"]));
        }

        [TestMethod]
        public void AtLeastOneAnimalSelected()
        {
            //Arrange
            var animalBookingVM = new AnimalBookingVM() { BookingDate = DateTime.Parse("12-1-2020"), AnimalIds = new List<int>() };

            var errors = new List<ValidationResult>();

            //Act
            animalBookingVM.GetMoreThenZeroChosenAnimalsRule(errors);

            //Assert
            Assert.IsTrue(errors.Count == 1);
        }

        [TestMethod]
        public void AtLeastOneAnimalSelectedFail()
        {
            //Arrange
            var animalBookingVM = new AnimalBookingVM() { BookingDate = DateTime.Parse("12-1-2020"), AnimalIds = new List<int>() { 1 } };

            var errors = new List<ValidationResult>();

            //Act
            animalBookingVM.GetMoreThenZeroChosenAnimalsRule(errors);

            //Assert
            Assert.IsFalse(errors.Count == 1);
        }

        [TestMethod]
        public void NoPinguinInWeekends()
        {
            //Arrange
            var animalBookingVM = new AnimalBookingVM() { BookingDate = DateTime.Parse("12-1-2020") };

            var errors = new List<ValidationResult>();
            var chosenAnimals = new List<Animal>()
            {
                new Animal()
                {
                    Name = "Pinguin"
                }
            };

            //Act
            animalBookingVM.GetPinguinRule(errors, chosenAnimals);

            //Assert
            Assert.IsTrue(errors.Count == 1);
        }

        [TestMethod]
        public void NoPinguinInWeekendsFail()
        {
            //Arrange
            var animalBookingVM = new AnimalBookingVM() { BookingDate = DateTime.Parse("12-1-2020") };

            var errors = new List<ValidationResult>();
            var chosenAnimals = new List<Animal>()
            {
                new Animal()
                {
                    Name = "Leeuw"
                }
            };

            //Act
            animalBookingVM.GetPinguinRule(errors, chosenAnimals);

            //Assert
            Assert.IsFalse(errors.Count == 1);
        }

        [TestMethod]
        public void LionNotWithFarmAnimal()
        {
            //Arrange
            var animalBookingVM = new AnimalBookingVM() { BookingDate = DateTime.Parse("12-1-2020") };

            var errors = new List<ValidationResult>();
            var chosenAnimals = new List<Animal>()
            {
                new Animal()
                {
                    Name = "Leeuw",
                    TypeName = "Woestijn"
                },
                new Animal()
                {
                    Name = "Koe",
                    TypeName = "Boerderij"
                }
            };

            //Act
            animalBookingVM.GetLionOrPolarbearNotWithFarmAnimalRule(errors, chosenAnimals);

            //Assert
            Assert.IsTrue(errors.Count == 1);
        }

        [TestMethod]
        public void PolarBearNotWithFarmAnimal()
        {
            //Arrange
            var animalBookingVM = new AnimalBookingVM() { BookingDate = DateTime.Parse("12-1-2020") };

            var errors = new List<ValidationResult>();
            var chosenAnimals = new List<Animal>()
            {
                new Animal()
                {
                    Name = "Ijsbeer",
                    TypeName = "Woestijn"
                },
                new Animal()
                {
                    Name = "Koe",
                    TypeName = "Boerderij"
                }
            };

            //Act
            animalBookingVM.GetLionOrPolarbearNotWithFarmAnimalRule(errors, chosenAnimals);

            //Assert
            Assert.IsTrue(errors.Count == 1);
        }

        [TestMethod]
        public void LionOrPolarBearNotWithFarmAnimalFail()
        {
            //Arrange
            var animalBookingVM = new AnimalBookingVM() { BookingDate = DateTime.Parse("12-1-2020") };

            var errors = new List<ValidationResult>();
            var chosenAnimals = new List<Animal>()
            {
                new Animal()
                {
                    Name = "Slang",
                    TypeName = "Woestijn"
                },
                new Animal()
                {
                    Name = "Koe",
                    TypeName = "Boerderij"
                }
            };

            //Act
            animalBookingVM.GetLionOrPolarbearNotWithFarmAnimalRule(errors, chosenAnimals);

            //Assert
            Assert.IsFalse(errors.Count == 1);
        }

        [TestMethod]
        public void DesertAnimalNotInOktoberToFebuariOktoberTest()
        {
            //Arrange
            var animalBookingVM = new AnimalBookingVM() { BookingDate = DateTime.Parse("1-10-2020") };

            var errors = new List<ValidationResult>();
            var chosenAnimals = new List<Animal>()
            {
                new Animal()
                {
                    Name = "Leeuw",
                    TypeName = "Woestijn"
                }
            };

            //Act
            animalBookingVM.GetDesertAnimalsNotInOktoberToFebuari(errors, chosenAnimals);

            //Assert
            Assert.IsTrue(errors.Count == 1);
        }

        [TestMethod]
        public void DesertAnimalNotInOktoberToFebuariFebuariTest()
        {
            //Arrange
            var animalBookingVM = new AnimalBookingVM() { BookingDate = DateTime.Parse("1-2-2020") };

            var errors = new List<ValidationResult>();
            var chosenAnimals = new List<Animal>()
            {
                new Animal()
                {
                    Name = "Leeuw",
                    TypeName = "Woestijn"
                }
            };

            //Act
            animalBookingVM.GetDesertAnimalsNotInOktoberToFebuari(errors, chosenAnimals);

            //Assert
            Assert.IsTrue(errors.Count == 1);
        }

        [TestMethod]
        public void DesertAnimalNotInOktoberToFebuariDecemberTest()
        {
            //Arrange
            var animalBookingVM = new AnimalBookingVM() { BookingDate = DateTime.Parse("1-12-2020") };

            var errors = new List<ValidationResult>();
            var chosenAnimals = new List<Animal>()
            {
                new Animal()
                {
                    Name = "Leeuw",
                    TypeName = "Woestijn"
                }
            };

            //Act
            animalBookingVM.GetDesertAnimalsNotInOktoberToFebuari(errors, chosenAnimals);

            //Assert
            Assert.IsTrue(errors.Count == 1);
        }

        [TestMethod]
        public void DesertAnimalNotInOktoberToFebuariJuneTest()
        {
            //Arrange
            var animalBookingVM = new AnimalBookingVM() { BookingDate = DateTime.Parse("1-6-2020") };

            var errors = new List<ValidationResult>();
            var chosenAnimals = new List<Animal>()
            {
                new Animal()
                {
                    Name = "Leeuw",
                    TypeName = "Woestijn"
                }
            };

            //Act
            animalBookingVM.GetDesertAnimalsNotInOktoberToFebuari(errors, chosenAnimals);

            //Assert
            Assert.IsFalse(errors.Count == 1);
        }

        [TestMethod]
        public void SnowAnimalsNotInJuneToAugustJuneTest()
        {
            //Arrange
            var animalBookingVM = new AnimalBookingVM() { BookingDate = DateTime.Parse("1-6-2020") };

            var errors = new List<ValidationResult>();
            var chosenAnimals = new List<Animal>()
            {
                new Animal()
                {
                    Name = "Pinguin",
                    TypeName = "Sneeuw"
                }
            };

            //Act
            animalBookingVM.GetSnowAnimalsNotInJuneToAugust(errors, chosenAnimals);

            //Assert
            Assert.IsTrue(errors.Count == 1);
        }

        [TestMethod]
        public void SnowAnimalsNotInJuneToAugustJulyTest()
        {
            //Arrange
            var animalBookingVM = new AnimalBookingVM() { BookingDate = DateTime.Parse("1-7-2020") };

            var errors = new List<ValidationResult>();
            var chosenAnimals = new List<Animal>()
            {
                new Animal()
                {
                    Name = "Pinguin",
                    TypeName = "Sneeuw"
                }
            };

            //Act
            animalBookingVM.GetSnowAnimalsNotInJuneToAugust(errors, chosenAnimals);

            //Assert
            Assert.IsTrue(errors.Count == 1);
        }

        [TestMethod]
        public void SnowAnimalsNotInJuneToAugustAugustTest()
        {
            //Arrange
            var animalBookingVM = new AnimalBookingVM() { BookingDate = DateTime.Parse("1-8-2020") };

            var errors = new List<ValidationResult>();
            var chosenAnimals = new List<Animal>()
            {
                new Animal()
                {
                    Name = "Pinguin",
                    TypeName = "Sneeuw"
                }
            };

            //Act
            animalBookingVM.GetSnowAnimalsNotInJuneToAugust(errors, chosenAnimals);

            //Assert
            Assert.IsTrue(errors.Count == 1);
        }

        [TestMethod]
        public void SnowAnimalsNotInJuneToAugustDecemberTest()
        {
            //Arrange
            var animalBookingVM = new AnimalBookingVM() { BookingDate = DateTime.Parse("1-12-2020") };

            var errors = new List<ValidationResult>();
            var chosenAnimals = new List<Animal>()
            {
                new Animal()
                {
                    Name = "Pinguin",
                    TypeName = "Sneeuw"
                }
            };

            //Act
            animalBookingVM.GetSnowAnimalsNotInJuneToAugust(errors, chosenAnimals);

            //Assert
            Assert.IsFalse(errors.Count == 1);
        }

        [TestMethod]
        public void ContactInfoFirstNameConnotBeNull()
        {
            //Arrange
            var contactInfoVM = new ContactInfoBookingVM() { LastName = "test", Insertion = "test", Address = "test", Mail = "test", PhoneNumber = "test", Booking = new Booking() };
            var errors = new List<ValidationResult>();

            //Act
            contactInfoVM.GetErrors(errors);

            //Assert
            Assert.IsTrue(errors.Count > 0);
        }

        [TestMethod]
        public void ContactInfoFirstNameConnotBeNullFail()
        {
            //Arrange
            var contactInfoVM = new ContactInfoBookingVM() { LastName = "test", FirstName = "Test", Insertion = "test", Address = "test", Mail = "test", PhoneNumber = "test", Booking = new Booking() };
            var errors = new List<ValidationResult>();

            //Act
            contactInfoVM.GetErrors(errors);

            //Assert
            Assert.IsFalse(errors.Count > 0);
        }

        [TestMethod]
        public void ContactInfoLastNameConnotBeNull()
        {
            //Arrange
            var contactInfoVM = new ContactInfoBookingVM() { FirstName = "test", Insertion = "test", Address = "test", Mail = "test", PhoneNumber = "test", Booking = new Booking() };
            var errors = new List<ValidationResult>();

            //Act
            contactInfoVM.GetErrors(errors);

            //Assert
            Assert.IsTrue(errors.Count > 0);
        }

        [TestMethod]
        public void ContactInfoLastNameConnotBeNullFail()
        {
            //Arrange
            var contactInfoVM = new ContactInfoBookingVM() { FirstName = "test", LastName = "test", Insertion = "test", Address = "test", Mail = "test", PhoneNumber = "test", Booking = new Booking() };
            var errors = new List<ValidationResult>();

            //Act
            contactInfoVM.GetErrors(errors);

            //Assert
            Assert.IsFalse(errors.Count > 0);
        }

        [TestMethod]
        public void ContactInfoAddressConnotBeNull()
        {
            //Arrange
            var contactInfoVM = new ContactInfoBookingVM() { LastName = "test", Insertion = "test", FirstName = "test", Mail = "test", PhoneNumber = "test", Booking = new Booking() };
            var errors = new List<ValidationResult>();

            //Act
            contactInfoVM.GetErrors(errors);

            //Assert
            Assert.IsTrue(errors.Count > 0);
        }

        [TestMethod]
        public void ContactInfoAddressConnotBeNullFail()
        {
            //Arrange
            var contactInfoVM = new ContactInfoBookingVM() { LastName = "test", Address = "test", Insertion = "test", FirstName = "test", Mail = "test", PhoneNumber = "test", Booking = new Booking() };
            var errors = new List<ValidationResult>();

            //Act
            contactInfoVM.GetErrors(errors);

            //Assert
            Assert.IsFalse(errors.Count > 0);
        }
    }
}
