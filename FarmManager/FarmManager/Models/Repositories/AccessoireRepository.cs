using FarmManager.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FarmManager.Models.Repositories
{
    public class AccessoireRepository : IAccessoireRepository
    {
        public FarmContext Context { get; set; }

        public AccessoireRepository()
        {
            Context = new FarmContext();
        }

        public List<Accessoire> GetAccessoires()
        {
            return Context.Accessoires.ToList();
        }

        public Accessoire GetAccessoire(int accessoireId)
        {
            return Context.Accessoires.Find(accessoireId);
        }

        public bool AddAccessoire(Accessoire accessoire)
        {
            try
            {
                Context.Accessoires.Add(accessoire);
                Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool EditAccessoire(Accessoire accessoire)
        {
            try
            {
                var toEdit = Context.Accessoires.Find(accessoire.Id);
                toEdit.Name = accessoire.Name;
                toEdit.Price = accessoire.Price;
                toEdit.ImageUrl = accessoire.ImageUrl;
                toEdit.Animal = accessoire.Animal;

                Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteAccessoire(int accessoireId)
        {
            try
            {
                var toRemove = Context.Accessoires.Find(accessoireId);
                Context.Accessoires.Remove(toRemove);

                Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}