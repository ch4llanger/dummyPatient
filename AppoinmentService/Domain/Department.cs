using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentAPI.Domain
{
    [Table("t_departments")]
    public class Department
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public void Update(string name)
        {
            Name = name;

    }
    }


}
