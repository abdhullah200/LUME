using Lume.Data;
using Lume.Data.Models;
using LumeData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LumeData.Helpers
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(AppDbContext appDbContext)
        {
            if (!appDbContext.Users.Any() && !appDbContext.Posts.Any()) 
            { 
                var newUser = new User()
                {
                    fullName = "Abdhullah Ariff",
                    ProfilePictureUrl = "https://example.com/profile-picture.jpg"
                };
                await appDbContext.Users.AddAsync(newUser);
                await appDbContext.SaveChangesAsync();

                var newPost = new Post()
                {
                    Content = "This is the content of my first post.",
                    ImageUrl="",
                    NumOfReports = 0,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    UserId = newUser.Id
                };

                var newwithImage = new Post()
                {
                    Content = "This is the content of my second post.",
                    ImageUrl = "https://commons.wikimedia.org/wiki/File:Example_image.svg",
                    NumOfReports = 0,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    UserId = newUser.Id
                };

                await appDbContext.Posts.AddRangeAsync(newPost, newwithImage);
                await appDbContext.SaveChangesAsync();
            }
        }
    }
}

