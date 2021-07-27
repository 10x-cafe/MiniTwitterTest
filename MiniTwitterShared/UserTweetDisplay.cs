using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniTwitterShared
{
    public class UserTweetDisplay
    {
        public List<User> Users { get; set; } = new List<User>();

        public UserTweetDisplay(List<string> usersFileContents, List<string> tweetFileContents)
        {
            var userList = new List<User>();

            var tweetList = new List<Tweet>();


            foreach (var userLine in usersFileContents)
            {
                var userDetails = userLine.Split("follows");
                var userName = userDetails[0].Trim();
                var peopleThisUserFollows = userDetails[1].Split(',').ToList();

                if (userList.Select(x=>x.Name).Contains(userName))
                {
                    foreach (var user in peopleThisUserFollows
                            .Select(x => x.Trim())
                            .Where(
                                x=>
                                    !userList
                                    .Select(x=>x.Name)
                                    .Contains(x)
                            )
                            .Distinct()
                            .ToList()
                    )
                    {
                        var _user = userList.Where(x => x.Name == userName).FirstOrDefault();
                        if (_user is not null)
                        {
                            _user.PeopleThisUserFollows.Add(user);
                        }
                    }
                    continue;

                }
                userList.Add(new User
                {
                    Name = userName.Trim(),
                    PeopleThisUserFollows =
                        peopleThisUserFollows
                            .Select(x => x.Trim())
                            .Distinct()
                            .ToList()
                });
            }
            foreach (var userLine in usersFileContents)
            {
                var userDetails = userLine.Split("follows");
                var peopleThisUserFollows = userDetails[1].Split(',').ToList();

                foreach (var user in peopleThisUserFollows)
                {
                    if (!userList.Select(x => x.Name).Contains(user.Trim()))
                    {
                        userList.Add(new User
                        {
                            Name = user.Trim()
                        });
                    }
                }
            }

            var count = 1;

            foreach (var tweetLine in tweetFileContents)
            {
                var tweetDetails = tweetLine.Split('>');
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

            foreach (var user in userList.OrderBy(x => x.Name))
            {
                Users.Add(user);
                foreach (var tweet in tweetList.OrderBy(x => x.Id))
                {

                    if
                    (
                        user.PeopleThisUserFollows.Contains(tweet.User) ||
                        user.Name == tweet.User
                    )
                    {
                        user.UserFeed.Add(tweet);
                    }
                }
            }
        }
    }
}
