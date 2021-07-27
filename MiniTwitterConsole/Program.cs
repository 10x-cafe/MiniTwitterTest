using MiniTwitterShared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MiniTwitterConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!File.Exists("user.txt"))
            {
                Console.WriteLine("user.txt does not exist. " +
                    "Please make sure that the file is placed in " +
                    "the same file as the application.");
                return;
            }
            if (!File.Exists("tweet.txt"))
            {
                Console.WriteLine("tweet.txt does not exist. " +
                    "Please make sure that the file is placed in " +
                    "the same file as the application.");
                return;
            }


            var usersFileContents = File.ReadAllLines("user.txt");

            var tweetFileContents = File.ReadAllLines("tweet.txt");

            var userDisplayModel =
                new UserTweetDisplay(
                    usersFileContents.ToList(),
                    tweetFileContents.ToList()
                );

            foreach (var user in userDisplayModel.Users)
            {
                Console.WriteLine(user.Name);
                foreach (var tweet in user.UserFeed)
                {
                    Console.WriteLine($"\t@{tweet.User}: {tweet.Message}");
                }
            }
        }
    }
}
