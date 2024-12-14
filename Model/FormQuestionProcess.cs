namespace FormMaker.Model
{
    public class FormQuestionProcess
    {
        public int FormQuestionProcessID { get; set; }
        public int FormQuestionID { get; set; }
        public int FormProcessID { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

        // Relations
        public FormQuestion FormQuestion { get; set; }
        public FormProcess FormProcess { get; set; }
        public ICollection<Answer> Answers { get; set; }

    }
}
