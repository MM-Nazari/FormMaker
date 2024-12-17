namespace FormMaker.Dto
{
    public class QuestionCreateDto
    {
        public string QuestionTitle { get; set; }
        public string QuestionType { get; set; }
        public string ValidationRule { get; set; }
        public bool IsFrequent { get; set; }
    }
}
