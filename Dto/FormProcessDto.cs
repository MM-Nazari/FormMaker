namespace FormMaker.Dto
{
    public class FormProcessDTO
    {
        public int FormProcessID { get; set; }
        public int ProcessID { get; set; }
        public int FormID { get; set; }
        public int Stage { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
