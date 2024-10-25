using Microsoft.Extensions.Localization;
using Newtonsoft.Json;

namespace API.Localization
{
    public class JsonStringLocalizer : IStringLocalizer
    {
        private readonly JsonSerializer _jsonSerializer;

        public JsonStringLocalizer()
        {
            _jsonSerializer = new();
        }
      
        public LocalizedString this[string name] => new LocalizedString(name , GetValueFromJson(name));

        public LocalizedString this[string name, params object[] arguments] => new LocalizedString( name, string.Format(this[name].Value , arguments));

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
           var filePath = $"JSONResouces/{Thread.CurrentThread.CurrentCulture.Name}.json";
           var fullFilePath = Path.GetFullPath(filePath);

            if (!File.Exists(fullFilePath)){
                yield break;
            }

            using FileStream stream = new(fullFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using StreamReader streamReader = new(stream);
            using JsonTextReader reader = new(streamReader);

            while (reader.Read())
            {
                if (reader.TokenType != JsonToken.PropertyName)
                    continue;

                var key = reader.Value as string;
                reader.Read();
                var value = _jsonSerializer.Deserialize<string>(reader);
                yield return new LocalizedString(key, value);
            }
            
        }

        // helper methods 
        public string GetValueFromJson(string propName){

            if (string.IsNullOrEmpty(propName))
                return string.Empty;
                
            var filePath = $"Localization/{Thread.CurrentThread.CurrentCulture.Name}.json";
            var fullFilePath = Path.GetFullPath(filePath);

            if (File.Exists(fullFilePath))
             {

                using var fileStram = new FileStream(filePath, FileMode.Open,FileAccess.Read, FileShare.Read);
                using var StreamReader = new StreamReader(fileStram);
                using var jsonReader = new JsonTextReader(StreamReader);

                while (jsonReader.Read()){
                    if (jsonReader.TokenType == JsonToken.PropertyName 
                            && string.Equals(jsonReader.Value as string , propName , StringComparison.OrdinalIgnoreCase)){
                            jsonReader.Read();
                            return _jsonSerializer.Deserialize<string>(jsonReader);
                    }
                }
             }
           return string.Empty;
        }
    }
}