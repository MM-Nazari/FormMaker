namespace FormMaker.Model
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public int StatusCode { get; set; }

        public ApiResponse(bool success, string message, T data, int statusCode)
        {
            Success = success;
            StatusCode = statusCode;
            Message = message;
            Data = data;
            
        }
    }
}
