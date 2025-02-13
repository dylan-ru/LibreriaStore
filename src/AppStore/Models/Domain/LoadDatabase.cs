//Loading data master
using System.Security.Authentication;
using Microsoft.AspNetCore.Identity;

namespace AppStore.Models.Domain;

public class LoadDatabase
{
    public static async Task InsertData(DatabaseContext context, 
                                        UserManager<ApplicationUser> userManager, 
                                        RoleManager<IdentityRole> roleManager)
    {
        if (!context.Roles.Any())
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
        }

        if (!userManager.Users.Any())
        {
            var user = new ApplicationUser
            {
                UserName = "admin",
                FirstName = "Alex",
                Email = "admin@gmail.com",
                EmailConfirmed = true,
            };

            await userManager.CreateAsync(user, "Admin@123");
            await userManager.AddToRoleAsync(user, "Admin");
        }

        if (!context.Categories!.Any())
        {
            await context.Categories!.AddRangeAsync(new Category {Name = "Drama"},
                                        new Category {Name = "Comedy"},
                                        new Category {Name = "Action"},
                                        new Category {Name = "Adventure"},
                                        new Category {Name = "Fantasy"},
                                        new Category {Name = "Mystery"},
                                        new Category {Name = "Romance"},
                                        new Category {Name = "Thriller"});

            await context.SaveChangesAsync();
        }

        if (!context.Books!.Any())
        {
            await context.Books!.AddRangeAsync(new Book {Title = "The Dark Knight",
                                            Author = "Christopher Nolan",
                                            Image = "https://prnt.sc/8e52_RwvTrEI",
                                            CreateDate = "2008-07-18",
                                            },
                                            new Book {Title = "Quijote de la Mancha",
                                            Author = "Miguel de Cervantes",
                                            Image = "https://prnt.sc/BgBJI3-_hELh",
                                            CreateDate = "1605-01-01",
                                            },
                                            new Book {Title = "Harry Potter y la piedra filosofal",
                                            Author = "J.K. Rowling",
                                            Image = "https://prnt.sc/UkTueZdy0A0D",
                                            CreateDate = "2001-06-26",
                                            }
                                            );

            await context.SaveChangesAsync();
        }

        if (!context.CategoryBooks!.Any())
        {
            await context.CategoryBooks!.AddRangeAsync(new CategoryBook {CategoryId = 1, BookId = 1},
                                            new CategoryBook {CategoryId = 2, BookId = 2}
            );

            await context.SaveChangesAsync();
        }

        await context.SaveChangesAsync();
    }
}

