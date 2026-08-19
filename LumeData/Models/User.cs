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
        public ICollection<Post> Posts   { get; set; } = new List<Post>();
    }
}
