namespace FormMaker.Dto
{
    public class FormUpdateDto
    {
        public int FormID { get; set; }
        public string FormTitle { get; set; }
        public string FormDescription { get; set; } = string.Empty;
        public bool IsFrequent { get; set; }
    }
}
