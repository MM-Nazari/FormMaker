namespace FormMaker.Dto
{
    public class AnswerCreateDto
    {
        public int FormID { get; set; }
        public int QuestionID { get; set; }
        public int ProcessID { get; set; }
        public string AnswerText { get; set; }
        public int? AnswerOptionID { get; set; }
        public string FilePath { get; set; }
        public bool? IsCaptchaSolved { get; set; }
        public string CaptchaAnswer { get; set; }
    }
}
