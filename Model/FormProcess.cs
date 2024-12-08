namespace FormMaker.Model
{
    public class FormProcess
    {
        public int FormProcessID { get; set; }
        public int ProcessID { get; set; }  
        public int FormID { get; set; }  
        public int Stage { get; set; } 


        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }


        // Relations
        public Process Process { get; set; }
        public Form Form { get; set; }
        public ICollection<Answer> Answers { get; set; }
    }
}
