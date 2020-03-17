using FarmManager.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FarmManager.Models.Repositories
{
    public interface IAnimalRepository
    {
        FarmContext Context { get; set; }

        List<Animal> GetAnimals();

        Animal GetAnimal(int id);

        bool AddAnimal(Animal animal);

        bool EditAnimal(Animal animal);

        bool DeleteAnimal(int animalId);

        List<string> GetAnimalTypes();
    }
}