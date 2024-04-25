namespace MCSHR.Models
{
    public class ResponseModel
    {
        public bool IsResponse { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = "";
    }
}
