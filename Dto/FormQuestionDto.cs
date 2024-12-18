namespace FormMaker.Dto
{
    public class FormQuestionDto
    {
        public int FormQuestionID { get; set; }
        public int FormID { get; set; }
        public int QuestionID { get; set; }
        public int QuestionOrder { get; set; }
        public bool IsRequired { get; set; }

        public string CreatedAtJalali { get; set; }
        public string UpdatedAtJalali { get; set; }

        public bool IsDeleted { get; set; } = false;

    }
}
