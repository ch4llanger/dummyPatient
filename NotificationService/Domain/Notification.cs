using System.ComponentModel.DataAnnotations.Schema;

namespace NotificationAPI.Domain
{
    [Table("t_notifications")]
    public class Notification
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public string Message { get; set; }
        public DateTime CreateDate { get; set; }

        public Notification(Guid patientId, string message)
        {
            Id = Guid.NewGuid();
            PatientId = patientId;
            Message = message;
            CreateDate = DateTime.UtcNow;
        }

        public Notification()
        {

        }

        public static Notification Create(Guid patientId, string message)
        {
            return new Notification(patientId, message);
        }
    }
}
