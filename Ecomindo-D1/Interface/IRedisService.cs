using System.Threading.Tasks;

namespace Ecomindo_D1.Interface
{
    public interface IRedisService
    {
        Task SaveAsync(string key, object value);
        Task<T> GetAsync<T>(string key);
        Task<bool> DeleteAsync(string key);

    }
}
