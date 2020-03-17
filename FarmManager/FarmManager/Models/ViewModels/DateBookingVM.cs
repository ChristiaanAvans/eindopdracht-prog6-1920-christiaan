using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FarmManager.Models.ViewModels
{
    public class DateBookingVM : IValidatableObject
    {
        public DateTime BookingDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (BookingDate <= DateTime.Today)
                results.Add(new ValidationResult("De boekingsdatum moet in de toekomst liggen", new[] { "BookingDate" }));

            return results;
        }
    }
}