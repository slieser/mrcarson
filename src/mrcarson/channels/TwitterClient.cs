using System;
using System.Linq;
using mrcarson.infrastructure;
using Tweetinvi;
using Tweetinvi.Core.Exceptions;
using Tweetinvi.Models;

namespace mrcarson.channels
{
    public class TwitterClient : ITwitterClient
    {
        private const string ConsumerKey = "NZabF89r22GKB4eX0tizqMqBi";
        private const string ConsumerSecret = "ymGOxXysZDpTwWfUm6rDEw9pv6kj72PpZO5MQ8EDgMwca8SjRX";

        private IAuthenticationContext authenticationContext;

        public void AuthenticateAppViaBrowser() {
            var appCredentials = new TwitterCredentials(ConsumerKey, ConsumerSecret);
            authenticationContext = AuthFlow.InitAuthentication(appCredentials);
            Browser.Open(authenticationContext.AuthorizationURL);
        }

        public Token CreateTokenFromPin(string pin) {
            var userCredentials = AuthFlow.CreateCredentialsFromVerifierCode(pin, authenticationContext);
            var token = new Token { Key = userCredentials.AccessToken, Secret = userCredentials.AccessTokenSecret };
            return token;
        }

        public ITweet Tweet(string message, Token token) {
            var user = GetAuthenticatedUser(token);
            var response = user.PublishTweet(message);
            if (response == null) {
                var twitterException = ExceptionHandler.GetLastException();
                var status = GetExceptionInfos(twitterException);
                throw new Exception(status);
            }
            return response;
        }

        private IAuthenticatedUser GetAuthenticatedUser(Token token) {
            var credentials = Auth.SetUserCredentials(ConsumerKey, ConsumerSecret, token.Key, token.Secret);
            var user = User.GetAuthenticatedUser(credentials);
            return user;
        }

        private static string GetExceptionInfos(ITwitterException twitterException) {
            var messages = string.Join("\n", twitterException.TwitterExceptionInfos.Select(x => x.Message));
            var status = $"{twitterException.TwitterDescription}\n   {messages}";
            return status;
        }
    }
}