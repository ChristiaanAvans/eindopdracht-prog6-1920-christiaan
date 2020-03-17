namespace FarmManager.Models.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Accessoire")]
    public partial class Accessoire : IValidatableObject
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Accessoire()
        {
            Bookings = new HashSet<Booking>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public int Animal { get; set; }

        public virtual Animal Animal1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Booking> Bookings { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if (Price < 0)
                errors.Add(new ValidationResult("De prijs kan niet negatief zijn", new[] { "Price" }));
            if (Animal == 0)
                errors.Add(new ValidationResult("Een accessoire moet bij een beestje horen", new[] { "Animal" }));
            if (Name == null)
                errors.Add(new ValidationResult("Er moet een naam ingevuld worden", new[] { "Name" }));

            return errors;
        }
    }
}
