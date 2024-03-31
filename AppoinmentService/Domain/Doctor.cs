
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentAPI.Domain
{
    [Table("t_doctors")]
    public class Doctor
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FullName
        {
            get { return $"{this.Name} {this.Surname}"; }
        }


        public void Update(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }
    }
}
