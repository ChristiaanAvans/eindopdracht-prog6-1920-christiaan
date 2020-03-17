using System;
using System.Collections.Generic;
using FarmManager.Models.Domain;
using FarmManager.Models.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FarmManager.Test
{
    [TestClass]
    public class Discount_Tests
    {
        [TestMethod]
        public void AnimalTypeTest()
        {
            //Arrange
            var bookingVM = new BookingVM() { Booking = new Booking(), Discounts = new Dictionary<string, int>() };

            var animal1 = new Animal() { TypeName = "Woestijn" };
            var animal2 = new Animal() { TypeName = "Woestijn" };
            var animal3 = new Animal() { TypeName = "Woestijn" };
            bookingVM.Booking.Animals.Add(animal1);
            bookingVM.Booking.Animals.Add(animal2);
            bookingVM.Booking.Animals.Add(animal3);

            var types = new List<string>() { "Woestijn", "Sneeuw", "Boerderij", "Jungle" };

            //Act
            bookingVM.GetAnimalTypeDiscount(types);

            //Assert
            int result;
            bookingVM.Discounts.TryGetValue("3 types", out result);
            Assert.AreEqual(10, result);
        }

        [TestMethod]
        public void AnimalTypeTestFail()
        {
            //Arrange
            var bookingVM = new BookingVM() { Booking = new Booking(), Discounts = new Dictionary<string, int>() };

            var animal1 = new Animal() { TypeName = "Woestijn" };
            var animal2 = new Animal() { TypeName = "Woestijn" };
            var animal3 = new Animal() { TypeName = "Boerderij" };
            bookingVM.Booking.Animals.Add(animal1);
            bookingVM.Booking.Animals.Add(animal2);
            bookingVM.Booking.Animals.Add(animal3);

            var types = new List<string>() { "Woestijn", "Sneeuw", "Boerderij", "Jungle" };

            //Act
            bookingVM.GetAnimalTypeDiscount(types);

            //Assert
            int result;
            bookingVM.Discounts.TryGetValue("3 types", out result);
            Assert.AreNotEqual(10, result);
        }

        [TestMethod]
        public void DuckDiscountTest()
        {
            //Arrange
            var bookingVM = new BookingVM() { Booking = new Booking(), Discounts = new Dictionary<string, int>() };
            var animal = new Animal() { Name = "Eend" };
            bookingVM.Booking.Animals.Add(animal);
            int randomNumber = 0;

            //Act
            bookingVM.GetDuckDiscount(randomNumber);

            //Assert
            int result;
            bookingVM.Discounts.TryGetValue("Eend", out result);
            Assert.AreEqual(50, result);
        }

        [TestMethod]
        public void DuckDiscountFailTest()
        {
            //Arrange
            var bookingVM = new BookingVM() { Booking = new Booking(), Discounts = new Dictionary<string, int>() };
            var animal = new Animal() { Name = "Eend" };
            bookingVM.Booking.Animals.Add(animal);
            int randomNumber = 4;

            //Act
            bookingVM.GetDuckDiscount(randomNumber);

            //Assert
            int result;
            bookingVM.Discounts.TryGetValue("Eend", out result);
            Assert.AreNotEqual(50, result);
        }

        [TestMethod]
        public void DuckDiscountNotDuckTest()
        {
            //Arrange
            var bookingVM = new BookingVM() { Booking = new Booking(), Discounts = new Dictionary<string, int>() };
            var animal = new Animal() { Name = "Leeuw" };
            bookingVM.Booking.Animals.Add(animal);
            int randomNumber = 0;

            //Act
            bookingVM.GetDuckDiscount(randomNumber);

            //Assert
            int result;
            bookingVM.Discounts.TryGetValue("Eend", out result);
            Assert.AreNotEqual(50, result);
        }

        [TestMethod]
        public void MondayDiscountTest()
        {
            //Arrange
            var bookingVM = new BookingVM() { Booking = new Booking(), Discounts = new Dictionary<string, int>() };
            bookingVM.Booking.BookingDate = DateTime.Parse("13-4-2020");

            //Act
            bookingVM.GetStartOfWeekDiscount();

            //Assert
            int result;
            bookingVM.Discounts.TryGetValue("Boeking op Monday", out result);
            Assert.AreEqual(15, result);
        }

        [TestMethod]
        public void TuesdayDiscountTest()
        {
            //Arrange
            var bookingVM = new BookingVM() { Booking = new Booking(), Discounts = new Dictionary<string, int>() };
            bookingVM.Booking.BookingDate = DateTime.Parse("14-4-2020");

            //Act
            bookingVM.GetStartOfWeekDiscount();

            //Assert
            int result;
            bookingVM.Discounts.TryGetValue("Boeking op Tuesday", out result);
            Assert.AreEqual(15, result);
        }

        [TestMethod]
        public void StartOfWeekDiscountTestFail()
        {
            //Arrange
            var bookingVM = new BookingVM() { Booking = new Booking(), Discounts = new Dictionary<string, int>() };
            bookingVM.Booking.BookingDate = DateTime.Parse("15-4-2020");

            //Act
            bookingVM.GetStartOfWeekDiscount();

            //Assert
            int result;
            bookingVM.Discounts.TryGetValue("Boeking op Wednesday", out result);
            Assert.AreNotEqual(15, result);
        }

        [TestMethod]
        public void LetterDiscountTest()
        {
            //Arrange
            var bookingVM = new BookingVM() { Booking = new Booking(), Discounts = new Dictionary<string, int>() };

            var animal1 = new Animal() { Name = "abcdefgh" };
            var animal2 = new Animal() { Name = "abcefghi" };
            var animal3 = new Animal() { Name = "barencd" };
            bookingVM.Booking.Animals.Add(animal1);
            bookingVM.Booking.Animals.Add(animal2);
            bookingVM.Booking.Animals.Add(animal3);

            //Act
            bookingVM.GetLetterDiscount();

            //Assert
            int result1;
            bookingVM.Discounts.TryGetValue("Letter korting abcdefgh", out result1);
            int result2;
            bookingVM.Discounts.TryGetValue("Letter korting abcefghi", out result2);
            int result3;
            bookingVM.Discounts.TryGetValue("Letter korting barencd", out result3);

            Assert.AreEqual(16, result1);
            Assert.AreEqual(6, result2);
            Assert.AreEqual(10, result3);
        }

        [TestMethod]
        public void CalculateTotalDiscountTest()
        {
            //Arrange
            var bookingVM = new BookingVM() { Booking = new Booking() { BookingDate = DateTime.Parse("13-4-2020") } };

            List<Animal> animals = new List<Animal>()
            {
                new Animal()
                {
                    Name = "abcdefghij",
                    TypeName = "Woestijn"
                },
                new Animal()
                {
                    Name = "pablo",
                    TypeName = "Woestijn"
                },
                new Animal()
                {
                    Name = "leeuw",
                    TypeName = "Woestijn"
                }
            };
            bookingVM.Booking.Animals = animals;

            List<string> types = new List<string>() { "Woestijn", "Boerderij", "Jungle", "Sneeuw" };

            //Act
            bookingVM.CalculateDiscounts(types, 3);

            //Assert
            Assert.AreEqual(49, bookingVM.TotalDiscount);
        }

        [TestMethod]
        public void CalculateTotalDiscountNoMoreThen60Test()
        {
            //Arrange
            var bookingVM = new BookingVM() { Booking = new Booking() { BookingDate = DateTime.Parse("13-4-2020") } };

            List<Animal> animals = new List<Animal>()
            {
                new Animal()
                {
                    Name = "abcdefghijklmnopqrstuvwxyz",
                    TypeName = "Woestijn"
                },
                new Animal()
                {
                    Name = "pablo",
                    TypeName = "Woestijn"
                },
                new Animal()
                {
                    Name = "leeuw",
                    TypeName = "Woestijn"
                }
            };
            bookingVM.Booking.Animals = animals;

            List<string> types = new List<string>() { "Woestijn", "Boerderij", "Jungle", "Sneeuw" };

            //Act
            bookingVM.CalculateDiscounts(types, 0);

            //Assert
            Assert.AreEqual(60, bookingVM.TotalDiscount);
        }
    }
}
