using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;

namespace MiniTwitterShared
{
    public static class IFormFileExtensions
    {
        public static List<string> ReadAsList(this IFormFile file)
        {
            var result = new List<string>();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                    result.Add(reader.ReadLine());
            }
            return result;
        }
    }
}
