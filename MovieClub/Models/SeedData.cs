using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MovieClub.Data;
using System;
using System.Linq;

namespace MovieClub.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MvcTextContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MvcTextContext>>()))
            {
                // Look for any movies.
                if (context.Movie.Any())
                {
                    return;   // DB has been seeded
                }

                context.Movie.AddRange(
                    new Movie
                    {
                        Title = "10 Things I Hate About You",
                        ReleaseDate = DateTime.Parse("1999-03-31"),
                        Genre = "Romantic Comedy",
                        Rating = "7.3",
                        Price = 7.99M,
                        Description = "A modern retelling of Shakespeare's 'The Taming of the Shrew', set in a high school, where two sisters navigate teenage love and life. Cast includes Heath Ledger, Julia Stiles, and Joseph Gordon-Levitt."
                    },

                    new Movie
                    {
                        Title = "The Breakfast Club",
                        ReleaseDate = DateTime.Parse("1985-02-15"),
                        Genre = "High school life",
                        Rating = "7.8",
                        Price = 8.99M,
                        Description = "Five high school students from different walks of life endure a Saturday detention under a power-hungry principal. Cast includes Emilio Estevez, Molly Ringwald, and Judd Nelson."
                    },

                    new Movie
                    {
                        Title = "When Harry Met Sally",
                        ReleaseDate = DateTime.Parse("1989-07-21"),
                        Genre = "Romantic",
                        Rating = "7.6",
                        Price = 7.99M,
                        Description = "A romantic movie that explores whether men and women can be friends without romantic involvement. Starring Billy Crystal and Meg Ryan."
                    },

                    new Movie
                    {
                        Title = "The Dark Knight",
                        ReleaseDate = DateTime.Parse("2008-07-18"),
                        Genre = "Action",
                        Rating = "9.0",
                        Price = 9.99M,
                        Description = "Batman battles the Joker, a criminal mastermind who seeks to plunge Gotham City into anarchy. Directed by Christopher Nolan, starring Christian Bale and Heath Ledger."
                    },

                    new Movie
                    {
                        Title = "Avatar",
                        ReleaseDate = DateTime.Parse("2009-12-18"),
                        Genre = "Science Fiction",
                        Rating = "7.8",
                        Price = 8.99M,
                        Description = "A paraplegic Marine is sent to the moon Pandora on a unique mission and becomes torn between following orders and protecting an alien civilization. Directed by James Cameron, starring Sam Worthington and Zoe Saldana."
                    },

                    new Movie
                    {
                        Title = "FRIENDS",
                        ReleaseDate = DateTime.Parse("1994-09-22"),
                        Genre = "Comedy sit-com",
                        Rating = "8.9",
                        Price = 10.99M,
                        Description = "Follows the personal and professional lives of six twenty to thirty-something friends living in Manhattan. Starring Jennifer Aniston, Courteney Cox, and Matthew Perry."
                    },

                    new Movie
                    {
                        Title = "The Godfather",
                        ReleaseDate = DateTime.Parse("1972-03-24"),
                        Genre = "Crime",
                        Rating = "9.2",
                        Price = 9.99M,
                        Description = "The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son. Directed by Francis Ford Coppola, starring Marlon Brando and Al Pacino."
                    },

                    new Movie
                    {
                        Title = "The Shawshank Redemption",
                        ReleaseDate = DateTime.Parse("1994-09-23"),
                        Genre = "Drama",
                        Rating = "9.3",
                        Price = 9.99M,
                        Description = "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency. Starring Tim Robbins and Morgan Freeman."
                    }
                );
                context.SaveChanges();

                // Seed roles and users
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

                // Create Admin Role
                if (!roleManager.RoleExistsAsync("Admin").Result)
                {
                    var role = new IdentityRole("Admin");
                    roleManager.CreateAsync(role).Wait();
                }

                // Create Admin User
                var adminEmail = "CEOofMovieClub@gmail.com";
                var adminPassword = "Verona2914!";
                var adminUser = userManager.FindByEmailAsync(adminEmail).Result;

                if (adminUser == null)
                {
                    adminUser = new IdentityUser { UserName = adminEmail, Email = adminEmail };
                    var result = userManager.CreateAsync(adminUser, adminPassword).Result;

                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(adminUser, "Admin").Wait();
                    }
                }
            }
        }
    }
}