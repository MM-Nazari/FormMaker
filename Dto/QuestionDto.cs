namespace FormMaker.Dto
{
    public class QuestionDTO
    {
        public int QuestionID { get; set; }
        public string QuestionTitle { get; set; }
        public string QuestionType { get; set; }
        public string ValidationRule { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsFrequent { get; set; }
    }
}
