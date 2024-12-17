namespace FormMaker.Dto
{
    public class FormDto
    {
        public int FormID { get; set; }
        public string FormTitle { get; set; }
        public string FormDescription { get; set; }
        public bool IsFrequent { get; set; }

        public string CreatedAtJalali { get; set; }
        public string UpdatedAtJalali { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
