using Newtonsoft.Json;
using System.Xml.Serialization;
using VooltServices.Interfaces;

namespace VooltServices.Implementations
{
    public class ReadWriteFile : IReadWriteFile
    {
        public Task<bool> CreateAsync<T>(string key, T value)
        {
            if (File.Exists($"{Environment.CurrentDirectory}/Files/{key}.json")) 
            {
                return Task.FromResult(false);
            }

            TextWriter writer = null;
            try
            {
                var contentsToWriteToFile = JsonConvert.SerializeObject(value);
                writer = new StreamWriter($"{Environment.CurrentDirectory}/Files/{key}.json", true);
                writer.Write(contentsToWriteToFile);

                return Task.FromResult(true);
            }
            catch 
            {
                return Task.FromResult(false);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }

        }

        public Task<Tuple<bool, T>> ReadAsync<T>(string key)
        {
            if (!File.Exists($"{Environment.CurrentDirectory}/Files/{key}.json"))
            {
                return Task.FromResult<Tuple<bool, T>>(new Tuple<bool, T?>(false, JsonConvert.DeserializeObject<T>(string.Empty)));
            }

            TextReader reader = null;
            try
            {
                reader = new StreamReader($"{Environment.CurrentDirectory}/Files/{key}.json");
                var fileContents = reader.ReadToEnd();
                return Task.FromResult<Tuple<bool, T>>(new Tuple<bool, T>(true, JsonConvert.DeserializeObject<T>(fileContents)));
            }
            catch(Exception ex)
            {
                return Task.FromException<Tuple<bool, T>>(ex);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }

        public Task<bool> UpdateAsync<T>(string key, T value)
        {
            if (!File.Exists($"{Environment.CurrentDirectory}/Files/{key}.json"))
            {
                return Task.FromResult(false);
            }

            TextWriter writer = null;
            try
            {
                File.Delete($"{Environment.CurrentDirectory}/Files/{key}.json");

                var contentsToWriteToFile = JsonConvert.SerializeObject(value);
                writer = new StreamWriter($"{Environment.CurrentDirectory}/Files/{key}.json", true);
                writer.Write(contentsToWriteToFile);

                return Task.FromResult(true);
            }
            catch
            {
                return Task.FromResult(false);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }

        }

        public Task<bool> DeleteAsync<T>(string key)
        {
            if (!File.Exists($"{Environment.CurrentDirectory}/Files/{key}.json"))
            {
                return Task.FromResult(false);
            }

            File.Delete($"{Environment.CurrentDirectory}/Files/{key}.json");

            return Task.FromResult(true);
        }
    }
}
