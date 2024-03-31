using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Xml.Linq;

namespace PatientService.Domain
{
    [Table("t_patients")]
    public class Patient
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public Patient( string name, string surname)
        {
            Id = Guid.NewGuid();         
            Name = name;
            Surname = surname;

        }


        public static Patient Create( string name, string surname)
        {
            return new Patient( name, surname);
        }

    }
}
