using Newtonsoft.Json;

namespace Chrono.FileSystem.DataProviders
{
    public class JsonFileDataProvider : FileDataProvider
    {
        protected override TEntity Deserialize<TEntity>(string stringValue)
        {
            var value = JsonConvert.DeserializeObject<TEntity>(stringValue);

            return value;
        }

        protected override string Serialize(object value)
        {
            var stringValue = JsonConvert.SerializeObject(value);

            return stringValue;
        }
    }
}
