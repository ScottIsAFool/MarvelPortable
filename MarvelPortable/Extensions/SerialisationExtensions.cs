using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MarvelPortable.Extensions
{
    internal static class SerialisationExtensions
    {
        internal static Task<TReturnType> DeserialiseAsync<TReturnType>(this string json)
        {
            return Task.Factory.StartNew(() => JsonConvert.DeserializeObject<TReturnType>(json));
        }

        internal static Task<string> SerialiseAsync(this object item)
        {
            return Task.Factory.StartNew(() => JsonConvert.SerializeObject(item));
        }

        internal static Task<string> SerialiseAsync(this object item, JsonConverter converter)
        {
            return Task.Factory.StartNew(() => JsonConvert.SerializeObject(item, converter));
        }
    }
}
