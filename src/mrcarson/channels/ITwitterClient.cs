using mrcarson.infrastructure;
using Tweetinvi.Models;

namespace mrcarson.channels
{
    public interface ITwitterClient
    {
        void AuthenticateAppViaBrowser();

        Token CreateTokenFromPin(string pin);

        ITweet Tweet(string message, Token token);
    }
}