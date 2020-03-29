using System.Net;

namespace UpskillStore.Utils.Extensions
{
    public static class HttpCodeExtensions
    {
        public static bool IsHttpStatusCodeSucessful(this int httpStatusCode)
        {
            var isSuccessfull = httpStatusCode >= (int)HttpStatusCode.OK && httpStatusCode < (int)HttpStatusCode.Ambiguous;

            return isSuccessfull;
        }
    }
}
