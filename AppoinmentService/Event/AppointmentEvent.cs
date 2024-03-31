namespace AppoinmentService.Event
{
    public class AppointmentEvent
    {
        public Guid Id { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; }
        public string DoctorName { get; set; }
        public string DepartmentName { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

        private AppointmentEvent(Guid departmentId, Guid doctorId, Guid patientId, string doctorName, string departmentName, DateTime date, string description)
        {
            Id = Guid.NewGuid();
            DepartmentId = departmentId;
            DoctorId = doctorId;
            PatientId = patientId;
            DoctorName = doctorName;
            DepartmentName = departmentName;
            Date = date;
            Description = description;
        }

        public static AppointmentEvent Create(Guid departmentId, Guid doctorId, Guid patientId, string doctorName, string departmentName, DateTime date, string description)
        {
            return new AppointmentEvent(departmentId, doctorId, patientId, doctorName, departmentName, date, description);
        }
    }
}
