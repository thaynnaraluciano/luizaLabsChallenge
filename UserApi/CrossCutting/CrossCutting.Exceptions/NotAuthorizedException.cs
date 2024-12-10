using System.Net;

namespace CrossCutting.Exceptions
{
    public class NotAuthorizedException : Exception
    {
        public int StatusCode { get; } = 401;

        public NotAuthorizedException(string message)
            : base(message)
        {
        }

        public NotAuthorizedException(string message, int statusCode)
            : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
