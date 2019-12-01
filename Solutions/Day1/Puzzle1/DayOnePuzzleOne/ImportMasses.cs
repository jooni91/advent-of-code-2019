using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;

namespace DayOnePuzzleOne
{
    public class ImportMasses
    {
        private readonly HttpClient _httpCLient;

        public ImportMasses()
        {
            _httpCLient = new HttpClient();
        }

        public async IAsyncEnumerable<int> GetMassesFromWeb(string url)
        {
            using var stream = new StreamReader(await _httpCLient.GetStreamAsync(url));

            while (!stream.EndOfStream)
            {
                yield return Convert.ToInt32(await stream.ReadLineAsync());
            }
        }
        public async IAsyncEnumerable<int> GetMassesFromEmbeddedFile()
        {
            using var stream = new StreamReader(File.OpenRead("ImportedMasses.txt"));

            while (!stream.EndOfStream)
            {
                yield return Convert.ToInt32(await stream.ReadLineAsync());
            }
        }

        public void Dispose() => _httpCLient.Dispose();
    }
}
