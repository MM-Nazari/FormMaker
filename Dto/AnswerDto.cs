namespace FormMaker.Dto
{
    public class AnswerDto
    {
        public int AnswerID { get; set; }
        public int FormID { get; set; }
        public int ProcessID { get; set; }
        public int QuestionID { get; set; }
        public string AnswerText { get; set; }
        public int? AnswerOptionID { get; set; }
        public string FilePath { get; set; }
        public bool? IsCaptchaSolved { get; set; }
        public string CaptchaAnswer { get; set; }

        public string CreatedAtJalali { get; set; }
        public string UpdatedAtJalali { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
