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

            var userList = new List<User>();
            var tweetList = new List<Tweet>();

            var usersFileContents = File.ReadAllLines("user.txt");

            var tweetFileContents = File.ReadAllLines("tweet.txt");


            foreach (var userLine in usersFileContents)
            {
                var userDetails = userLine.Split("follows");
                var userName = userDetails[0].Trim();
                var peopleThisUserFollows = userDetails[1].Split(',').ToList();

                userList.Add(new User
                {
                    Name = userName,
                    PeopleThisUserFollows =
                        peopleThisUserFollows
                            .Select(x => x.Trim())
                            .ToList()
                });
            }

            var count = 1;

            foreach (var tweetLine in tweetFileContents)
            {
                var tweetDetails = tweetLine.Split(">");
                var user = tweetDetails[0].Trim();
                var tweet = tweetDetails[1].Trim();

                if (tweet.Length > 280)
                {
                    continue;
                }

                tweetList.Add(new Tweet
                {
                    Id = count,
                    User = user,
                    Message = tweet
                });
                count++;
            }

            foreach (var user in userList.OrderBy(x=>x.Name))
            {
                Console.WriteLine(user.Name);
                foreach (var tweet in tweetList.OrderBy(x=>x.Id))
                {
                    if
                    (
                        user.PeopleThisUserFollows.Contains(tweet.User) ||
                        user.Name == tweet.User
                    )
                    {
                        Console.WriteLine($"\t@{tweet.User}: {tweet.Message}");
                    }
                }
            }
        }
    }
}
