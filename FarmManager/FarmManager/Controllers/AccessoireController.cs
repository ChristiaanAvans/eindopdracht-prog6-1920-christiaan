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
    public class AccessoireController : Controller
    {
        public AccessoireRepository AccessoireRepo { get; set; }
        public AnimalRepository AnimalRepo { get; set; }

        public AccessoireController()
        {
            AccessoireRepo = new AccessoireRepository();
            AnimalRepo = new AnimalRepository();
        }

        public ActionResult Index()
        {
            return View(AccessoireRepo.GetAccessoires());
        }

        [HttpGet]
        public ActionResult Create()
        {
            var VM = new AccessoireVM() { Accessoire = new Accessoire(), Animals = AnimalRepo.GetAnimals() };

            return View(VM);
        }

        [HttpPost]
        public ActionResult Create(AccessoireVM VM)
        {
            VM.Accessoire.Animal = VM.AnimalId;
            if (AccessoireRepo.AddAccessoire(VM.Accessoire))
                return RedirectToAction("Index");

            VM.Animals = AnimalRepo.GetAnimals();
            return View(VM);
        }

        [HttpGet]
        public ActionResult Details(int accessoireId)
        {
            var VM = new AccessoireVM() { Accessoire = AccessoireRepo.GetAccessoire(accessoireId), Animals = AnimalRepo.GetAnimals() };

            return View(VM);
        }

        [HttpGet]
        public ActionResult Edit(int accessoireId)
        {
            var VM = new AccessoireVM() { Accessoire = AccessoireRepo.GetAccessoire(accessoireId), Animals = AnimalRepo.GetAnimals() };

            return View(VM);
        }

        [HttpPost]
        public ActionResult Edit(AccessoireVM VM)
        {
            VM.Accessoire.Animal = VM.AnimalId;
            if (AccessoireRepo.EditAccessoire(VM.Accessoire))
                return RedirectToAction("Index");

            VM.Animals = AnimalRepo.GetAnimals();
            return View(VM);
        }

        [HttpGet]
        public ActionResult Delete(int accessoireId)
        {
            var VM = new AccessoireVM() { Accessoire = AccessoireRepo.GetAccessoire(accessoireId), Animals = AnimalRepo.GetAnimals() };

            return View(VM);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int accessoireId)
        {
            if (AccessoireRepo.DeleteAccessoire(accessoireId))
                return RedirectToAction("Index");
            return View(AccessoireRepo.GetAccessoire(accessoireId));
        }
    }
}