namespace CrossCutting.Exceptions
{
    public class NotFoundException : Exception
    {
        public int StatusCode { get; } = 404;

        public NotFoundException(string message)
            : base(message)
        {
        }

        public NotFoundException(string message, int statusCode)
            : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
