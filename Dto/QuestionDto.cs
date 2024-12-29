namespace FormMaker.Dto
{
    public class QuestionDto
    {
        public int QuestionID { get; set; }
        public string QuestionTitle { get; set; }
        public string QuestionType { get; set; }
        public string ValidationRule { get; set; }
        public bool IsFrequent { get; set; }

        public string CreatedAtJalali { get; set; }
        public string UpdatedAtJalali { get; set; }

        public bool IsDeleted { get; set; } = false;

        public IEnumerable<int> FormIDs { get; set; }
        public IEnumerable<int> ProcessIDs { get; set; }

    }
}
