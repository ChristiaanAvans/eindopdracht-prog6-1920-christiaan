using FarmManager.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FarmManager.Models.ViewModels
{
    public class ContactInfoBookingVM : IValidatableObject
    {
        public Booking Booking { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Insertion { get; set; }

        public string Address { get; set; }

        public string Mail { get; set; }

        public string PhoneNumber { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            GetErrors(errors);

            return errors;
        }

        public void GetErrors(List<ValidationResult> errors)
        {
            if (FirstName == null)
                errors.Add(new ValidationResult("Er moet een voornaam ingevuld worden", new[] { "FirstName" }));
            if (LastName == null)
                errors.Add(new ValidationResult("Er moet een achternaam ingevuld worden", new[] { "LastName" }));
            if (Address == null)
                errors.Add(new ValidationResult("Er moet een adres ingevuld worden", new[] { "Address" }));
        }
    }
}