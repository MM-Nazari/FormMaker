namespace FormMaker.Dto
{
    public class FormCreateDto
    {
        public string FormTitle { get; set; }
        public string FormDescription { get; set; } = string.Empty;
        public bool IsFrequent { get; set; }
    }
}
