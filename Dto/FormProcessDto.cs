namespace FormMaker.Dto
{
    public class FormProcessDto
    {
        public int FormProcessID { get; set; }
        public int ProcessID { get; set; }
        public int FormID { get; set; }
        public int Stage { get; set; }

        public string CreatedAtJalali { get; set; }
        public string UpdatedAtJalali { get; set; }

        public bool IsDeleted { get; set; }
    }
}
