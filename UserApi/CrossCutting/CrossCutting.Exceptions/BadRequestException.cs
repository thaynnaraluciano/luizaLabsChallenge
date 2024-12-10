namespace CrossCutting.Exceptions
{
    public class BadRequestException : Exception
    {
        public int StatusCode { get; } = 400;

        public BadRequestException(string message)
            : base(message)
        {
        }

        public BadRequestException(string message, int statusCode)
            : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
