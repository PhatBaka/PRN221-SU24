using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Helpers
{
    public class Util
    {
        public static List<DateTime> GetDatesInRangeByDayOfWeek(DateTime startDate, DateTime endDate, DayOfWeek targetDayOfWeek)
        {
            List<DateTime> dateList = new List<DateTime>();

            for (DateTime currentDate = startDate; currentDate <= endDate; currentDate = currentDate.AddDays(1))
            {
                if (currentDate.DayOfWeek == targetDayOfWeek)
                {
                    dateList.Add(currentDate);
                }
            }
            return dateList;
        }
    }
}
