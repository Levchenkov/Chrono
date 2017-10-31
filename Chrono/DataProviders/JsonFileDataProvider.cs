using Newtonsoft.Json;

namespace Chrono.DataProviders
{
    public class JsonFileDataProvider : FileDataProvider
    {
        protected override TEntity Deserialize<TEntity>(string stringValue)
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new SessionJsonConverter());

            var value = JsonConvert.DeserializeObject<TEntity>(stringValue, settings);

            return value;
        }

        protected override string Serialize(object value)
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented
            };
            var stringValue = JsonConvert.SerializeObject(value, settings);

            return stringValue;
        }
    }
}
