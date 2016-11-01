using System;
using System.IO;
using mrcarson.channels;
using mrcarson.content;
using mrcarson.infrastructure;
using Tweetinvi.Models;

namespace mrcarson
{
    public class MrCarson
    {
        private readonly ITwitterClient twitterClient;
        private const string CredentialsFilename = "twitter.credentials";
        private const string CsvFilename = "tweets.csv";
        private const string ContentLibraryFilename = "content.json";

        public bool AuthenticationRequired() {
            return !File.Exists(CredentialsFilename);
        }

        public MrCarson()
            : this(new TwitterClient()) {
        }

        internal MrCarson(ITwitterClient twitterClient) {
            this.twitterClient = twitterClient;
        }

        public void Authenticate() {
            twitterClient.AuthenticateAppViaBrowser();

            Console.Write("Please enter the pin: ");
            var pin = Console.ReadLine();

            var token = twitterClient.CreateTokenFromPin(pin);
            token.WriteToFile(CredentialsFilename);
        }

        public bool ContentImportRequired() {
            return !File.Exists(ContentLibraryFilename);
        }

        public void ImportContent() {
            var contents = ContentLibrary.ImportFromCsvFile(CsvFilename);
            ContentLibrary.SaveAsJsonFile(contents, ContentLibraryFilename);
        }

        public void Tweet() {
            var token = Token.ReadFromFile(CredentialsFilename);
            var contents = ContentLibrary.ReadFromJsonFile(ContentLibraryFilename);
            var contentFinder = new ContentFinder(contents);
            var contentToPublish = contentFinder.GetNextContent();
            var response = Tweet(contentToPublish, token);
            ContentLibrary.SaveAsJsonFile(contents, ContentLibraryFilename);

            Console.WriteLine($"Id of tweet is: {response.IdStr}");
        }

        private ITweet Tweet(Content contentToPublish, Token token) {
            var response = twitterClient.Tweet($"{contentToPublish.Message} - {contentToPublish.Link}", token);
            contentToPublish.PublishedId = response.IdStr;
            contentToPublish.PublishedAt = DateTime.Now;
            return response;
        }
    }
}