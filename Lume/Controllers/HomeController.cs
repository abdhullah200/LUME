using Lume.Data;
using Lume.Data.Models;
using Lume.ViewModels.Home;
using LumeData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Lume.Controllers
{
    /// <summary>
    /// Controller responsible for handling requests related to the home page and posts.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext            _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class with the specified logger and database context.
        /// </summary>
        /// <param name="logger">The logger to be used for logging messages.</param>
        /// <param name="context">The database context to be used for data access.</param>
        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger  = logger;
            _context = context;
        }

        /// <summary>
        /// Handles the request to display the home page.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="IActionResult"/> that renders the home page view.</returns>
        public async Task<IActionResult> Index()
        {
            var allPosts = await _context.Posts
                .Include(n => n.User)
                .Include(n => n.likes)
                .Include(n => n.favorites)
                .Include(n => n.comments).ThenInclude(c => c.User)
                .OrderByDescending(n => n.DateCreated)
                .ToListAsync();

            return View(allPosts);
        }

        /// <summary>
        /// Handles the creation of a new post. 
        /// This action method is invoked when a POST request is made to create a new post. 
        /// It retrieves the logged-in user's ID, creates a new Post entity, 
        /// saves any uploaded image, and adds the post to the database.
        /// </summary>
        /// <param name="post">The view model containing the data for the new post.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="IActionResult"/> that redirects to the Index action.</returns>
        [HttpPost]
        public async Task<IActionResult> CreatePost(PostVM post)
        {
            // TODO: Get logged in user from session
            int loggedInUserId = 1;

            // Create a new Post entity and set its properties
            var newPost = new Post
            {
                Content      = post.Content,
                DateCreated  = DateTime.UtcNow,
                DateUpdated  = DateTime.UtcNow,
                NumOfReports = 0,
                ImageUrl     = "",
                UserId       = loggedInUserId
            };

            // check and save image
            if (post.Image != null && post.Image.Length > 0)
            {
                string rootFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                if(post.Image.ContentType.Contains("image"))
                {
                    string rootFolderImages = Path.Combine(rootFolderPath, "images/Uploaded");
                    Directory.CreateDirectory(rootFolderImages); // Ensure the images directory exists

                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(post.Image.FileName);
                    string filePath = Path.Combine(rootFolderImages, fileName);

                    using(var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await post.Image.CopyToAsync(stream);
                    }

                    //set url to the new post object
                    newPost.ImageUrl = "/images/Uploaded/" + fileName;
                }
            }

            // Add the post to the database
            await _context.Posts.AddAsync(newPost);
            await _context.SaveChangesAsync();

            // Redirect to the Index action to display the updated list of posts
            return RedirectToAction("Index");

        }

        /// <summary>
        /// Handles the toggling of a like for a post.
        /// </summary>
        /// <param name="postLike">The view model containing the data for the post like.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="IActionResult"/> that redirects to the Index action.</returns>
        [HttpPost]
        public async Task<IActionResult> TogglePostLike(PostLike postLike)
        {
            int loggedInUserId = 1;

            // Check if the user has already liked the post
            var like = await _context.Likes
                .Where(l => l.postId == postLike.PostId && l.userId == loggedInUserId)
                .FirstOrDefaultAsync();

            if(like !=null)
            {
                _context.Likes.Remove(like);
                await _context.SaveChangesAsync();
            }
            else
            {
                var newLike = new Like()
                {
                    postId = postLike.PostId,
                    userId = loggedInUserId
                };
                await _context.Likes.AddAsync(newLike);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Handles the toggling of a favorite for a post.
        /// </summary>
        /// <param name="favoritePosts">The view model containing the data for the favorite post.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="IActionResult"/> that redirects to the Index action.</returns>
        [HttpPost]
        public async Task<IActionResult> ToggleFavorite(FavoritePosts favoritePosts)
        {
            int loggedInUserId = 1;

            // Check if the user has favorite liked the post
            var favorite = await _context.Favorites
                .Where(l => l.postId == favoritePosts.PostId && l.userId == loggedInUserId)
                .FirstOrDefaultAsync();

            if (favorite != null)
            {
                _context.Favorites.Remove(favorite);
                await _context.SaveChangesAsync();
            }
            else
            {
                var newfavorite = new Favorite()
                {
                    postId = favoritePosts.PostId,
                    userId = loggedInUserId
                };
                await _context.Favorites.AddAsync(newfavorite);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Handles the toggling of the visibility of a post (private/public).
        /// </summary>
        /// <param name="postVisibility">The view model containing the data for the post visibility.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="IActionResult"/> that redirects to the Index action.</returns>
        [HttpPost]
        public async Task<IActionResult> TogglePostVisiblity(PostVisibility postVisibility)
        {
            int loggedInUserId = 1;

            var post = await _context.Posts
                .FirstOrDefaultAsync(l => l.Id == postVisibility.PostId && l.UserId == loggedInUserId);

            if (post != null)
            {
                post.IsPrivate = !post.IsPrivate;
                _context.Posts.Update(post);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }


        /// <summary>
        /// Handles the addition of a comment to a post.
        /// </summary>
        /// <param name="postComment">The view model containing the data for the post comment.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="IActionResult"/> that redirects to the Index action.</returns>
        [HttpPost]
        public async Task<IActionResult> AddPostComment(PostComment postComment )
        {
            int loggedInUserId = 1;

            var newComment = new Comment()
            {
                postId      = postComment.PostId,
                userId      = loggedInUserId,
                Content     = postComment.Content,
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow
            };
            await _context.Comments.AddAsync(newComment);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemovePostComment(RemoveComment removeComment)
        {
            var commentDb = await _context.Comments.FirstOrDefaultAsync(c => c.id == removeComment.CommentId);

            if(commentDb != null)
            {
                _context.Comments.Remove(commentDb);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");

        }
    }
}
