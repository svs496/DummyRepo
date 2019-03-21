using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterClone.Core.Models.ViewModels
{
    public class TweetViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(140, ErrorMessage = "The Tweet can be up to {1} characters long.", MinimumLength = 2)]
        public string Content { get; set; }

        public string DisplayCreateDate { get; set; }

        public string DisplayModifedDate { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }
        public bool OwnTweet { get; set; }
    }
}
