namespace FormMaker.Dto
{
    public class QuestionUpdateDto
    {
        public int QuestionID { get; set; }
        public string QuestionTitle { get; set; }
        public string QuestionType { get; set; }
        public string ValidationRule { get; set; }
        public bool IsFrequent { get; set; }
    }
}
