namespace FormMaker.Dto
{
    public class ProcessDto
    {
        public int ProcessID { get; set; }
        public string ProcessTitle { get; set; }
        public string ProcessDescription { get; set; }

        public string CreatedAtJalali { get; set; }
        public string UpdatedAtJalali { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
