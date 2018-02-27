using BBS.Data;
using BBS.Models;
using System;
using System.Linq;

namespace BBS.Data
{
    public class DbInitializer
    {
        public static void Initialize(BBSContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return;
            }

            var users = new User[]
            {
                new User{Name="000000",Password="123456",AddTime=DateTime.Parse("2018-02-27"),State=State.Offline}
            };
            foreach(User u in users)
            {
                context.Users.Add(u);
            }
            context.SaveChanges();
        }
    }
}
