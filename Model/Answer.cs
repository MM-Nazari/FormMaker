﻿namespace FormMaker.Model
{
    public class Answer
    {
        public int AnswerID { get; set; }
        public int FormQuestionProcessID { get; set; }
        public string AnswerText { get; set; }  
        public int? AnswerOptionID { get; set; }  
        public string FilePath { get; set; }  
        public bool? IsCaptchaSolved { get; set; }  
        public string CaptchaAnswer { get; set; }  

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

        // Relations
        public AnswerOption AnswerOption { get; set; }
        public FormQuestionProcess FormQuestionProcess { get; set; }
    }
}
