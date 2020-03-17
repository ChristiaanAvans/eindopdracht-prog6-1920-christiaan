using FarmManager.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FarmManager.Models.ViewModels
{
    public class AnimalVM
    {
        public Animal Animal { get; set; }
        public string ChosenAnimalType { get; set; }
        public List<string> AnimalTypes { get; set; }
    }
}