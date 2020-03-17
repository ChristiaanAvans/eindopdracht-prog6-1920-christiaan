using FarmManager.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FarmManager.Models.Repositories
{
    public class AnimalRepository : IAnimalRepository
    {
        public FarmContext Context { get; set; }

        public AnimalRepository()
        {
            Context = new FarmContext();
        }

        public List<Animal> GetAnimals()
        {
            return Context.Animals.ToList();
        }

        public Animal GetAnimal(int id)
        {
            return Context.Animals.Find(id);
        }

        public bool AddAnimal(Animal animal)
        {
            try
            {
                Context.Animals.Add(animal);
                Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool EditAnimal(Animal animal)
        {
            try
            {
                var toEdit = Context.Animals.Find(animal.Id);
                toEdit.Name = animal.Name;
                toEdit.Price = animal.Price;
                toEdit.ImageUrl = animal.ImageUrl;
                toEdit.TypeName = animal.TypeName;

                Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteAnimal(int animalId)
        {
            try
            {
                var animalToRemove = Context.Animals.Find(animalId);

                foreach (var accessoire in animalToRemove.Accessoires)
                {
                    var accessoireToRemove = Context.Accessoires.Find(accessoire.Id);
                    Context.Accessoires.Remove(accessoireToRemove);
                }

                Context.Animals.Remove(animalToRemove);

                Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<string> GetAnimalTypes()
        {
            List<string> types = new List<string>();
            foreach (var type in Context.Types)
                types.Add(type.Name);

            return types;
        }
    }
}