using System.Data.Entity;

namespace InfoVideo.Models
{
    //public enum Cities
    //{
    //    Мінск, Гародня, Віцебск, Магілёў, Гомель, Брэст
    //}
    //public enum StreetsM
    //{
    //    Багдановіча, Кульман, Броўкі, Сурганава
    //}

    //public enum StreetsH
    //{
    //    Шчарбакова, Прохарава
    //}

    //public enum StreetsV
    //{
    //    Мінкевіч, Лекароў, Дыетолагаў
    //}

    //public enum StreetsMa
    //{
    //    Мінкевіч, Лекароў, Дыетолагаў
    //}

    //public enum StreetsG
    //{
    //    Белая, Цягнікоў, Колавая
    //}

    //public enum StreetsB
    //{
    //    Пераможцаў, Танкаў, Самалётаў
    //}


    public class Helpers
    {
    }

    public class MyDbUserInitializer : DropCreateDatabaseIfModelChanges<InfoVideoContext>
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