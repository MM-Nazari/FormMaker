namespace FormMaker.Dto
{
    public class AnswerOptionCreateDto
    {
        public int QuestionID { get; set; }
        public string OptionText { get; set; }
        public int Priority { get; set; }
    }
}
