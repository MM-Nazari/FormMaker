namespace FormMaker.Model
{
    public class Form
    {
        public int FormID { get; set; }
        public string FormTitle { get; set; }
        public string FormDescription { get; set; }


        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsFrequent { get; set; }

        // Relations
        public ICollection<FormQuestion> FormQuestions { get; set; }
        public ICollection<FormProcess> FormProcesses { get; set; }
    }
}
