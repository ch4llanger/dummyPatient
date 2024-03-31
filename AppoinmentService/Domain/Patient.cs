
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentAPI.Domain
{
    [Table("t_patients")]
    public class Patient
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public void Update(string name, string surname)
        {
       
            Name = name;
            Surname = surname;
        }
    }
}
