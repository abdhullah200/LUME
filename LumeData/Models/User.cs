using Lume.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LumeData.Models
{
    /// <summary>
    /// Represents an application user.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public int Id                    { get; set; }

        /// <summary>
        /// Gets or sets the full name of the user.
        /// </summary>
        public string fullName           { get; set; }
                                         
        /// <summary>                    
        /// Gets or sets the URL of the user's profile picture.
        /// </summary>
        public string? ProfilePictureUrl { get; set; }

        // Navigation property

        /// <summary>
        /// Gets or sets the collection of posts associated with the user.
        /// </summary>                   
        public ICollection<Post> Posts       { get; set; } = new List<Post>();

        /// <summary>
        /// Gets or sets the collection of likes associated with the user.
        /// </summary>
        public ICollection<Like> Likes         { get; set; } = new List<Like>();
        
        /// <summary>
        /// Gets or sets the collection of comments associated with the user.
        /// </summary>
        public ICollection<Comment> Comments   { get; set; } = new List<Comment>();

        /// <summary>
        /// Gets or sets the collection of favorites associated with the user.
        /// </summary>
        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
    }
}
