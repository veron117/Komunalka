using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BrigantinaDelo.Core
{
    public static class DateTimeExtension
    {
        public static string QuoteDayFormatDateTime(this DateTime date)
        {
            var ru = CultureInfo.GetCultureInfo("ru-RU");
            var year = date.ToString("yyyy г.");
            var month = ru.DateTimeFormat.MonthGenitiveNames[date.Month - 1];
            var day = @"""" + date.ToString("dd") + @"""";

            return day + " " + month + " " + year;
        }

        public static bool IsInRange(this DateTime dateToCheck, DateTime startDate, DateTime endDate)
        {
            return dateToCheck >= startDate && dateToCheck <= endDate.AddDays(1);
        }
        public static bool IsBirthDayInRange(this DateTime birthday, DateTime min, DateTime max)
        {
            var dates = new DateTime[] { birthday, min };
            for (int i = 0; i < dates.Length; i++)
            {
                if (dates[i].Month == 2 && dates[i].Day == 29)
                {
                    dates[i] = dates[i].AddDays(-1);
                }
            }

            birthday = dates[0];
            min = dates[1];

            DateTime nLower = new DateTime(min.Year, birthday.Month, birthday.Day);
            DateTime nUpper = new DateTime(max.Year, birthday.Month, birthday.Day);

            if (birthday.Year <= max.Year &&
                ((nLower >= min && nLower <= max) || (nUpper >= min && nUpper <= max)))
            {
                return true;
            }

            return false;
        }
    }
}
