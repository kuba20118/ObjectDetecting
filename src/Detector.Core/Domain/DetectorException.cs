using System;

namespace Detector.Core.Domain
{
    public abstract class DetectorException : Exception
    {
        public string Code { get; }

        protected DetectorException()
        {
        }

        public DetectorException(string code)
        {
            Code = code;
        }

        public DetectorException(string message, params object[] args)
            : this(string.Empty, message, args)
        {
        }

        public DetectorException(string code, string message, params object[] args)
            : this(null, string.Empty, message, args)
        {
        }

        public DetectorException(Exception innerException, string message, params object[] args)
            : this(innerException, string.Empty, message, args)
        {
        }

        public DetectorException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            Code = code;
        }
    }
}