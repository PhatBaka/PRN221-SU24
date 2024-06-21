using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Helpers
{
    public class ImageHelper
    {
        public static List<byte[]> FormatImageFile(List<IFormFile> files)
        {
            List<byte[]> result = new List<byte[]>();
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    if (IsImageFile(file.FileName))
                    {
                        using var memoryStream = new MemoryStream();
                        file.CopyTo(memoryStream);
                        result.Add(memoryStream.ToArray());
                        string base64Image = Convert.ToBase64String(memoryStream.ToArray());
                        byte[] byteArray = Convert.FromBase64String(base64Image);
                        result.Add(byteArray);
                    }
                    else
                    {
                        throw new Exception("The file is not an image file");
                    }

                }
            }
            return result;
        }

        public static bool IsImageFile(string fileName)
        {
            string ext = Path.GetExtension(fileName).ToLower();
            return ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".gif" || ext == ".bmp";
        }

        public static async Task<byte[]> ConvertToByteArrayAsync(IFormFile formFile)
        {
            if (formFile == null || formFile.Length == 0)
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
