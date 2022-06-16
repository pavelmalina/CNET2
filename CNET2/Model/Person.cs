using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    [Index(nameof(Email))]
    public class Person
    {
        #region konstruktory
        public Person(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public Person()
        {

        }
        #endregion

        #region vlastnosti

        public int Id { get; set; }

        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [MaxLength(120)]
        public string LastName { get; set; } = string.Empty;

        public string FullName
        {
            get => GetFullName();
        }

        public DateTime DateOfBirth { get; set; }

        public Address HomeAddress { get; set; } = new Address();

        [MaxLength(256)]
        [Required]
        public string Email { get; set; }

        [MaxLength(100)]
        public string Phone { get; set; }

        public List<Contract> Contracts { get; set; } = new List<Contract>();

        #endregion

        #region metody

        // ToString se používá i v debuggeru, tj. zobrazuje po najetí na objekt
        public override string ToString() => $"{FirstName} {LastName}";

        public int Age()
        {
            DateTime today = DateTime.Today;

            int age = today.Year - DateOfBirth.Year;

            if (today.Month < DateOfBirth.Month ||
           ((today.Month == DateOfBirth.Month) && (today.Day < DateOfBirth.Day)))
            {
                age--;  //birthday in current year not yet reached, we are 1 year younger ;)
            }

            return age;
        }

        public string GetFullName() => $"{FirstName} {LastName}";

        #endregion
    }
}