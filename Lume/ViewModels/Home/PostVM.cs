namespace Lume.ViewModels.Home
{
    /// <summary>
    /// Represents the view model for creating a new post, containing the content and an optional image file.
    /// </summary>
    public class PostVM
    {
        /// <summary>
        /// Gets or sets the content of the post.
        /// </summary>
        public string Content  { get; set; }
                               
        /// <summary>
        /// Gets or sets the image file associated with the post.
        /// </summary>
        public IFormFile Image { get; set; }
    }
}
