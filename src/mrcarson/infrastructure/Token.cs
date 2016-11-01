using System.IO;
using System.Reflection;

namespace mrcarson.infrastructure
{
    public class Token
    {
        public string Key { get; set; }

        public string Secret { get; set; }

        public static Token ReadFromRessource(Assembly assembly, string name) {
            var token = new Token();
            var stream = assembly.GetManifestResourceStream(name);
            using (var streamReader = new StreamReader(stream)) {
                ReadToken(streamReader, token);
            }
            return token;
        }

        public static Token ReadFromFile(string name) {
            var token = new Token();
            using (var stream = new FileStream(name, FileMode.Open)) {
                using (var streamReader = new StreamReader(stream)) {
                    ReadToken(streamReader, token);
                }
            }
            return token;
        }

        public void WriteToFile(string name) {
            using (var stream = new FileStream(name, FileMode.OpenOrCreate)) {
                using (var streamWriter = new StreamWriter(stream)) {
                    streamWriter.WriteLine(Key);
                    streamWriter.WriteLine(Secret);
                }
            }
        }

        private static void ReadToken(StreamReader streamReader, Token token) {
            token.Key = streamReader.ReadLine();
            token.Secret = streamReader.ReadLine();
        }
    }
}