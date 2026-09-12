using Lume.Data.Models;

namespace Lume.ViewModels.Profile
{
    /// <summary>
    /// Represents the view model for the Instagram-style profile page.
    /// Follower/following counts, bio, and story highlights are placeholders
    /// until those features have real database backing (Follow, StoryHighlight
    /// entities, User.Bio/Username columns do not exist yet).
    /// </summary>
    public class ProfileVM
    {
        /// <summary>
        /// The unique identifier of the user whose profile is being viewed.
        /// </summary>
        public int Id                                        { get; set; }

        /// <summary>
        /// The full name of the user whose profile is being viewed.
        /// </summary>
        public string FullName                               { get; set; }

        /// <summary>
        /// The URL of the profile picture of the user whose profile is being viewed.
        /// </summary>
        public string ProfilePictureUrl                      { get; set; }

        /// <summary>
        /// The number of posts made by the user whose profile is being viewed.
        /// </summary>
        public int PostCount                                 { get; set; }

        /// <summary>
        /// The number of followers of the user whose profile is being viewed.
        /// </summary>
        public int FollowerCount                             { get; set; } = 0;

        /// <summary>
        /// The number of users that the user whose profile is being viewed is following.
        /// </summary>
        public int FollowingCount                            { get; set; } = 0;
        
        /// <summary>
        /// The bio of the user whose profile is being viewed.
        /// </summary>
        // Placeholder - User has no Bio column yet
        public string Bio                                    { get; set; } = "";

        /// <summary>
        /// Indicates whether the profile being viewed belongs to the logged-in user.
        /// </summary>
        public bool IsOwnProfile                             { get; set; }

        /// <summary>
        /// The list of posts made by the user whose profile is being viewed.
        /// </summary>
        public List<Post> Posts                              { get; set; } = new();

        // Placeholder - no StoryHighlight entity in the database yet
        /// <summary>
        /// The list of story highlights for the user whose profile is being viewed.
        /// </summary>
        public List<ProfileStoryHighlightVM> StoryHighlights { get; set; } = new();
    }
}
