namespace FormMaker.Model
{
    public class Process
    {
        public int ProcessID { get; set; }
        public string ProcessTitle { get; set; }
        public string ProcessDescription { get; set; }


        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

        // Relations
        public ICollection<FormProcess> FormProcesses { get; set; }
    }
}
