using System;
using System.Threading.Tasks;

namespace NancyWebApp
{
    internal static class Extensions
    {
        public static T RunAsSynchronous<T>(this Task<T> task)
        {
            try
            {
                var value = task.Result;
                return value;
            }
            catch (AggregateException e)
            {
                var innerException = e.InnerExceptions[0];
                if (innerException is AggregateException)
                    innerException = ((AggregateException)innerException).InnerExceptions[0];
                throw innerException;
            }
            catch
            {
                throw;
            }
        }
    }
}