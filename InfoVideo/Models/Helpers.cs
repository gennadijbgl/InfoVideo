using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace InfoVideo.Models
{


    public class LoginModel
    {
        [Required]
        [StringLength(20)]
     

        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(20)]

        [DisplayName("Пароль")]


        public string Password { get; set; }
    }


    public sealed class MyDbInitializer : DropCreateDatabaseAlways<InfoVideoEntities>
    {
        private string[] addr_s = {"Багдановіча", "Кульман", "Вітаўта", "Альгерда"};
        private string[] city = { "Мінск, ", "Віцебск, ", "Гародня, ", "Магілёў, " };
        private string[] name = { "Алесь", "Генадзь", "Віталь", "Пятро","Раман","Марыя","Юлія","Зміцер" };
        private string[] em = { "@mail.ru", "@gmail.com", "@yandex.com" };
        private string[] ln = { "Прыгожы", "Лепшы", "Добры" };
        private string[] boxs = { "Дыск DVD", "Дыск BlueRay", "USB-Flash","Дыск mini-DVD" };

        Random r = new Random();

        protected override void Seed(InfoVideoEntities context)
        {

   


            var adminR = new Roles { Name = "Administrator" };
            var userR = new Roles { Name = "User" };

            context.Roles.Add(adminR);
            context.Roles.Add(userR);
                    
            var admin = new Users { Email = "asd@mail1.ru", FirstName = "Leon", LastName = "Budkouski", Password = "Asd123",Login = "Hienadz", Address = "Мінск, Кульман 33", Discount  = 10,Roles = adminR};
            var user = new Users { Email = "kross@mail.ru", FirstName = "Dylan", LastName = "Kross", Password = "Asd111", Login = "HienadzA", Address = "Мінск, Багдановіча М 100", Discount = 0, Roles = userR};

            for (int i = 0; i < 10; i++)
            {
                var ff = name[r.Next(0, 100)%name.Length];
                var c = new Users
                {
                   Address = city[r.Next(0,100)%city.Length]  + addr_s[r.Next(0, 100) % addr_s.Length] + " " + r.Next(0, 100),
                   Discount = (short?) r.Next(0,40),
                   FirstName = ff,
                   Email = ff + r.Next(80, 99) + em[r.Next(0, 100) % em.Length],
                   Password = ff + r.Next(0, 10000)          ,
                   Roles = userR,
                   LastName = ln[r.Next(0, 100) % ln.Length]      ,
                    Login =         ff +r.Next(0,100)

                };
                context.Users.Add(c);
            }

            context.Users.Add(admin);
            context.Users.Add(user);
       

     

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


            Video v2 = new Video
            {
                Title = "The Black List",
                Description = "An FBI fugitive, Raymond \"Red\" Reddington (Spader), joins the FBI in an attempt to work"+
                                " together and bring down criminals and terrorists. After their first victory, "+
                                "a list of dangerous but unknown criminals is revealed by Red. "+
                                    "With the help of a rookie FBI profiler, Red will work to eliminate all those in \"The List\"",
                Date = new DateTime(2014, 02, 01),
                Genre = "Шпіёнскі",
                Logo = "the-blacklist.jpg"

            };


            Video v3 = new Video
            {
                Title = "Chicago Fire",
                Description = "Chicago Fire is an American action-drama television series that airs on NBC and was created " +
                              "by Michael Brandt and Derek Haas with Dick Wolf serving as an executive producer. " +
                              "The series originally premiered on October 10, 2012. The show follows the lives of the firefighters" +
                              " and paramedics working at the Chicago Fire Department at the firehouse of Engine 51, Truck 81, Squad 3," +
                              " Ambulance 61 and Battalion 25. The pilot episode had an early release at NBC.com, " +
                              "before the series' premiere on television.",
                Date = new DateTime(2014, 06, 21),
                Genre = "Пажарныя",
                Logo = "fire-key-art.jpg"

            };


            context.Video.Add(v);
            context.Video.Add(v1);
            context.Video.Add(v2);
            context.Video.Add(v3);

            Format f= new Format {Languages = "Bel, En, De", Container = "mkv", Support3D = false};
            Format f1 = new Format { Languages = "Bel, En", Container = "mp4", Support3D = true };
            Format f2 = new Format { Languages = "En", Container = "3gp", Support3D = false };
            Format f3 = new Format { Languages = "De", Container = "avi", Support3D = true };

            context.Format.Add(f);
            context.Format.Add(f1);
            context.Format.Add(f2);
            context.Format.Add(f3);

            context.SaveChanges();

            for (int i = 0; i < 15; i++)
            {
                var ee = new Edition();

                ee.Box = boxs[r.Next(0, 100) % boxs.Length];
                ee.Price = r.Next(20, 800)*1000;
                ee.Video = context.Video.ToList().ElementAt(r.Next(0, context.Video.Count() ));
                ee.Format = context.Format.ToList().ElementAt(r.Next(0, context.Format.Count() ));
              

                context.Edition.Add(ee);
            }

            Edition e = new Edition {Box = "Діск BluRay", Format = f, Price = 130000, Video = v};
            Edition e1 = new Edition { Box = "Діск DVD", Format = f1, Price = 30000, Video = v1 };
            Edition e2 = new Edition { Box = "Діск BluRay", Format = f1, Price = 30000, Video = v };

            context.Edition.Add(e);
            context.Edition.Add(e1);
            context.Edition.Add(e2);

            History h = new History {Date = DateTime.Now, Edition = e,Users = admin};
            History h1 = new History { Date = DateTime.Today, Edition = e1, Users = user };

            context.History.Add(h);
            context.History.Add(h1);

            context.SaveChanges();

            DateTime start = new DateTime(2015, 1, 1);
            int range = (DateTime.Today - start).Days;

            for (int i = 0; i < 20; i++)
            {
                var hh = new History {

                       
                

                Date = start.AddDays(r.Next(range)),

                Edition = context.Edition.ToList().ElementAt(r.Next(0, context.Edition.Count())),

                    Users = context.Users.ToList().ElementAt(r.Next(0, context.Users.Count()))
                };

                context.History.Add(hh);

            }
        
            context.SaveChanges();
            base.Seed(context);
        }
    }
}