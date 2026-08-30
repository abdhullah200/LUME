using Lume.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LumeData.Models
{
    public class Comment
    {
        /// <summary>
        /// Gets or sets the unique identifier for the Like entity.
        /// </summary>
        public int id     { get; set; }

        /// <summary>
        /// Gets or sets the content of the comment.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the comment was created.
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the comment was last updated.
        /// </summary>
        public DateTime DateUpdated { get; set; }

        //Foreign keys

        /// <summary>
        /// Gets or sets the identifier of the post that is commented on.
        /// </summary>
        public int postId { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who commented on the post.
        /// </summary>
        public int userId { get; set; }

        // Navigation properties
        /// <summary>
        /// Gets or sets the post associated with the like.
        /// </summary>
        public Post post  { get; set; }

        /// <summary>
        /// Gets or sets the user associated with the like.
        /// </summary>
        public User User  { get; set; }
    }
}
