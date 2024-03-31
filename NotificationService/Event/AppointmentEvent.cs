namespace AppointmentAPI.Events
{
    public class AppointmentEvent
    {
        public Guid Id { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; }
        public string DoctorName { get; set; }
        public string DepartmentName { get; set; }
        public string PatientName { get; set; }
        public string PatientEmail { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

        private AppointmentEvent(Guid departmentId, Guid doctorId, Guid patientId, string doctorName, string departmentName, string patientName, DateTime date, string description)
        {
            Id = Guid.NewGuid();
            DepartmentId = departmentId;
            DoctorId = doctorId;
            PatientId = patientId;
            DoctorName = doctorName;
            DepartmentName = departmentName;
            PatientName = patientName;
            Date = date;
            Description = description;
        }

        public static AppointmentEvent Create(Guid departmentId, Guid doctorId, Guid patientId, string doctorName, string departmentName, string patientName, DateTime date, string description)
        {
            return new AppointmentEvent(departmentId, doctorId, patientId, doctorName, departmentName, patientName, date, description);
        }
    }
}
