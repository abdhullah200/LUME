using Lume.Data;
using Lume.ViewModels.Profile;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lume.Controllers
{
    /// <summary>
    /// Controller responsible for handling requests related to the profile page.
    /// </summary>
    public class ProfileController : Controller
    {
        private readonly AppDbContext _context;

        public ProfileController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Displays the profile page for the given user id.
        /// Falls back to the logged in user when no id is supplied.
        /// </summary>
        /// <param name="id">The id of the user whose profile is being viewed.</param>
        public async Task<IActionResult> Index(int? id)
        {
            // TODO: Get logged in user from session, same as HomeController
            int loggedInUserId = 1;
            int profileUserId  = id ?? loggedInUserId;

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == profileUserId);

            if (user == null)
            {
                return NotFound();
            }

            var posts = await _context.Posts
                .Include(p => p.likes)
                .Include(p => p.comments)
                .Where(p => p.UserId == user.Id)
                .OrderByDescending(p => p.DateCreated)
                .ToListAsync();

            var viewModel = new ProfileVM
            {
                Id                = user.Id,
                FullName          = user.fullName,
                ProfilePictureUrl = string.IsNullOrEmpty(user.ProfilePictureUrl)
                    ? "/images/avatar/user.png"
                    : user.ProfilePictureUrl,

                PostCount    = posts.Count,
                IsOwnProfile = profileUserId == loggedInUserId,
                Posts        = posts

                // FollowerCount, FollowingCount, Bio, StoryHighlights are left
                // at their ProfileVM defaults (0 / "" / empty list) until
                // those features have real database backing.
            };

            return View(viewModel);
        }
    }
}
