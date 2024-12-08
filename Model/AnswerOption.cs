namespace FormMaker.Model
{
    public class AnswerOption
    {
        public int OptionID { get; set; }
        public int QuestionID { get; set; } 
        public string OptionText { get; set; }
        public int Priority { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

        // Relations
        public Question Question { get; set; }
        public ICollection<Answer> Answers { get; set; }
    }
}
