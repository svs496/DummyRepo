using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TwitterClone.Data.Repositories;

namespace TwitterClone.Data.Models
{
    // Add profile data for application users by adding properties to the User class
    public class User : IdentityUser, IEntity<string>
    {
        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }

        [MaxLength(250)]
        public string Biography { get; set; }

        [Required]
        [MaxLength(20)]
        public string Location { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? BirthDay { get; set; }

        public string AvatarUrl { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime AccountCreationDate { get; set; }

        public virtual ICollection<Tweet> OwnTweets { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<UserFollower> Users { get; set; }

        [InverseProperty("Follower")]
        public virtual ICollection<UserFollower> Followers { get; set; }
    }
}
