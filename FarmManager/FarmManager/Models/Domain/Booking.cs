namespace FarmManager.Models.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Booking")]
    public partial class Booking : IValidatableObject
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Booking()
        {
            Accessoires = new HashSet<Accessoire>();
            Animals = new HashSet<Animal>();
        }

        public int Id { get; set; }

        public DateTime BookingDate { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Insertion { get; set; }

        public string Address { get; set; }

        public string Mail { get; set; }

        public string PhoneNumber { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Accessoire> Accessoires { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Animal> Animals { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if (FirstName ==null)
                errors.Add(new ValidationResult("Er moet een voornaam ingevuld worden", new[] { "FirstName" }));
            if (LastName == null)
                errors.Add(new ValidationResult("Er moet een achternaam ingevuld worden", new[] { "LastName" }));
            if (Address == null)
                errors.Add(new ValidationResult("Er moet een adres ingevuld worden", new[] { "Address" }));

            return errors;
        }
    }
}
