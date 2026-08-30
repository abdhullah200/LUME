namespace Lume.ViewModels.Home
{
    public class PostComment
    {
        /// <summary>
        /// Gets or sets the identifier of the post that is being commented on.
        /// </summary>
        public int PostId { get; set; }

        /// <summary>
        /// Gets or sets the content of the comment.
        /// </summary>
        public string Content { get; set; }
    }
}
