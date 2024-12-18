using System.Text.Json.Serialization;

namespace FormMaker.Dto
{
    public class FormWithProcessCreateDto
    {
        public string FormTitle { get; set; }
        public string FormDescription { get; set; }
        public bool IsFrequent { get; set; }
        public int ProcessID { get; set; }
        public int Stage { get; set; }
    }
}
