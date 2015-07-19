using System;
using System.Runtime.Serialization;

namespace Geometry.Text
{
    public class GeometryException : ApplicationException
    {
        /// <summary>
        /// Error message Code
        /// </summary>
        /// The passed-in Code is used to retrieve the corresponding error message text from the
        /// text storage</remarks>
        private string messageCode;


        /// <summary>
        /// Public constructor
        /// </summary>
        /// <param name="messageCode">Message Code</param>
        /// <remarks>Text for error message is stored in Messages table of the Metadata database.
        /// The passed-in Code is used to retrieve the corresponding error message text from the
        /// metadata</remarks>
        public GeometryException(string messageCode, string lang)
            : base (TextFactory.Instance.GetText(messageCode, lang))
        {
            Initialize(messageCode);
        }

        /// <summary>
        /// Public constructor
        /// </summary>
        /// <param name="messageCode">Message Code</param>
        /// <param name="parameters">An array of parameter strings to be inserted in the error
        /// message text</param>
        public GeometryException(string messageCode, string lang, params object[] parameters)
            : base(string.Format(TextFactory.Instance.GetText(messageCode, lang), parameters))
        {
            Initialize(messageCode);
        }

        /// <summary>
        /// Public constructor
        /// </summary>
        /// <param name="messageCode">Message Code</param>
        /// <param name="parameters">An array of parameter strings to be inserted in the error
        /// <param name="cause">The original exception, which will become the
        /// inner exception of the current object</param>
        /// message text</param>
        public GeometryException(string messageCode, string lang, Exception cause, params object[] parameters)
            : base(string.Format(TextFactory.Instance.GetText(messageCode, lang), parameters), cause)
        {
            Initialize(messageCode);
        }


        /// <summary>
        /// Public constructor
        /// </summary>
        /// <param name="messageCode">Message Code</param>
        /// <param name="cause">The original exception, which will become the
        /// inner exception of the current object</param>
        /// <remarks>Text for error message is stored in Messages table of the Metadata database.
        /// The passed-in Code is used to retrieve the corresponding error message text from the
        /// metadata</remarks>
        public GeometryException(string messageCode, string lang, Exception cause)
            : base(TextFactory.Instance.GetText(messageCode, lang), cause)
        {
            Initialize(messageCode);
        }

        

        /// <summary>
        /// Protected constructor used by the .NET framework for de-serialization
        /// of exceptions
        /// </summary>
        /// <param name="info">Fields and properties of the object to de-serialize</param>
        /// <param name="context">De-serialization context</param>
        protected GeometryException(SerializationInfo info, StreamingContext context)
            : base (info, context)
        {

        }


        /// <summary>
        /// Assignes the passed-in message Code to the internal member variable
        /// </summary>
        /// <param name="Code">Code of the message</param>
        protected void Initialize(string code)
        {
            messageCode = code;
        }

        /// <summary>
        /// A convenience method for obtaining the detailed error description
        /// </summary>
        /// <param name="ex">The exception object for which the detailed error description has 
        /// been requested</param>
        /// <returns>Detailed error description</returns>
        /// <remarks>This function uses recursion to iterated through all inner exceptions
        /// inside the passed-in exception and concatenates the error messages</remarks>
        public static string ExceptionSummary(Exception ex)
        {
            string message = string.Empty;
            if (ex.InnerException != null)
            {
                string inner = ex.InnerException.Message;
                message = string.Format("{0};{1}", inner, ExceptionSummary(ex.InnerException));
            }
            return message;
        }

        /// <summary>
        /// A convenience method for obtaining the detailed error description
        /// </summary>
        /// <param name="ex">The exception object for which the detailed error description has 
        /// been requested</param>
        /// <returns>Detailed error description</returns>
        /// <remarks>This function uses recursion to iterated through all inner exceptions
        /// inside the passed-in exception, starting from the last one and concatenates the error messages</remarks>
        public static string ExceptionSummaryBackwards(Exception ex)
        {
            string message = string.Empty;
            if (ex.InnerException != null)
            {
                string inner = string.Format("{0}. ", ex.InnerException.Message);
                message = string.Format("{0}{1}", ExceptionSummary(ex.InnerException), inner);
            }
            return message;
        }

        /// <summary>
        /// A convenience method for obtaining the original (root) error
        /// </summary>
        /// <param name="ex">The exception object that was caught for display</param>
        /// <returns>Root error</returns>
        /// <remarks>This function uses recursion to iterated through all inner exceptions
        /// and retrieves the message of the last inner exception in the stack</remarks>
        public static string GetRootError(Exception ex)
        {
            if (ex == null)
            {
                return string.Empty;
            }
            string message = string.Empty;
            Exception inner = ex.InnerException;
            while (inner != null)
            {
                message = inner.Message;
                inner = inner.InnerException;
            }
            return message;
        }

        /// <summary>
        /// Formats a string that contains the error message and the root message of the 
        /// passed-in exception
        /// </summary>
        /// <param name="ex">Exception object for which the full message message is requested</param>
        /// <returns>Complete message string</returns>
        public static string GetFullMessage(Exception ex)
        {
            if (ex == null)
            {
                return string.Empty;
            }
            string root = GetRootError(ex);
            if (root != string.Empty && root != ex.Message)
            {
                return string.Format("{0}. {1}", ex.Message, root);
            }
            else
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// A convenience method for obtaining the detailed error description
        /// </summary>
        /// <param name="ex">The exception object for which the detailed error description has 
        /// been requested</param>
        /// <returns>Detailed error description</returns>
        /// <remarks>This function uses recursion to iterated through all inner exceptions
        /// inside the passed-in exception and concatenates the error messages</remarks>
        public virtual string GetExceptionSummary(Exception ex)
        {
            Exception current = ex;
            if (ex == null)
            {
                current = this;
            }
            string message = current.Message;
            if (current.InnerException != null)
            {
                Exception innerEx = current.InnerException;
                if (innerEx is GeometryException)
                {
                    message = string.Format("{0};{1}",message,(innerEx as GeometryException).GetExceptionSummary(null));
                }
                else
                {
                    message = string.Format("{0};{1}",message,GetExceptionSummary(innerEx));
                }
            }
            return message;
        }

        /// <summary>
        /// Gets the message id of the exception.
        /// </summary>
        public string MessageCode
        {
            get
            {
                return this.messageCode;
            }
        }
    }
}
