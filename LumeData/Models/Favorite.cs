using Lume.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LumeData.Models
{
    public class Favorite
    {
        /// <summary>
        /// Gets or sets the unique identifier for the Like entity.
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the like was created.
        /// </summary>
        public DateTime dateCreated { get; set; } 

        /// <summary>
        /// Gets or sets the identifier of the post that is liked.
        /// </summary>
        public int postId { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who liked the post.
        /// </summary>
        public int userId { get; set; }

        // Navigation properties
        /// <summary>
        /// Gets or sets the post associated with the like.
        /// </summary>
        public Post post { get; set; }

        /// <summary>
        /// Gets or sets the user associated with the like.
        /// </summary>
        public User user { get; set; }
    }
}
