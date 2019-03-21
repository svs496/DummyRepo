using System;
using System.Collections.Generic;
using System.Text;
using TwitterClone.Data.Repositories;

namespace TwitterClone.Data.Models
{
    public class UserFollower : IEntity<int>
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public string FollowerId { get; set; }
        public User Follower { get; set; }

        public FollowActions FollowAction { get; set; }
    }

    public enum FollowActions
    {
        Following = 1,
        NotFollowing = 0
    }
}
