namespace FormMaker.Dto
{
    public class CreateQuestionAndLinkToFormDto
    {
        public string QuestionTitle { get; set; }
        public string QuestionType { get; set; }
        public string ValidationRule { get; set; }
        public bool IsFrequent { get; set; }
        public int FormID { get; set; }
        public int ProcessID { get; set; }
        public int QuestionOrder { get; set; }
    }
}
