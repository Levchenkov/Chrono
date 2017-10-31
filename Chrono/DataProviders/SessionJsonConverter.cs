using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Chrono.DataProviders
{
    public class SessionJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Session);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jobject = JObject.Load(reader);
            var session = jobject.ToObject<Session>();

            var snapshots = session.Snapshots.Values;
            foreach (var snapshot in snapshots)
            {
                snapshot.Session = session;
            }

            return session;
        }

        public override bool CanWrite
        {
            get
            {
                return false;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}