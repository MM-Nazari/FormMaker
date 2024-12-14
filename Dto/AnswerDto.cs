namespace FormMaker.Dto
{
    public class AnswerDto
    {
        public int AnswerID { get; set; }
        public int FormQuestionProcessID { get; set; }
        public string AnswerText { get; set; }
        public int? AnswerOptionID { get; set; }
        public string FilePath { get; set; }
        public bool? IsCaptchaSolved { get; set; }
        public string CaptchaAnswer { get; set; }
    }
}
