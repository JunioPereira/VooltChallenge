namespace VooltServices.Interfaces
{
    public interface IReadWriteFile
    {
        Task<bool> CreateAsync<T>(string key, T value);
        Task<Tuple<bool, T>> ReadAsync<T>(string key);
        Task<bool> UpdateAsync<T>(string key, T value);
        Task<bool> DeleteAsync<T>(string key);
    }
}
