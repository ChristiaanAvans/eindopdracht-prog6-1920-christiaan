using FarmManager.Models.Domain;
using FarmManager.Models.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FarmManager.Models.ViewModels
{
    public class AnimalBookingVM : IValidatableObject
    {
        public DateTime BookingDate { get; set; }
        public List<int> AnimalIds { get; set; }
        public List<Animal> Animals { get; set; }
        public List<Animal> UnavailableAnimals { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            Animals = new AnimalRepository().GetAnimals();
            var chosenAnimals = new List<Animal>();
            if (AnimalIds != null)
                foreach (var animalId in AnimalIds)
                    foreach (var animal in Animals)
                        if (animal.Id == animalId)
                            chosenAnimals.Add(animal);

            GetMoreThenZeroChosenAnimalsRule(results);
            GetPinguinRule(results, chosenAnimals);
            GetLionOrPolarbearNotWithFarmAnimalRule(results, chosenAnimals);
            GetDesertAnimalsNotInOktoberToFebuari(results, chosenAnimals);
            GetSnowAnimalsNotInJuneToAugust(results, chosenAnimals);

            return results;
        }

        public void GetMoreThenZeroChosenAnimalsRule(List<ValidationResult> results)
        {
            if (AnimalIds == null)
                results.Add(new ValidationResult("Je moet minimaal 1 beestje kiezen", new[] { "AnimalIds" }));
            else if (AnimalIds.Count() == 0)
                results.Add(new ValidationResult("Je moet minimaal 1 beestje kiezen", new[] { "AnimalIds" }));
        }

        public void GetPinguinRule(List<ValidationResult> results, List<Animal> chosenAnimals)
        {
            if ((BookingDate.DayOfWeek == DayOfWeek.Saturday) || (BookingDate.DayOfWeek == DayOfWeek.Sunday))
                foreach (var animal in chosenAnimals)
                    if (animal.Name.ToLower().Equals("pinguin"))
                        results.Add(new ValidationResult("Een pinguin kan niet in het weekend geboekt worden", new[] { "AnimalIds" }));
        }

        public void GetLionOrPolarbearNotWithFarmAnimalRule(List<ValidationResult> results, List<Animal> chosenAnimals)
        {
            foreach (var animal in chosenAnimals)
                if (animal.TypeName.ToLower().Equals("boerderij"))
                    foreach (var checkAnimal in chosenAnimals)
                        if (checkAnimal.Name.ToLower().Equals("leeuw") || checkAnimal.Name.ToLower().Equals("ijsbeer"))
                            results.Add(new ValidationResult("Een leeuw of ijbeer kan niet geboekt worden in combinatie met een boerderijdier", new[] { "AnimalIds" }));
        }
        
        public void GetDesertAnimalsNotInOktoberToFebuari(List<ValidationResult> results, List<Animal> chosenAnimals)
        {
            if (BookingDate.Month >= 10 || BookingDate.Month <= 2)
                foreach (var animal in chosenAnimals)
                    if (animal.TypeName.ToLower().Equals("woestijn"))
                        results.Add(new ValidationResult("Woestijndieren kunnen niet geboekt worden van oktober tot en met februari", new[] { "AnimalIds" }));
        }

        public void GetSnowAnimalsNotInJuneToAugust(List<ValidationResult> results, List<Animal> chosenAnimals)
        {
            if (BookingDate.Month >= 6 && BookingDate.Month <= 8)
                foreach (var animal in chosenAnimals)
                    if (animal.TypeName.ToLower().Equals("sneeuw"))
                        results.Add(new ValidationResult("Sneeuwdieren kunnen niet geboekt worden van juni tot en met augustus", new[] { "AnimalIds" }));
        }
    }
}