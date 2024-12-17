namespace FormMaker.Dto
{
    public class FormQuestionCreateDto
    {
        public int FormID { get; set; }
        public int QuestionID { get; set; }
        public int QuestionOrder { get; set; }
        public bool IsRequired { get; set; }
    }
}
