using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TwitterClone.Data.Repositories;

namespace TwitterClone.Data.Models
{
    public class Tweet :IEntity<int>
    {
        [Key]
        public int Id { get; set; }

        [MinLength(2)]
        [MaxLength(140)]
        public string Content { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        
        public DateTime ModifiedDate { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
