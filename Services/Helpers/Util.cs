using Microsoft.AspNetCore.Http;
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

        public static string CapitalizeFirstLetterOfSentence(string sentence)
        {
            if (string.IsNullOrEmpty(sentence))
            {
                return sentence;
            }

            return char.ToUpper(sentence[0]) + sentence.Substring(1);
        }

        public static string ReadJsonFile(string fileName)
        {
            string jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);

            if (!File.Exists(jsonFilePath))
            {
                throw new FileNotFoundException($"The file '{fileName}' was not found in the current directory.");
            }

            string jsonString = File.ReadAllText(jsonFilePath);
            return jsonString;
        }

        public static async Task<byte[]> ToByteArrayAsync(IFormFile formFile)
        {
            if (formFile == null)
            {
                return null;
            }

            using (var memoryStream = new MemoryStream())
            {
                await formFile.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
