using FarmManager.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FarmManager.Models.Repositories
{
    public interface IAccessoireRepository
    {
        FarmContext Context { get; set; }

        List<Accessoire> GetAccessoires();

        Accessoire GetAccessoire(int accessoireId);

        bool AddAccessoire(Accessoire accessoire);

        bool EditAccessoire(Accessoire accessoire);

        bool DeleteAccessoire(int accessoireId);
    }
}