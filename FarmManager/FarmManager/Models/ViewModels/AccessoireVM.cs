using FarmManager.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FarmManager.Models.ViewModels
{
    public class AccessoireVM
    {
        public Accessoire Accessoire { get; set; }
        public int AnimalId { get; set; }
        public List<Animal> Animals { get; set; }
    }
}