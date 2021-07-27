using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroveAirlines.ControllerLayer.JSONData
{
    public class BookingData : IValidatableObject
    {
        private string _firstName;  // backing field
        public string FirstName
        {
            get => _firstName;  // return
            set => _firstName = ValidateName(value, nameof(FirstName)); // set value
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set => _lastName = ValidateName(value, nameof(LastName));
        }


        private string ValidateName(string name, string propertyName) =>
            string.IsNullOrEmpty(name)
                ? throw new InvalidOperationException("could not set " + propertyName)
                : name;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> results = new List<ValidationResult>();
            if (FirstName == null && LastName == null)
            {
                results.Add(new ValidationResult("All given data points are null"));
            }
            else if (FirstName == null || LastName == null)
            {
                results.Add(new ValidationResult("One of the given data points is null"));
            }

            return results;
        }
    }
}
