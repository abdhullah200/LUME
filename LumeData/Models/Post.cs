using LumeData.Models;
using System.ComponentModel.DataAnnotations;

namespace Lume.Data.Models
{
    /// <summary>
    /// Represents a user post with content, image, reports, and timestamps.
    /// </summary>
    public class Post
    {
        /// <summary>
        /// Gets or sets the post identifier.
        /// </summary>
        [Key]
        public int Id               { get; set; }
                                    
        /// <summary>
        /// Gets or sets the post content.
        /// </summary>
        public string Content       { get; set; }
                                    
        /// <summary>               
        /// Gets or sets the URL of the post image.
        /// </summary>              
        public string? ImageUrl     { get; set; }
                                    
        /// <summary>               
        /// Gets or sets the number of reports for the post.
        /// </summary>              
        public int NumOfReports     { get; set; }
                                    
        /// <summary>               
        /// Gets or sets the date and time when the post was created.
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the post was last updated.
        /// </summary>
        public DateTime DateUpdated { get; set; }

        // Foreign key for User

        /// <summary>
        /// Gets or sets the user identifier associated with the post.
        /// </summary>              
        public int UserId           { get; set; }

        /// <summary>
        /// Gets or sets the user associated with the post.
        /// </summary>
        public User User            { get; set; }
    }
}
