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
    public class AnimalController : Controller
    {
        public AnimalRepository AnimalRepo { get; set; }

        public AnimalController()
        {
            AnimalRepo = new AnimalRepository();
        }

        public ActionResult Index()
        {
            return View(AnimalRepo.GetAnimals());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new AnimalVM() { Animal = new Animal(), AnimalTypes = AnimalRepo.GetAnimalTypes() });
        }

        [HttpPost]
        public ActionResult Create(AnimalVM animalVM)
        {
            animalVM.Animal.TypeName = animalVM.ChosenAnimalType;
            if (AnimalRepo.AddAnimal(animalVM.Animal))
                return RedirectToAction("Index");

            animalVM.AnimalTypes = AnimalRepo.GetAnimalTypes();
            return View(animalVM);
        }

        [HttpGet]
        public ActionResult Details(int animalId)
        {
            return View(AnimalRepo.GetAnimal(animalId));
        }

        [HttpGet]
        public ActionResult Edit(int animalId)
        {
            return View(new AnimalVM() { Animal = AnimalRepo.GetAnimal(animalId), AnimalTypes = AnimalRepo.GetAnimalTypes() });
        }

        [HttpPost]
        public ActionResult Edit(AnimalVM animalVM)
        {
            animalVM.Animal.TypeName = animalVM.ChosenAnimalType;
            if (AnimalRepo.EditAnimal(animalVM.Animal))
                return RedirectToAction("Index");

            animalVM.AnimalTypes = AnimalRepo.GetAnimalTypes();
            return View(animalVM);
        }

        [HttpGet]
        public ActionResult Delete(int animalId)
        {
            return View(AnimalRepo.GetAnimal(animalId));
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int animalId)
        {
            if (AnimalRepo.DeleteAnimal(animalId))
                return RedirectToAction("Index");
            return View(AnimalRepo.GetAnimal(animalId));
        }
    }
}