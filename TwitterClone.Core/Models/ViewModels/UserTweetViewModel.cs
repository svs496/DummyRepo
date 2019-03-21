using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TwitterClone.Core.Models.ViewModels
{
    public class UserTweetViewModel
    {

        public UserTweetViewModel()
        {
            OwnTweets = new List<TweetViewModel>();
        }

        public string UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserAvatarUrl { get; set; }

        public string UserBiography { get; set; }

        public string UserLocation { get; set; }

        
        public string UserBirthDay { get; set; }

        [NotMapped]
       
        public string UserAccountCreationDate { get; set; }

        [NotMapped]
        public int? FollowersCount { get; set; }

        [NotMapped]
        public int? TotalTweetCount { get; set; }

        [NotMapped]
        public int? FollowingCount { get; set; }

        [NotMapped]
        public string UserFullName { get; set; }

        public virtual ICollection<TweetViewModel> OwnTweets { get; set; }

        public string TwitterHandle { get; set; }

        public TweetViewModel TweetVM { get; set; }

        public bool FollowFlag { get; set; }


    }
}
