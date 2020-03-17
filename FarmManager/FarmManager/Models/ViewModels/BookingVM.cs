using FarmManager.Models.Domain;
using FarmManager.Models.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FarmManager.Models.ViewModels
{
    public class BookingVM 
    {
        public decimal TotalPrice { get; set; }
        public int TotalDiscount { get; set; }
        public Booking Booking { get; set; }
        public List<int> AccessoireIds { get; set; }
        public List<Accessoire> Accessoires { get; set; }
        public Dictionary<string, int> Discounts { get; set; }

        public void CalculateDiscounts(List<string> types, int random)
        {
            Discounts = new Dictionary<string, int>();

            GetAnimalTypeDiscount(types);
            GetDuckDiscount(random);
            GetStartOfWeekDiscount();
            GetLetterDiscount();
            GetTotalDiscount();
        }

        public void GetAnimalTypeDiscount(List<string> types)
        {
            foreach (var type in types)
            {
                int sameType = 0;
                foreach (var animal in Booking.Animals)
                    if (animal.TypeName.ToLower().Equals(type.ToLower()))
                        sameType++;
                if (sameType >= 3)
                {
                    Discounts.Add("3 types", 10);
                    break;
                }
            }
        }

        public void GetDuckDiscount(int random)
        {
            foreach (var animal in Booking.Animals)
                if (animal.Name.ToLower().Equals("eend"))
                    if (random == 0)
                        Discounts.Add("Eend", 50);
        }

        public void GetStartOfWeekDiscount()
        {
            if (Booking.BookingDate.DayOfWeek == DayOfWeek.Monday || Booking.BookingDate.DayOfWeek == DayOfWeek.Tuesday)
                Discounts.Add("Boeking op " + Booking.BookingDate.DayOfWeek.ToString(), 15);
        }

        public void GetLetterDiscount()
        {
            foreach (var animal in Booking.Animals)
            {
                int letterDiscount = 0;
                foreach (var letter in "abcdefghijklmnopqrstuvwxyz".ToCharArray())
                    if (animal.Name.Contains(letter))
                        letterDiscount += 2;
                    else
                        break;

                if (letterDiscount > 0)
                    Discounts.Add("Letter korting " + animal.Name, letterDiscount);
            }
        }

        public void GetTotalDiscount()
        {
            TotalDiscount = 0;
            foreach (var discount in Discounts)
            {
                TotalDiscount += discount.Value;
            }

            if (TotalDiscount > 60)
                TotalDiscount = 60;
        }
    }
}