
using System.ComponentModel.DataAnnotations.Schema;

namespace DepartmentAPI.Domain
{
    [Table("t_departments")]
    public class Department
    {
        public Department()
        {
            
        }

        public Department(string name)
        {

            Id = Guid.NewGuid();
            Name = name;
        }

        public static Department Create(string name)
        {
            return new Department(name);
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
