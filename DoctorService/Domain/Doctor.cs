using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DepartmentAPI.Domain
{
    [Table("t_doctors")]
    public class Doctor
    {
        public Doctor()
        {
               
        }

        public Doctor(Guid departmentId, string name, string surname)
        {

            Id = Guid.NewGuid();
            DepartmentId = departmentId;
            Name = name;
            Surname = surname;
        }

        public static Doctor Create(Guid departmentId, string name, string surname)
        {
            return new Doctor(departmentId, name, surname);
        }

        public Guid Id { get; set; }
        public Guid DepartmentId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FullName
        {
            get { return $"{this.Name} {this.Surname}"; }
        }

        [JsonIgnore]
        public Department Department { get; set; }


    }
}
