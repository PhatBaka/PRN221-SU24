using Newtonsoft.Json;

namespace UI.Helper
{
    public static class Utils
    {
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

        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
