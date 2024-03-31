using System.ComponentModel.DataAnnotations.Schema;

namespace QuestionService.Domain
{
    [Table("t_questions")]
    public class Question
    {
        public Question()
        {

        }

        public Question(Guid doctorId, Guid patientId, string title, string description, DateTime createDate)
        {
            Id = Guid.NewGuid();
            DoctorId = doctorId;
            PatientId = patientId;
            Title = title;
            Description = description;
            CreateDate = createDate;
        }

        public Guid Id { get; set; }
        public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; } 
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public Answer Answer { get; set; }
    }
}
