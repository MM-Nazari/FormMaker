namespace FormMaker.Dto
{
    public class FormQuestionProcessDto
    {
        public int FormQuestionProcessID { get; set; }
        public int FormQuestionID { get; set; }
        public int FormProcessID { get; set; }

        public string CreatedAtJalali { get; set; }
        public string UpdatedAtJalali { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
