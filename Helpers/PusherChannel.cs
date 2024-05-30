using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PusherServer;

namespace Gaz_BackEnd.Helpers
{
    public class PusherChannel
    {
        public static async Task Trigger(object data, string channelName, string eventName)
        {
            // set json serializer to camelCase
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var json = JsonConvert.SerializeObject(data, settings);
            data = JsonConvert.DeserializeObject(json);
            
            var options = new PusherOptions
            {
                Cluster = "us2",
                Encrypted = true,
                
            };

            var pusher = new Pusher(
            "1707554",
            "8a87753b253bd2f11a5b",
            "03c4ad74f0cfa79a0120",
            options);

            var result = await pusher.TriggerAsync(
            channelName,
            eventName,
            data);
            
        }
    }
}