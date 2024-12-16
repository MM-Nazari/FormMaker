using System.Globalization;

namespace FormMaker.Util
{
    public class Jalali
    {
        public static string ToJalali(DateTime dateTime)
        {
            TimeZoneInfo iranTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Iran Standard Time");
            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(dateTime, iranTimeZone);
            PersianCalendar pc = new PersianCalendar();
            string jalaliDate = $"{pc.GetYear(localTime)}/{pc.GetMonth(localTime):00}/{pc.GetDayOfMonth(localTime):00}";
            string time = $"{localTime.Hour:00}:{localTime.Minute:00}:{localTime.Second:00}";
            return $"{jalaliDate} {time}";
        }
    }
}
