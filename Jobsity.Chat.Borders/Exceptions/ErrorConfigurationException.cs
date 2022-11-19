namespace Jobsity.Chat.Borders.Exceptions
{
    using System.Runtime.Serialization;

    public class ErrorConfigurationException : Exception
    {
        protected ErrorConfigurationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public ErrorConfigurationException(string errorMessage) : base(errorMessage)
        {
        }
    }
}
