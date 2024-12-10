namespace FormMaker.Model
{
    public class FormQuestion
    {
        public int FormQuestionID { get; set; }
        public int FormID { get; set; }  
        public int QuestionID { get; set; }  
        public int QuestionOrder { get; set; }
        public bool IsRequired { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

        // Relations
        public Form Form { get; set; }
        public Question Question { get; set; }
        public ICollection<FormQuestionProcess> FormQuestionProcesses { get; set; }
        //public ICollection<Answer> Answers { get; set; }
    }
}
