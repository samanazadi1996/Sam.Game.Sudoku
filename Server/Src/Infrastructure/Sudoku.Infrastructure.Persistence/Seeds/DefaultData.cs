using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sudoku.Application.Helpers;
using Sudoku.Domain.Entities;
using Sudoku.Infrastructure.Persistence.Contexts;
using System.Linq;
using Sudoku.Domain.Enums;

namespace Sudoku.Infrastructure.Persistence.Seeds
{
    public static class DefaultData
    {
        public static async Task SeedAsync(ApplicationDbContext applicationDbContext)
        {
            await SeedUsersAsync(applicationDbContext);

            await SeedGamesAsync(applicationDbContext);

            await applicationDbContext.SaveChangesAsync();
        }
        private static async Task SeedGamesAsync(ApplicationDbContext applicationDbContext)
        {
            if (await applicationDbContext.Games.AnyAsync()) return;

            var games = new List<Game>();

            foreach (var level in Enum.GetValues<GameLevel>())
            {
                for (int i = 0; i < 30; i++)
                {
                    games.Add(Game.Generate(level));
                }
            }
            await applicationDbContext.Games.AddRangeAsync(games);
        }
        private static async Task SeedUsersAsync(ApplicationDbContext applicationDbContext)
        {
            if (await applicationDbContext.Roles.AnyAsync()) return;


            Role adminRole = new Role() { Id = IdGenerator.Generate(), Name = Role.AdminRoleName, Title = "مدیر" };
            //Role clientAdminRole = new Role() { Id = IdGenerator.Generate(), Name = Role.ClientAdminRoleName, Title = "مدیر پروژه" };
            //Role clientExpertRole = new Role() { Id = IdGenerator.Generate(), Name = Role.ClientExpertRoleName, Title = "کارشناس" };

            List<User> users = new List<User> {
                    new User(){
                        IsActive=true,
                        PhoneNumber="09919953221",
                        PasswordHash="e1496edf658d83aa5a5b547ca5443f5f3aca45ebf60634a78b1ad72d6981b2da",
                        Created=DateTime.Now,
                        FirstName="سامان",
                        LastName="آزادی",
                        UserName="Admin",
                        ProfileImage=RandomHelper.GetProfileImage(),
                        SecurityStamp = Guid.NewGuid().ToString(),
                        UserRoles= new List<UserRole>{
                            new UserRole(){
                                RoleId=adminRole.Id,
                            }
                        }
                    }
                };

            await applicationDbContext.Roles.AddAsync(adminRole);
            await applicationDbContext.Users.AddRangeAsync(users);
        }
    }
}
