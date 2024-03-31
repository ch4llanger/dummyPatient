using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PatientService.Domain
{
    [Table("t_patient_appointments")]
    public class PatientAppointment
    {
        public Guid Id { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; }

    
    }
}
