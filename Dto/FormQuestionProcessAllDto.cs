namespace FormMaker.Dto
{
    public class FormQuestionProcessAllDto
    {
        public int FormQuestionProcessID { get; set; }
        public int FormID { get; set; }
        public int ProcessID { get; set; }
        public int QuestionID { get; set; }
        public string QuestionTitle { get; set; }
        public string QuestionType { get; set; }
        public string CreatedAtJalali { get; set; }
        public string UpdatedAtJalali { get; set; }
        public bool IsDeleted { get; set; }
    }
}
