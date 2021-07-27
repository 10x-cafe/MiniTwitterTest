using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniTwitterShared;
using System.IO;
using System.Linq;
using System.Text;

namespace MiniTwitterTests
{
    [TestClass]
    public class MiniTestSuite
    {
        [TestMethod]
        public void OutputTest()
        {
            var usersFileContents =
                File.ReadAllLines("assets/user.txt");

            var tweetFileContents =
                File.ReadAllLines("assets/tweet.txt");

            var correctResultFileContents =
                File.ReadAllText("assets/correctResult.txt")
                .Replace("\r","");


            var testModel =
                new UserTweetDisplay(
                    usersFileContents.ToList(),
                    tweetFileContents.ToList()
                );


            var stringBuilder = new StringBuilder();

            foreach (var user in testModel.Users)
            {
                stringBuilder.Append(user.Name + '\n');
                foreach (var tweet in user.UserFeed)
                {
                    stringBuilder.Append($"    @{tweet.User}: {tweet.Message}\n");
                }

            }
            var result = stringBuilder.ToString().Trim('\n');

            Assert.AreEqual(correctResultFileContents, result);
        }
    }
}
