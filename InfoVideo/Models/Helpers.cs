using System;
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
                    
            var admin = new User { Email = "asd@mail1.ru", FirstName = "Leon", LastName = "Budkouski", Password = ("Asd123"),Login = "Hienadz", Address = "Мінск, Кульман 33", Discount  = 10};
            var user = new User { Email = "kross@mail.ru", FirstName = "Dylan", LastName = "Kross", Password = ("Asd111"), Login = "HienadzA", Address = "Мінск, Багдановіча М 100", Discount = 0};

            context.Users.Add(admin);
            context.Users.Add(user);
       

            context.UserRoles.Add(new UserRoles { Role = adminR, User = admin });
            context.UserRoles.Add(new UserRoles { Role = userR, User = user });

     

            Video v = new Video {Title = "The 100 - Season 3",
                Description = "Пасля ядзернай вайны прайшло 97 гадоў. " +
                              "Рэшткі чалавецтва, якія хаваюцца на касмічнай станцыі «Каўчэг»," +
                              " пакутуюць ад недахопу рэсурсаў, неабходных для далейшага выжывання. " +
                              "Урад вычарпаў усе меры, уключаючы цвёрдае абмежаванне нараджальнасці, " +
                              "але сітуацыя працягвае пагаршацца. Застаецца адзінае выйсце — паспрабаваць вярнуцца на Зямлю. " +
                              "Каб пазнаць, ці прыдатная для жыцця пакінутая даўным-даўно планета, " +
                              "урад адпраўляе туды сотню маладых правапарушальнікаў, у ліку дзецей высокапастаўленых службоўцаў" +
                              " і іншых вядомых на «Каўчэгу» людзей. " +
                              "Зараз лёс усяго чалавецтва залежыць ад гэтай групы «выгнаннікаў»",
                                Date = new DateTime(2014,01,01),
                                Genre = "Фантастыка",
                                Logo = "the100.png"

            };

            Video v1 = new Video
            {
                Title = "The Shannara Chronicles",
                Description = "Канал MTV прадстаўляе фэнтэзі-серыял «Хронікі Шаннары»," +
                              " у аснову якога лёг аднайменны цыкл раманаў фантаста Тэры Брукса. " +
                              "Сам пісьменнік прымаў актыўны ўдзел у адаптацыі сваіх твораў для маленькіх экранаў," +
                              " а таму знатакі яго творчасці цалкам могуць разлічваць на тое, " +
                              "што тэлевізійная версія «Харонікі Шаннары» будзе максімальна адпавядаць першакрыніцы. " +
                              "З моманту публікацыі кніг Брукса прайшло нямала часу, " +
                              "але менавіта цяпер развіццё тэхналогій і наяўныя ў тэлебачання магчымасці, " +
                              "па меркаванні аўтара і прадзюсараў праекта, дазволяць займальна і ў " +
                              "поўнай меры перанесці фантастычны свет пісьменніка на экраны. " +
                              "Гледачоў чакае незабыўнае вандраванне па Чатырох Землях, населеным дзіўнымі істотамі, " +
                              "а візуальны складнік пацешыць нават самых прыдзірлівых серыяламанаў.",
                Date = new DateTime(2015, 05, 07),
                Genre = "Прыгоды, фантастыка",
                Logo = "shannar.jpg"

            };

            context.Video.Add(v);
            context.Video.Add(v1);

    

            Format f= new Format {Codec = "h.264", Container = "mkv", Size = 1560458835};
            Format f1 = new Format { Codec = "mpeg4", Container = "mp4", Size = 1078684114 };


            context.Format.Add(f);
            context.Format.Add(f1);

            Edition e = new Edition {Box = "Діск BluRay", Format = f, Price = 130000, Video = v};
            Edition e1 = new Edition { Box = "Діск DVD", Format = f1, Price = 30000, Video = v1 };

            context.Edition.Add(e);
            context.Edition.Add(e1);

            History h = new History {Date = DateTime.Now, Edition = e,User = admin};
            History h1 = new History { Date = DateTime.Today, Edition = e1, User = user };

            context.History.Add(h);
            context.History.Add(h1);

            context.SaveChanges();
            base.Seed(context);
        }
    }
}