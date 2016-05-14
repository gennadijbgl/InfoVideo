﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

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


    public sealed class MyDbInitializer : DropCreateDatabaseAlways<InfoVideoContext>
    {
       
    
       

        protected override void Seed(InfoVideoContext context)
        {
            var adminR = new Roles { Name = "Administrator" };
            var userR = new Roles { Name = "User" };

            context.Roles.Add(adminR);
            context.Roles.Add(userR);
                    
            var admin = new Users { Email = "asd@mail1.ru", FirstName = "Leon", LastName = "Budkouski", Password = "Asd123",Login = "Hienadz", Address = "Мінск, Кульман 33", Discount  = 10,Roles = adminR};
            var user = new Users { Email = "kross@mail.ru", FirstName = "Dylan", LastName = "Kross", Password = "Asd111", Login = "HienadzA", Address = "Мінск, Багдановіча М 100", Discount = 0, Roles = userR};

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

            context.Video.Add(v);
            context.Video.Add(v1);

    

            Format f= new Format {Languages = "Bel, Eng, Deu", Container = "mkv", Support3D = true};
            Format f1 = new Format { Languages = "Bel, Eng", Container = "mp4", Support3D = true };


            context.Format.Add(f);
            context.Format.Add(f1);

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
            base.Seed(context);
        }
    }
}