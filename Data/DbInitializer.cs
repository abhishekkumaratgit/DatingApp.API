using DatingApp.API.Helper;
using DatingApp.API.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DatingApp.API.Data
{
    public static class DbInitializer
    {
        public static void Initialize(DataContext dbContext)
        {
            if (!dbContext.Users.Any())
            {
                var userData = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(@"Data/UserSeedData.json"));

                foreach (var user in userData)
                {
                    byte[] passwordHash, passwordSalt;
                    Utility.CreatePasswordHash("password", out passwordHash, out passwordSalt);
                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;
                    user.UserName = user.UserName.ToLower();
                    dbContext.Users.Add(user);
                }
                dbContext.SaveChanges();
            }
        }
    }
}