using System.Threading.Tasks;

namespace MarvelPortable.Extensions
{
    internal static class ObjectExtensions
    {
        internal async static Task<TReturnType> ToCollection<TReturnType>(this object item)
        {
            var json = await item.SerialiseAsync();

            return await json.DeserialiseAsync<TReturnType>();
        }
    }
}
