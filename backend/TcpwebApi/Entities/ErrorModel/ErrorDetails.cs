namespace TcpWebApi.Entities
{
    public class ErrorDetails
    {
        public bool Success { get; set; } = false;
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return $"{{ \"success\": {Success}, \"statusCode\": {StatusCode}, \"message\": \"{Message}\" }}";
        }
    }
}
