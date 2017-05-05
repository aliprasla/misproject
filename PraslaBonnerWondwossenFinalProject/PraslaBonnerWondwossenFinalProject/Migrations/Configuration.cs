namespace PraslaBonnerWondwossenFinalProject.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq.Expressions;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using PraslaBonnerWondwossenFinalProject.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<PraslaBonnerWondwossenFinalProject.Models.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PraslaBonnerWondwossenFinalProject.Models.AppDbContext db)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            UserManager<AppUser> userManager = new UserManager<AppUser>(new UserStore<AppUser>(db));
            AppRoleManager roleManager = new AppRoleManager(new RoleStore<AppRole>(db));
            var hasher = new PasswordHasher();
            String strEmail = "";
            //chunks
            String roleName = "Customer";
            strEmail = "cbaker@freezing.co.uk";
            AppUser user2 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Christopher",
                Middle = "L",
                LName = "Baker",
                Address = "1245 Lake Austin Blvd.",
                City = "Austin",
                State = "TX",
                Zip = 78733,
                PhoneNumber = "5125571146",
                Birthday = DateTime.Parse("2/7/1991"),
                PasswordHash = hasher.HashPassword("hello"),
                isActive = true
            };
            db.Users.AddOrUpdate(u => u.UserName, user2);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "mb@aool.com";
            AppUser user3 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Michelle",
                Middle = "",
                LName = "Banks",
                Address = "1300 Tall Pine Lane",
                City = "San Antonio",
                State = "TX",
                Zip = 78261,
                PhoneNumber = "2102678873",
                Birthday = DateTime.Parse("6/23/1990"),
                PasswordHash = hasher.HashPassword("banquet"),
                isActive = true
            };
            db.Users.AddOrUpdate(u => u.UserName, user3);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "fd@aool.com";
            AppUser user4 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Franco",
                Middle = "V",
                LName = "Broccolo",
                Address = "62 Browning Rd",
                City = "Houston",
                State = "TX",
                Zip = 77019,
                PhoneNumber = "8175659699",
                Birthday = DateTime.Parse("5/6/1986"),
                PasswordHash = hasher.HashPassword("666666"),
                isActive = true
            };
            db.Users.AddOrUpdate(u => u.UserName, user4);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "wendy@ggmail.com";
            AppUser user5 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Wendy",
                Middle = "L",
                LName = "Chang",
                Address = "202 Bellmont Hall",
                City = "Austin",
                State = "TX",
                Zip = 78713,
                PhoneNumber = "5125943222",
                Birthday = DateTime.Parse("12/21/1964"),
                PasswordHash = hasher.HashPassword("texas"),
                isActive = true
            };
            db.Users.AddOrUpdate(u => u.UserName, user5);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "limchou@yaho.com";
            AppUser user6 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Lim",
                Middle = "",
                LName = "Chou",
                Address = "1600 Teresa Lane",
                City = "San Antonio",
                State = "TX",
                Zip = 78266,
                PhoneNumber = "2107724599",
                Birthday = DateTime.Parse("6/14/1950"),
                PasswordHash = hasher.HashPassword("austin"),
                isActive = true
            };
            db.Users.AddOrUpdate(u => u.UserName, user6);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "Dixon@aool.com";
            AppUser user7 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Shan",
                Middle = "D",
                LName = "Dixon",
                Address = "234 Holston Circle",
                City = "Dallas",
                State = "TX",
                Zip = 75208,
                PhoneNumber = "2142643255",
                Birthday = DateTime.Parse("5/9/1930"),
                PasswordHash = hasher.HashPassword("mailbox"),
                isActive = true
            };
            db.Users.AddOrUpdate(u => u.UserName, user7);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "louann@ggmail.com";
            AppUser user8 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Lou Ann",
                Middle = "K",
                LName = "Feeley",
                Address = "600 S 8th Street W",
                City = "Houston",
                State = "TX",
                Zip = 77010,
                PhoneNumber = "8172556749",
                Birthday = DateTime.Parse("2/24/1930"),
                PasswordHash = hasher.HashPassword("aggies"),
                isActive = true
            };
            db.Users.AddOrUpdate(u => u.UserName, user8);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "tfreeley@minntonka.ci.state.mn.us";
            AppUser user9 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Tesa",
                Middle = "P",
                LName = "Freeley",
                Address = "4448 Fairview Ave.",
                City = "Houston",
                State = "TX",
                Zip = 77009,
                PhoneNumber = "8173255687",
                Birthday = DateTime.Parse("9/1/1935"),
                PasswordHash = hasher.HashPassword("raiders"),
                isActive = true
            };
            db.Users.AddOrUpdate(u => u.UserName, user9);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "mgar@aool.com";
            AppUser user10 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Margaret",
                Middle = "L",
                LName = "Garcia",
                Address = "594 Longview",
                City = "Houston",
                State = "TX",
                Zip = 77003,
                PhoneNumber = "8176593544",
                Birthday = DateTime.Parse("7/3/1990"),
                PasswordHash = hasher.HashPassword("mustangs"),
                isActive = true
            };
            db.Users.AddOrUpdate(u => u.UserName, user10);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "chaley@thug.com";
            AppUser user11 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Charles",
                Middle = "E",
                LName = "Haley",
                Address = "One Cowboy Pkwy",
                City = "Dallas",
                State = "TX",
                Zip = 75261,
                PhoneNumber = "2148475583",
                Birthday = DateTime.Parse("9/17/1985"),
                PasswordHash = hasher.HashPassword("mydog"),
                isActive = true
            };
            db.Users.AddOrUpdate(u => u.UserName, user11);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "jeff@ggmail.com";
            AppUser user12 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Jeffrey",
                Middle = "T",
                LName = "Hampton",
                Address = "337 38th St.",
                City = "Austin",
                State = "TX",
                Zip = 78705,
                PhoneNumber = "5126978613",
                Birthday = DateTime.Parse("1/23/1995"),
                PasswordHash = hasher.HashPassword("jeffh"),
                isActive = true
            };
            db.Users.AddOrUpdate(u => u.UserName, user12);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "wjhearniii@umch.edu";
            AppUser user13 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "John",
                Middle = "B",
                LName = "Hearn",
                Address = "4225 North First",
                City = "Dallas",
                State = "TX",
                Zip = 75237,
                PhoneNumber = "2148965621",
                Birthday = DateTime.Parse("1/8/1994"),
                PasswordHash = hasher.HashPassword("logicon"),
                isActive = true
            };
            db.Users.AddOrUpdate(u => u.UserName, user13);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "hicks43@ggmail.com";
            AppUser user14 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Anthony",
                Middle = "J",
                LName = "Hicks",
                Address = "32 NE Garden Ln., Ste 910",
                City = "San Antonio",
                State = "TX",
                Zip = 78239,
                PhoneNumber = "2105788965",
                Birthday = DateTime.Parse("10/6/1990"),
                PasswordHash = hasher.HashPassword("doofus"),
                isActive = true
            };
            db.Users.AddOrUpdate(u => u.UserName, user14);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "bradsingram@mall.utexas.edu";
            AppUser user15 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Brad",
                Middle = "S",
                LName = "Ingram",
                Address = "6548 La Posada Ct.",
                City = "Austin",
                State = "TX",
                Zip = 78736,
                PhoneNumber = "5124678821",
                Birthday = DateTime.Parse("4/12/1984"),
                PasswordHash = hasher.HashPassword("mother"),
                isActive = true
            };
            db.Users.AddOrUpdate(u => u.UserName, user15);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "mother.Ingram@aool.com";
            AppUser user16 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Todd",
                Middle = "L",
                LName = "Jacobs",
                Address = "4564 Elm St.",
                City = "Austin",
                State = "TX",
                Zip = 78731,
                PhoneNumber = "5124653365",
                Birthday = DateTime.Parse("4/4/1983"),
                PasswordHash = hasher.HashPassword("whimsical"),
                isActive = true
            };
            db.Users.AddOrUpdate(u => u.UserName, user16);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "victoria@aool.com";
            AppUser user17 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Victoria",
                Middle = "M",
                LName = "Lawrence",
                Address = "6639 Butterfly Ln.",
                City = "Austin",
                State = "TX",
                Zip = 78761,
                PhoneNumber = "5129457399",
                Birthday = DateTime.Parse("2/3/1961"),
                PasswordHash = hasher.HashPassword("nothing"),
                isActive = true
            };
            db.Users.AddOrUpdate(u => u.UserName, user17);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "lineback@flush.net";
            AppUser user18 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Erik",
                Middle = "W",
                LName = "Lineback",
                Address = "1300 Netherland St",
                City = "San Antonio",
                State = "TX",
                Zip = 78293,
                PhoneNumber = "2102449976",
                Birthday = DateTime.Parse("9/3/1946"),
                PasswordHash = hasher.HashPassword("GoodFellow"),
                isActive = true
            };
            db.Users.AddOrUpdate(u => u.UserName, user18);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "elowe@netscrape.net";
            AppUser user19 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Ernest",
                Middle = "S",
                LName = "Lowe",
                Address = "3201 Pine Drive",
                City = "San Antonio",
                State = "TX",
                Zip = 78279,
                PhoneNumber = "2105344627",
                Birthday = DateTime.Parse("2/7/1992"),
                PasswordHash = hasher.HashPassword("Elbow"),
                isActive = true
            };
            db.Users.AddOrUpdate(u => u.UserName, user19);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "luce_chuck@ggmail.com";
            AppUser user20 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Chuck",
                Middle = "B",
                LName = "Luce",
                Address = "2345 Rolling Clouds",
                City = "San Antonio",
                State = "TX",
                Zip = 78268,
                PhoneNumber = "2106983548",
                Birthday = DateTime.Parse("10/25/1942"),
                PasswordHash = hasher.HashPassword("LuceyDucey"),
                isActive = true
            };
            db.Users.AddOrUpdate(u => u.UserName, user20);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "mackcloud@pimpdaddy.com";
            AppUser user21 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Jennifer",
                Middle = "D",
                LName = "MacLeod",
                Address = "2504 Far West Blvd.",
                City = "Austin",
                State = "TX",
                Zip = 78731,
                PhoneNumber = "5124748138",
                Birthday = DateTime.Parse("8/6/1965"),
                PasswordHash = hasher.HashPassword("cloudyday"),
                isActive = true
            };
            db.Users.AddOrUpdate(u => u.UserName, user21);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "liz@ggmail.com";
            AppUser user22 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Elizabeth",
                Middle = "P",
                LName = "Markham",
                Address = "7861 Chevy Chase",
                City = "Austin",
                State = "TX",
                Zip = 78732,
                PhoneNumber = "5124579845",
                Birthday = DateTime.Parse("4/13/1959"),
                PasswordHash = hasher.HashPassword("emarkbark"),
                isActive = true
            };
            db.Users.AddOrUpdate(u => u.UserName, user22);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "mclarence@aool.com";
            AppUser user23 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Clarence",
                Middle = "A",
                LName = "Martin",
                Address = "87 Alcedo St.",
                City = "Houston",
                State = "TX",
                Zip = 77045,
                PhoneNumber = "8174955201",
                Birthday = DateTime.Parse("1/6/1990"),
                PasswordHash = hasher.HashPassword("smartinmartin"),
                isActive = true
            };
            db.Users.AddOrUpdate(u => u.UserName, user23);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "smartinmartin.Martin@aool.com";
            AppUser user24 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Gregory",
                Middle = "R",
                LName = "Martinez",
                Address = "8295 Sunset Blvd.",
                City = "Houston",
                State = "TX",
                Zip = 77030,
                PhoneNumber = "8178746718",
                Birthday = DateTime.Parse("10/9/1987"),
                PasswordHash = hasher.HashPassword("grego"),
                isActive = true
            };
            db.Users.AddOrUpdate(u => u.UserName, user24);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "cmiller@mapster.com";
            AppUser user25 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Charles",
                Middle = "R",
                LName = "Miller",
                Address = "8962 Main St.",
                City = "Houston",
                State = "TX",
                Zip = 77031,
                PhoneNumber = "8177458615",
                Birthday = DateTime.Parse("7/21/1984"),
                PasswordHash = hasher.HashPassword("chucky33"),
                isActive = true
            };
            db.Users.AddOrUpdate(u => u.UserName, user25);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "nelson.Kelly@aool.com";
            AppUser user26 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Kelly",
                Middle = "T",
                LName = "Nelson",
                Address = "2601 Red River",
                City = "Austin",
                State = "TX",
                Zip = 78703,
                PhoneNumber = "5122926966",
                Birthday = DateTime.Parse("7/4/1956"),
                PasswordHash = hasher.HashPassword("orange"),
                isActive = true
            };
            db.Users.AddOrUpdate(u => u.UserName, user26);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "jojoe@ggmail.com";
            AppUser user27 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Joe",
                Middle = "C",
                LName = "Nguyen",
                Address = "1249 4th SW St.",
                City = "Dallas",
                State = "TX",
                Zip = 75238,
                PhoneNumber = "2143125897",
                Birthday = DateTime.Parse("1/29/1963"),
                PasswordHash = hasher.HashPassword("victorious"),
                isActive = true
            };
            db.Users.AddOrUpdate(u => u.UserName, user27);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "orielly@foxnets.com";
            AppUser user28 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Bill",
                Middle = "T",
                LName = "O'Reilly",
                Address = "8800 Gringo Drive",
                City = "San Antonio",
                State = "TX",
                Zip = 78260,
                PhoneNumber = "2103450925",
                Birthday = DateTime.Parse("1/7/1983"),
                PasswordHash = hasher.HashPassword("billyboy"),
                isActive = true
            };
            db.Users.AddOrUpdate(u => u.UserName, user28);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "or@aool.com";
            AppUser user29 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Anka",
                Middle = "L",
                LName = "Radkovich",
                Address = "1300 Elliott Pl",
                City = "Dallas",
                State = "TX",
                Zip = 75260,
                PhoneNumber = "2142345566",
                Birthday = DateTime.Parse("3/31/1980"),
                PasswordHash = hasher.HashPassword("radicalone"),
                isActive = true
            };
            db.Users.AddOrUpdate(u => u.UserName, user29);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "megrhodes@freezing.co.uk";
            AppUser user30 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Megan",
                Middle = "C",
                LName = "Rhodes",
                Address = "4587 Enfield Rd.",
                City = "Austin",
                State = "TX",
                Zip = 78707,
                PhoneNumber = "5123744746",
                Birthday = DateTime.Parse("8/12/1944"),
                PasswordHash = hasher.HashPassword("gohorns"),
                isActive = true
            };
            db.Users.AddOrUpdate(u => u.UserName, user30);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "erynrice@aool.com";
            AppUser user31 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Eryn",
                Middle = "M",
                LName = "Rice",
                Address = "3405 Rio Grande",
                City = "Austin",
                State = "TX",
                Zip = 78705,
                PhoneNumber = "5123876657",
                Birthday = DateTime.Parse("8/2/1934"),
                PasswordHash = hasher.HashPassword("iloveme"),
                isActive = true
            };
            db.Users.AddOrUpdate(u => u.UserName, user31);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "jorge@hootmail.com";
            AppUser user32 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Jorge",
                Middle = "",
                LName = "Rodriguez",
                Address = "6788 Cotter Street",
                City = "Houston",
                State = "TX",
                Zip = 77057,
                PhoneNumber = "8178904374",
                Birthday = DateTime.Parse("8/11/1989"),
                PasswordHash = hasher.HashPassword("greedy"),
                isActive = true
            };
            db.Users.AddOrUpdate(u => u.UserName, user32);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "ra@aoo.com";
            AppUser user33 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Allen",
                Middle = "B",
                LName = "Rogers",
                Address = "4965 Oak Hill",
                City = "Austin",
                State = "TX",
                Zip = 78732,
                PhoneNumber = "5128752943",
                Birthday = DateTime.Parse("8/27/1967"),
                isActive = true,
                PasswordHash = hasher.HashPassword("familiar")
            };
            db.Users.AddOrUpdate(u => u.UserName, user33);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "st-jean@home.com";
            AppUser user34 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Olivier",
                Middle = "M",
                LName = "Saint-Jean",
                Address = "255 Toncray Dr.",
                City = "San Antonio",
                State = "TX",
                Zip = 78292,
                PhoneNumber = "2104145678",
                Birthday = DateTime.Parse("7/8/1950"),
                PasswordHash = hasher.HashPassword("historical"),
                isActive = true
            };
            db.Users.AddOrUpdate(u => u.UserName, user34);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "ss34@ggmail.com";
            AppUser user35 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Sarah",
                Middle = "J",
                LName = "Saunders",
                Address = "332 Avenue C",
                City = "Austin",
                State = "TX",
                Zip = 78705,
                PhoneNumber = "5123497810",
                Birthday = DateTime.Parse("10/29/1977"),
                PasswordHash = hasher.HashPassword("guiltless"),
                isActive = true
            };
            db.Users.AddOrUpdate(u => u.UserName, user35);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "willsheff@email.com";
            AppUser user36 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "William",
                Middle = "T",
                LName = "Sewell",
                Address = "2365 51st St.",
                City = "Austin",
                State = "TX",
                Zip = 78709,
                PhoneNumber = "5124510084",
                Birthday = DateTime.Parse("4/21/1941"),
                PasswordHash = hasher.HashPassword("frequent"),
                isActive = true
            };
            db.Users.AddOrUpdate(u => u.UserName, user36);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "sheff44@ggmail.com";
            AppUser user37 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Martin",
                Middle = "J",
                LName = "Sheffield",
                Address = "3886 Avenue A",
                City = "Austin",
                State = "TX",
                Zip = 78705,
                PhoneNumber = "5125479167",
                Birthday = DateTime.Parse("11/10/1937"),
                PasswordHash = hasher.HashPassword("history"),
                isActive = true
            };
            db.Users.AddOrUpdate(u => u.UserName, user37);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "johnsmith187@aool.com";
            AppUser user38 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "John",
                Middle = "A",
                LName = "Smith",
                Address = "23 Hidden Forge Dr.",
                City = "San Antonio",
                State = "TX",
                Zip = 78280,
                PhoneNumber = "2108321888",
                Birthday = DateTime.Parse("10/26/1954"),
                PasswordHash = hasher.HashPassword("squirrel"),
                isActive = true
            };
            db.Users.AddOrUpdate(u => u.UserName, user38);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "dustroud@mail.com";
            AppUser user39 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Dustin",
                Middle = "P",
                LName = "Stroud",
                Address = "1212 Rita Rd",
                City = "Dallas",
                State = "TX",
                Zip = 75221,
                PhoneNumber = "2142346667",
                Birthday = DateTime.Parse("9/1/1932"),
                PasswordHash = hasher.HashPassword("snakes"),
                isActive = true
            };
            db.Users.AddOrUpdate(u => u.UserName, user39);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "ericstuart@aool.com";
            AppUser user40 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Eric",
                Middle = "D",
                LName = "Stuart",
                Address = "5576 Toro Ring",
                City = "Austin",
                State = "TX",
                Zip = 78746,
                PhoneNumber = "5128178335",
                Birthday = DateTime.Parse("12/28/1930"),
                PasswordHash = hasher.HashPassword("loaf"),
                isActive = true
            };
            db.Users.AddOrUpdate(u => u.UserName, user40);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "peterstump@hootmail.com";
            AppUser user41 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Peter",
                Middle = "L",
                LName = "Stump",
                Address = "1300 Kellen Circle",
                City = "Houston",
                State = "TX",
                Zip = 77018,
                PhoneNumber = "8174560903",
                Birthday = DateTime.Parse("8/13/1989"),
                PasswordHash = hasher.HashPassword("rhythm"),
                isActive = true
            };
            db.Users.AddOrUpdate(u => u.UserName, user41);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "tanner@ggmail.com";
            AppUser user42 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Jeremy",
                Middle = "S",
                LName = "Tanner",
                Address = "4347 Almstead",
                City = "Houston",
                State = "TX",
                Zip = 77044,
                PhoneNumber = "8174590929",
                Birthday = DateTime.Parse("5/21/1982"),
                PasswordHash = hasher.HashPassword("kindly"),
                isActive = true
            };
            db.Users.AddOrUpdate(u => u.UserName, user42);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "taylordjay@aool.com";
            AppUser user43 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Allison",
                Middle = "R",
                LName = "Taylor",
                Address = "467 Nueces St.",
                City = "Austin",
                State = "TX",
                Zip = 78705,
                PhoneNumber = "5124748452",
                Birthday = DateTime.Parse("1/8/1960"),
                PasswordHash = hasher.HashPassword("instrument"),
                isActive = true
            };
            db.Users.AddOrUpdate(u => u.UserName, user43);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "TayTaylor@aool.com";
            AppUser user44 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Rachel",
                Middle = "K",
                LName = "Taylor",
                Address = "345 Longview Dr.",
                City = "Austin",
                State = "TX",
                Zip = 78705,
                PhoneNumber = "5124512631",
                Birthday = DateTime.Parse("7/27/1975"),
                PasswordHash = hasher.HashPassword("deep"),
                isActive = true
            };
            db.Users.AddOrUpdate(u => u.UserName, user44);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "teefrank@hootmail.com";
            AppUser user45 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Frank",
                Middle = "J",
                LName = "Tee",
                Address = "5590 Lavell Dr",
                City = "Houston",
                State = "TX",
                Zip = 77004,
                PhoneNumber = "8178765543",
                Birthday = DateTime.Parse("4/6/1968"),
                PasswordHash = hasher.HashPassword("rest"),
                isActive = true
            };
            db.Users.AddOrUpdate(u => u.UserName, user45);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "tuck33@ggmail.com";
            AppUser user46 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Clent",
                Middle = "J",
                LName = "Tucker",
                Address = "312 Main St.",
                City = "Dallas",
                State = "TX",
                Zip = 75315,
                PhoneNumber = "2148471154",
                Birthday = DateTime.Parse("5/19/1978"),
                PasswordHash = hasher.HashPassword("approval"),
                isActive = true
            };
            db.Users.AddOrUpdate(u => u.UserName, user46);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "avelasco@yaho.com";
            AppUser user47 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Allen",
                Middle = "G",
                LName = "Velasco",
                Address = "679 W. 4th",
                City = "Dallas",
                State = "TX",
                Zip = 75207,
                PhoneNumber = "2143985638",
                Birthday = DateTime.Parse("10/6/1963"),
                PasswordHash = hasher.HashPassword("decorate"),
                isActive = true
            };
            db.Users.AddOrUpdate(u => u.UserName, user47);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "westj@pioneer.net";
            AppUser user48 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Jake",
                Middle = "T",
                LName = "West",
                Address = "RR 3287",
                City = "Dallas",
                State = "TX",
                Zip = 75323,
                PhoneNumber = "2148475244",
                Birthday = DateTime.Parse("10/14/1993"),
                PasswordHash = hasher.HashPassword("geese"),
                isActive = true
            };
            db.Users.AddOrUpdate(u => u.UserName, user48);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "louielouie@aool.com";
            AppUser user49 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Louis",
                Middle = "L",
                LName = "Winthorpe",
                Address = "2500 Padre Blvd",
                City = "Dallas",
                State = "TX",
                Zip = 75220,
                PhoneNumber = "2145650098",
                Birthday = DateTime.Parse("5/31/1952"),
                PasswordHash = hasher.HashPassword("sturdy"),
                isActive = true
            };
            db.Users.AddOrUpdate(u => u.UserName, user49);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "rwood@voyager.net";
            AppUser user50 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Reagan",
                Middle = "B",
                LName = "Wood",
                Address = "447 Westlake Dr.",
                City = "Austin",
                State = "TX",
                Zip = 78746,
                PhoneNumber = "5124545242",
                Birthday = DateTime.Parse("4/24/1992"),
                PasswordHash = hasher.HashPassword("decorous"),
                isActive = true
            };
            db.Users.AddOrUpdate(u => u.UserName, user50);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }

            strEmail = "t.jacobs@longhornbank.neet";
            roleName = "Employee";
            AppUser user52 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Todd",
                Middle = "L",
                LName = "Jacobs",
                Address = "4564 Elm St.",
                City = "Houston",
                State = "TX",
                Zip = 77003,
                PhoneNumber = "8176593544",
                SSN = "222222222",
                PasswordHash = hasher.HashPassword("society")
            };
            db.Users.AddOrUpdate(u => u.UserName, user52);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "e.rice@longhornbank.neet";
            roleName = "Employee";
            AppUser user53 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Eryn",
                Middle = "M",
                LName = "Rice",
                Address = "3405 Rio Grande",
                City = "Dallas",
                State = "TX",
                Zip = 75261,
                PhoneNumber = "2148475583",
                SSN = "111111111",
                PasswordHash = hasher.HashPassword("ricearoni")
            };
            db.Users.AddOrUpdate(u => u.UserName, user53);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "b.ingram@longhornbank.neet";
            roleName = "Employee";
            AppUser user54 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Brad",
                Middle = "S",
                LName = "Ingram",
                Address = "6548 La Posada Ct.",
                City = "Austin",
                State = "TX",
                Zip = 78705,
                PhoneNumber = "5126978613",
                SSN = "545454545",
                PasswordHash = hasher.HashPassword("ingram45")
            };
            db.Users.AddOrUpdate(u => u.UserName, user54);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "a.taylor@longhornbank.neet";
            roleName = "Manager";
            AppUser user55 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Allison",
                Middle = "R",
                LName = "Taylor",
                Address = "467 Nueces St.",
                City = "Dallas",
                State = "TX",
                Zip = 75237,
                PhoneNumber = "2148965621",
                SSN = "645889563",
                PasswordHash = hasher.HashPassword("nostalgic")
            };
            db.Users.AddOrUpdate(u => u.UserName, user55);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "g.martinez@longhornbank.neet";
            roleName = "Employee";
            AppUser user56 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Gregory",
                Middle = "R",
                LName = "Martinez",
                Address = "8295 Sunset Blvd.",
                City = "San Antonio",
                State = "TX",
                Zip = 78239,
                PhoneNumber = "2105788965",
                SSN = "574677829",
                PasswordHash = hasher.HashPassword("marty")
            };
            db.Users.AddOrUpdate(u => u.UserName, user56);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "m.sheffield@longhornbank.neet";
            roleName = "Manager";
            AppUser user57 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Martin",
                Middle = "J",
                LName = "Sheffield",
                Address = "3886 Avenue A",
                City = "Austin",
                State = "TX",
                Zip = 78736,
                PhoneNumber = "5124678821",
                SSN = "334557278",
                PasswordHash = hasher.HashPassword("longhorns")
            };
            db.Users.AddOrUpdate(u => u.UserName, user57);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "j.macleod@longhornbank.neet";
            roleName = "Manager";
            AppUser user58 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Jennifer",
                Middle = "D",
                LName = "MacLeod",
                Address = "2504 Far West Blvd.",
                City = "Austin",
                State = "TX",
                Zip = 78731,
                PhoneNumber = "5124653365",
                SSN = "886719249",
                PasswordHash = hasher.HashPassword("smitty")
            };
            db.Users.AddOrUpdate(u => u.UserName, user58);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "j.tanner@longhornbank.neet";
            roleName = "Employee";
            AppUser user59 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Jeremy",
                Middle = "S",
                LName = "Tanner",
                Address = "4347 Almstead",
                City = "Austin",
                State = "TX",
                Zip = 78761,
                PhoneNumber = "5129457399",
                SSN = "888887878",
                PasswordHash = hasher.HashPassword("tanman")
            };
            db.Users.AddOrUpdate(u => u.UserName, user59);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "m.rhodes@longhornbank.neet";
            roleName = "Manager";
            AppUser user60 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Megan",
                Middle = "C",
                LName = "Rhodes",
                Address = "4587 Enfield Rd.",
                City = "San Antonio",
                State = "TX",
                Zip = 78293,
                PhoneNumber = "2102449976",
                SSN = "999990909",
                PasswordHash = hasher.HashPassword("countryrhodes")
            };
            db.Users.AddOrUpdate(u => u.UserName, user60);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "e.stuart@longhornbank.neet";
            roleName = "Manager";
            AppUser user61 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Eric",
                Middle = "F",
                LName = "Stuart",
                Address = "5576 Toro Ring",
                City = "San Antonio",
                State = "TX",
                Zip = 78279,
                PhoneNumber = "2105344627",
                SSN = "212121212",
                PasswordHash = hasher.HashPassword("stewboy")
            };
            db.Users.AddOrUpdate(u => u.UserName, user61);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "l.chung@longhornbank.neet";
            roleName = "Employee";
            AppUser user62 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Lisa",
                Middle = "N",
                LName = "Chung",
                Address = "234 RR 12",
                City = "San Antonio",
                State = "TX",
                Zip = 78268,
                PhoneNumber = "2106983548",
                SSN = "333333333",
                PasswordHash = hasher.HashPassword("lisssa")
            };
            db.Users.AddOrUpdate(u => u.UserName, user62);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "l.swanson@longhornbank.neet";
            roleName = "Manager";
            AppUser user63 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Leon",
                Middle = "",
                LName = "Swanson",
                Address = "245 River Rd",
                City = "Austin",
                State = "TX",
                Zip = 78731,
                PhoneNumber = "5124748138",
                SSN = "444444444",
                PasswordHash = hasher.HashPassword("swansong")
            };
            db.Users.AddOrUpdate(u => u.UserName, user63);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "w.loter@longhornbank.neet";
            roleName = "Employee";
            AppUser user64 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Wanda",
                Middle = "K",
                LName = "Loter",
                Address = "3453 RR 3235",
                City = "Austin",
                State = "TX",
                Zip = 78732,
                PhoneNumber = "5124579845",
                SSN = "555555555",
                PasswordHash = hasher.HashPassword("lottery")
            };
            db.Users.AddOrUpdate(u => u.UserName, user64);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "j.white@longhornbank.neet";
            roleName = "Manager";
            AppUser user65 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Jason",
                Middle = "M",
                LName = "White",
                Address = "12 Valley View",
                City = "Houston",
                State = "TX",
                Zip = 77045,
                PhoneNumber = "8174955201",
                SSN = "666666666",
                PasswordHash = hasher.HashPassword("evanescent")
            };
            db.Users.AddOrUpdate(u => u.UserName, user65);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "w.montgomery@longhornbank.neet";
            roleName = "Manager";
            AppUser user66 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Wilda",
                Middle = "K",
                LName = "Montgomery",
                Address = "210 Blanco Dr",
                City = "Houston",
                State = "TX",
                Zip = 77030,
                PhoneNumber = "8178746718",
                SSN = "676767676",
                PasswordHash = hasher.HashPassword("monty3")
            };
            db.Users.AddOrUpdate(u => u.UserName, user66);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "h.morales@longhornbank.neet";
            roleName = "Employee";
            AppUser user67 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Hector",
                Middle = "N",
                LName = "Morales",
                Address = "4501 RR 140",
                City = "Houston",
                State = "TX",
                Zip = 77031,
                PhoneNumber = "8177458615",
                SSN = "898989898",
                PasswordHash = hasher.HashPassword("hecktour")
            };
            db.Users.AddOrUpdate(u => u.UserName, user67);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "m.rankin@longhornbank.neet";
            roleName = "Employee";
            AppUser user68 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Mary",
                Middle = "T",
                LName = "Rankin",
                Address = "340 Second St",
                City = "Austin",
                State = "TX",
                Zip = 78703,
                PhoneNumber = "5122926966",
                SSN = "999888777",
                PasswordHash = hasher.HashPassword("rankmary")
            };
            db.Users.AddOrUpdate(u => u.UserName, user68);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "l.walker@longhornbank.neet";
            roleName = "Manager";
            AppUser user69 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Larry",
                Middle = "G",
                LName = "Walker",
                Address = "9 Bison Circle",
                City = "Dallas",
                State = "TX",
                Zip = 75238,
                PhoneNumber = "2143125897",
                SSN = "323232323",
                PasswordHash = hasher.HashPassword("walkamile")
            };
            db.Users.AddOrUpdate(u => u.UserName, user69);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "g.chang@longhornbank.neet";
            roleName = "Manager";
            AppUser user70 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "George",
                Middle = "M",
                LName = "Chang",
                Address = "9003 Joshua St",
                City = "San Antonio",
                State = "TX",
                Zip = 78260,
                PhoneNumber = "2103450925",
                SSN = "111222233",
                PasswordHash = hasher.HashPassword("changalang")
            };
            db.Users.AddOrUpdate(u => u.UserName, user70);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }
            strEmail = "g.gonzalez@longhornbank.neet";
            roleName = "Employee";
            AppUser user71 = new AppUser()
            {
                UserName = strEmail,
                Email = strEmail,
                FName = "Gwen",
                Middle = "J",
                LName = "Gonzalez",
                Address = "103 Manor Rd",
                City = "Dallas",
                State = "TX",
                Zip = 75260,
                PhoneNumber = "2142345566",
                SSN = "499551454",
                PasswordHash = hasher.HashPassword("offbeat")
            };
            db.Users.AddOrUpdate(u => u.UserName, user71);
            base.Seed(db);

            db.SaveChanges();
            if (!userManager.IsInRole(userManager.FindByEmail(strEmail).Id, roleName))
            {
                userManager.AddToRole(userManager.FindByEmail(strEmail).Id, roleName);
            }

            strEmail = "Dixon@aool.com";
            BankAccount Account2 = new BankAccount
            {
                Name = "Shan's Stock",
                Customer = userManager.FindByEmail(strEmail),
                Type = AccountTypes.Stock,
                Balance = 0m,
                AccountNumber = 1000000000
            };
            db.BankAccounts.AddOrUpdate(u => u.AccountNumber, Account2);
            base.Seed(db);
            db.SaveChanges();
            strEmail = "willsheff@email.com";
            BankAccount Account3 = new BankAccount
            {
                Name = "William's Savings",
                Customer = userManager.FindByEmail(strEmail),
                Type = AccountTypes.Savings,
                Balance = 40035.5m,
                AccountNumber = 1000000001
            };
            db.BankAccounts.AddOrUpdate(u => u.AccountNumber, Account3);
            base.Seed(db);
            db.SaveChanges();
            strEmail = "smartinmartin.Martin@aool.com";
            BankAccount Account4 = new BankAccount
            {
                Name = "Gregory's Checking",
                Customer = userManager.FindByEmail(strEmail),
                Type = AccountTypes.Checking,
                Balance = 39779.49m,
                AccountNumber = 1000000002
            };
            db.BankAccounts.AddOrUpdate(u => u.AccountNumber, Account4);
            base.Seed(db);
            db.SaveChanges();
            strEmail = "avelasco@yaho.com";
            BankAccount Account5 = new BankAccount
            {
                Name = "Allen's Checking",
                Customer = userManager.FindByEmail(strEmail),
                Type = AccountTypes.Checking,
                Balance = 47277.33m,
                AccountNumber = 1000000003
            };
            db.BankAccounts.AddOrUpdate(u => u.AccountNumber, Account5);
            base.Seed(db);
            db.SaveChanges();
            strEmail = "rwood@voyager.net";
            BankAccount Account6 = new BankAccount
            {
                Name = "Reagan's Checking",
                Customer = userManager.FindByEmail(strEmail),
                Type = AccountTypes.Checking,
                Balance = 70812.15m,
                AccountNumber = 1000000004
            };
            db.BankAccounts.AddOrUpdate(u => u.AccountNumber, Account6);
            base.Seed(db);
            db.SaveChanges();
            strEmail = "nelson.Kelly@aool.com";
            BankAccount Account7 = new BankAccount
            {
                Name = "Kelly's Savings",
                Customer = userManager.FindByEmail(strEmail),
                Type = AccountTypes.Savings,
                Balance = 21901.97m,
                AccountNumber = 1000000005
            };
            db.BankAccounts.AddOrUpdate(u => u.AccountNumber, Account7);
            base.Seed(db);
            db.SaveChanges();
            strEmail = "erynrice@aool.com";
            BankAccount Account8 = new BankAccount
            {
                Name = "Eryn's Checking",
                Customer = userManager.FindByEmail(strEmail),
                Type = AccountTypes.Checking,
                Balance = 70480.99m,
                AccountNumber = 1000000006
            };
            db.BankAccounts.AddOrUpdate(u => u.AccountNumber, Account8);
            base.Seed(db);
            db.SaveChanges();
            strEmail = "westj@pioneer.net";
            BankAccount Account9 = new BankAccount
            {
                Name = "Jake's Savings",
                Customer = userManager.FindByEmail(strEmail),
                Type = AccountTypes.Savings,
                Balance = 7916.4m,
                AccountNumber = 1000000007
            };
            db.BankAccounts.AddOrUpdate(u => u.AccountNumber, Account9);
            base.Seed(db);
            db.SaveChanges();
            strEmail = "mb@aool.com";
            BankAccount Account10 = new BankAccount
            {
                Name = "Michelle's Stock",
                Customer = userManager.FindByEmail(strEmail),
                Type = AccountTypes.Stock,
                Balance = 0m,
                AccountNumber = 1000000008
            };
            db.BankAccounts.AddOrUpdate(u => u.AccountNumber, Account10);
            base.Seed(db);
            db.SaveChanges();
            strEmail = "jeff@ggmail.com";
            BankAccount Account11 = new BankAccount
            {
                Name = "Jeffrey's Savings",
                Customer = userManager.FindByEmail(strEmail),
                Type = AccountTypes.Savings,
                Balance = 69576.83m,
                AccountNumber = 1000000009
            };
            db.BankAccounts.AddOrUpdate(u => u.AccountNumber, Account11);
            base.Seed(db);
            db.SaveChanges();
            strEmail = "nelson.Kelly@aool.com";
            BankAccount Account12 = new BankAccount
            {
                Name = "Kelly's Stock",
                Customer = userManager.FindByEmail(strEmail),
                Type = AccountTypes.Stock,
                Balance = 0m,
                AccountNumber = 1000000010
            };
            db.BankAccounts.AddOrUpdate(u => u.AccountNumber, Account12);
            base.Seed(db);
            db.SaveChanges();
            strEmail = "erynrice@aool.com";
            BankAccount Account13 = new BankAccount
            {
                Name = "Eryn's Checking 2",
                Customer = userManager.FindByEmail(strEmail),
                Type = AccountTypes.Checking,
                Balance = 30279.33m,
                AccountNumber = 1000000011
            };
            db.BankAccounts.AddOrUpdate(u => u.AccountNumber, Account13);
            base.Seed(db);
            db.SaveChanges();
            strEmail = "mackcloud@pimpdaddy.com";
            BankAccount Account14 = new BankAccount
            {
                Name = "Jennifer's IRA",
                Customer = userManager.FindByEmail(strEmail),
                Type = AccountTypes.IRA,
                Balance = 53177.21m,
                AccountNumber = 1000000012
            };
            db.BankAccounts.AddOrUpdate(u => u.AccountNumber, Account14);
            base.Seed(db);
            db.SaveChanges();
            strEmail = "ss34@ggmail.com";
            BankAccount Account15 = new BankAccount
            {
                Name = "Sarah's Savings",
                Customer = userManager.FindByEmail(strEmail),
                Type = AccountTypes.Savings,
                Balance = 11958.08m,
                AccountNumber = 1000000013
            };
            db.BankAccounts.AddOrUpdate(u => u.AccountNumber, Account15);
            base.Seed(db);
            db.SaveChanges();
            strEmail = "tanner@ggmail.com";
            BankAccount Account16 = new BankAccount
            {
                Name = "Jeremy's Savings",
                Customer = userManager.FindByEmail(strEmail),
                Type = AccountTypes.Savings,
                Balance = 72990.47m,
                AccountNumber = 1000000014
            };
            db.BankAccounts.AddOrUpdate(u => u.AccountNumber, Account16);
            base.Seed(db);
            db.SaveChanges();
            strEmail = "liz@ggmail.com";
            BankAccount Account17 = new BankAccount
            {
                Name = "Elizabeth's Savings",
                Customer = userManager.FindByEmail(strEmail),
                Type = AccountTypes.Savings,
                Balance = 7417.2m,
                AccountNumber = 1000000015
            };
            db.BankAccounts.AddOrUpdate(u => u.AccountNumber, Account17);
            base.Seed(db);
            db.SaveChanges();
            strEmail = "ra@aoo.com";
            BankAccount Account18 = new BankAccount
            {
                Name = "Allen's IRA",
                Customer = userManager.FindByEmail(strEmail),
                Type = AccountTypes.IRA,
                Balance = 75866.69m,
                AccountNumber = 1000000016
            };
            db.BankAccounts.AddOrUpdate(u => u.AccountNumber, Account18);
            base.Seed(db);
            db.SaveChanges();
            strEmail = "johnsmith187@aool.com";
            BankAccount Account19 = new BankAccount
            {
                Name = "John's Stock",
                Customer = userManager.FindByEmail(strEmail),
                Type = AccountTypes.Stock,
                Balance = 0m,
                AccountNumber = 1000000017
            };
            db.BankAccounts.AddOrUpdate(u => u.AccountNumber, Account19);
            base.Seed(db);
            db.SaveChanges();
            strEmail = "mclarence@aool.com";
            BankAccount Account20 = new BankAccount
            {
                Name = "Clarence's Savings",
                Customer = userManager.FindByEmail(strEmail),
                Type = AccountTypes.Savings,
                Balance = 1642.82m,
                AccountNumber = 1000000018
            };
            db.BankAccounts.AddOrUpdate(u => u.AccountNumber, Account20);
            base.Seed(db);
            db.SaveChanges();
            strEmail = "ss34@ggmail.com";
            BankAccount Account21 = new BankAccount
            {
                Name = "Sarah's Checking",
                Customer = userManager.FindByEmail(strEmail),
                Type = AccountTypes.Checking,
                Balance = 84421.45m,
                AccountNumber = 1000000019
            };
            db.BankAccounts.AddOrUpdate(u => u.AccountNumber, Account21);
            base.Seed(db);
            db.SaveChanges();

            Payee payee2 = new Payee()
            {
                Name = "City of Austin Water",
                Type = PayeeTypes.Utilities,
                Address = "113 South Congress Ave.",
                City = "Austin",
                State = "TX",
                ZipCode = 78710,
                PhoneNumber = "5126645558"
            };
            db.Payees.AddOrUpdate(u => u.PayeeID, payee2);
            base.Seed(db);
            db.SaveChanges();
            Payee payee3 = new Payee()
            {
                Name = "Reliant Energy",
                Type = PayeeTypes.Utilities,
                Address = "3500 E. Interstate 10",
                City = "Houston",
                State = "TX",
                ZipCode = 77099,
                PhoneNumber = "7135546697"
            };
            db.Payees.AddOrUpdate(u => u.PayeeID, payee3);
            base.Seed(db);
            db.SaveChanges();
            Payee payee4 = new Payee()
            {
                Name = "Lee Properties",
                Type = PayeeTypes.Rent,
                Address = "2500 Salado",
                City = "Austin",
                State = "TX",
                ZipCode = 78705,
                PhoneNumber = "5124453312"
            };
            db.Payees.AddOrUpdate(u => u.PayeeID, payee4);
            base.Seed(db);
            db.SaveChanges();
            Payee payee5 = new Payee()
            {
                Name = "Capital One",
                Type = PayeeTypes.CreditCard,
                Address = "1299 Fargo Blvd.",
                City = "Cheyenne",
                State = "WY",
                ZipCode = 82001,
                PhoneNumber = "5302215542"
            };
            db.Payees.AddOrUpdate(u => u.PayeeID, payee5);
            base.Seed(db);
            db.SaveChanges();
            Payee payee6 = new Payee()
            {
                Name = "Vanguard Title",
                Type = PayeeTypes.Mortgage,
                Address = "10976 Interstate 35 South",
                City = "Austin",
                State = "TX",
                ZipCode = 78745,
                PhoneNumber = "5128654951"
            };
            db.Payees.AddOrUpdate(u => u.PayeeID, payee6);
            base.Seed(db);
            db.SaveChanges();
            Payee payee7 = new Payee()
            {
                Name = "Lawn Care of Texas",
                Type = PayeeTypes.Other,
                Address = "4473 W. 3rd Street",
                City = "Austin",
                State = "TX",
                ZipCode = 78712,
                PhoneNumber = "5123365247"
            };
            db.Payees.AddOrUpdate(u => u.PayeeID, payee7);
            base.Seed(db);
            db.SaveChanges();


        }
    }
}
