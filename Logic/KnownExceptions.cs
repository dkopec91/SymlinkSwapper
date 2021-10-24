using SymlinkSwapper.Resources;
using System;

namespace SymlinkSwapper.Logic
{
    public class KnownException : ApplicationException
    {
        private enum ContentType { Header, Message }

        public string ErrorHeader => GetContent(ContentType.Header);

        public string ErrorMessage => GetContent(ContentType.Message);

        private string GetContent(ContentType contentType)
        {
            string typeName = GetType().Name;
            string contentTypeStr = contentType.ToString();

            string result = GetResourceContent($"{typeName}_{contentTypeStr}");

            if (string.IsNullOrWhiteSpace(result))
            {
                result = GetResourceContent($"Default_{contentTypeStr}");
            }

            return result;
        }

        private static string GetResourceContent(string messageKey)
        {
            return ErrorMessages.ResourceManager.GetString(messageKey);
        }
    }

    public class PriviligeNotHeldException : KnownException { }
    public class FailedToCreateSymlinkException : KnownException { }
    public class NoFilesException : KnownException { }
}
