namespace mrcarson
{
    public class Program
    {
        public static void Main(string[] args) {
            var mrCarson = new MrCarson();

            if (mrCarson.AuthenticationRequired()) {
                mrCarson.Authenticate();
            }
            if (mrCarson.ContentImportRequired()) {
                mrCarson.ImportContent();
            }
            mrCarson.Tweet();
        }
    }
}