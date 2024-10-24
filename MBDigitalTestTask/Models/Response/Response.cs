namespace MBDigitalTestTask.Models.Response
{
    public class Response<T> : Response
    {
        public ICollection<T> Data { get; set; }
    }

    public class Response
    {
        public int Status { get; set; }
        public ICollection<string> Errors { get; set; }
        public Response()
        {
            Status = StatusCodes.Status200OK;
            Errors = new List<string>();
        }

        public void SetErrors(params string[] errors)
        {
            Errors = errors;
        }

        public bool IsSuccess { get => Status == StatusCodes.Status200OK; }
    }

}
