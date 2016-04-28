using System.Data.Entity;

namespace InfoVideo.Models
{
   

    public class Helpers
    {
    }

    public class MyDbUserInitializer : DropCreateDatabaseAlways<InfoVideoContext>
    {
        protected override void Seed(InfoVideoContext context)
        {
            var adminR = new Role { Name = "Administrator" };
            var userR = new Role { Name = "User" };

            context.Roles.Add(adminR);
            context.Roles.Add(userR);


            var admin = new User { Email = "asd@mail1.ru", FirstName = "Leon", LastName = "Budkouski", Password = ("asd123"),Login = "Hienadz"};
            var user = new User { Email = "kross@mail.ru", FirstName = "Dylan", LastName = "Kross", Password = ("asd111"), Login = "HienadzA" };

            context.Users.Add(admin);
            context.Users.Add(user);

            context.UserRoles.Add(new UserRoles { Role = adminR, User = admin });
            context.UserRoles.Add(new UserRoles { Role = userR, User = user });

            context.SaveChanges();

            base.Seed(context);
        }
    }
}