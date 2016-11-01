using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace mrcarson.content
{
    public static class ContentLibrary
    {
        public static IEnumerable<Content> ImportFromCsvFile(string filenme) {
            var lines = File.ReadLines(filenme, Encoding.UTF8);
            var contents = lines.Select(line => {
                var parts = line.Split(';');
                return new Content {
                    Id = Guid.NewGuid().ToString(),
                    Message = parts[0],
                    Link = parts[1]
                };
            });
            return contents;
        }

        public static void SaveAsJsonFile(IEnumerable<Content> contents, string filename) {
            var json = JsonConvert.SerializeObject(contents);
            File.WriteAllText(filename, json);
        }

        public static IEnumerable<Content> ReadFromJsonFile(string filename) {
            var json = File.ReadAllText(filename);
            var contents = JsonConvert.DeserializeObject<IEnumerable<Content>>(json);
            return contents;
        }
    }
}