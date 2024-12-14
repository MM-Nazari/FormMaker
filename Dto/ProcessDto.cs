namespace FormMaker.Dto
{
    public class ProcessDto
    {
        public int ProcessID { get; set; }
        public string ProcessTitle { get; set; }
        public string ProcessDescription { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
