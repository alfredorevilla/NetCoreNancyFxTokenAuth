using System;

namespace NancyWebApp.Common
{
    public class DateTimeHelper
    {
        private static DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Following https://tools.ietf.org/html/rfc7519#section-4.1.4
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static DateTime GetFromNumericDate(long number)
        {
            if (number < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(number));
            }

            return unixEpoch.AddSeconds(number);
        }
    }
}