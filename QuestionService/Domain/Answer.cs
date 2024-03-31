using System.ComponentModel.DataAnnotations.Schema;

namespace QuestionService.Domain
{
    [Table("t_answers")]
    public class Answer
    {
        public Guid Id { get; set; }
        public Guid QuestionId { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public Question Question { get; set; }
    }
}
