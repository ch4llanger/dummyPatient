using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AppointmentAPI.Domain
{
    [Table("t_appointments")]
    public class Appointment
    {
        public Guid Id { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

        public Appointment(Guid departmentId, Guid doctorId, Guid patientId, DateTime date)
        {
            Id = Guid.NewGuid();
            DepartmentId = departmentId;
            DoctorId = doctorId;
            PatientId = patientId;
            Date = date;
        }

        public static Appointment Create(Guid departmentId, Guid doctorId, Guid patientId, DateTime date)
        {
            return new Appointment(departmentId, doctorId, patientId, date);
        }

        [JsonIgnore]
        public Department Department { get; set; }
        [JsonIgnore]
        public Doctor Doctor { get; set; }
        [JsonIgnore]
        public Patient Patient { get; set; }
    }
}
