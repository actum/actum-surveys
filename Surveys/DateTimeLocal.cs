using System;
using System.Runtime.InteropServices;
using TimeZoneConverter;

namespace Surveys
{
    public static class DateTimeLocal
    {
        const string LocalTimeZoneId = "Central Europe Standard Time";

        static readonly TimeZoneInfo LocalTimeZoneInfo;

        static DateTimeLocal()
        {
            LocalTimeZoneInfo = GetTimeZoneInfo(LocalTimeZoneId);
        }

        public static DateTime Now
        {
            get
            {
                var utcNow = DateTimeOffset.UtcNow;
                var result = utcNow.ToOffset(LocalTimeZoneInfo.GetUtcOffset(utcNow));
                return result.DateTime;
            }
        }

        /// <summary>
        /// Supports multi-platform time zone resolving. Described in https://github.com/dotnet/corefx/issues/11897.
        /// </summary>
        /// <param name="windowsOrIanaTimeZoneId"></param>
        /// <returns></returns>
        public static TimeZoneInfo GetTimeZoneInfo(string windowsOrIanaTimeZoneId)
        {
            try
            {
                // Try a direct approach first
                return TimeZoneInfo.FindSystemTimeZoneById(windowsOrIanaTimeZoneId);
            }
            catch
            {
                // We have to convert to the opposite platform
                var tzid = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                    ? TZConvert.IanaToWindows(windowsOrIanaTimeZoneId)
                    : TZConvert.WindowsToIana(windowsOrIanaTimeZoneId);

                // Try with the converted ID
                return TimeZoneInfo.FindSystemTimeZoneById(tzid);
            }
        }
    }
}
