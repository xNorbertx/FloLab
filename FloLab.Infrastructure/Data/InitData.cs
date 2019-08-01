using FloLab.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloLab.Infrastructure.Data
{
    public class InitData
    {
        public static async Task Init(RoleManager<IdentityRole> roleManager, UserManager<User> userManager, FloLabContext context)
        {
            await InitializeRoles(roleManager);
            var adminTask = await InitializeAdmin(userManager);
            var userTask = await InitializeUser(userManager);

            InitializeSpace(context);
            await InitializeSubscriptions(context, userTask, userManager);
            InitializeBookings(context, userTask);
            InitializeAttendance(context, userTask);            
        }

        private static async Task InitializeRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                var admin = new Role
                {
                    Name = "Admin"
                };

                await roleManager.CreateAsync(admin);
            }

            if (!await roleManager.RoleExistsAsync("User"))
            {
                var user = new Role
                {
                    Name = "User"
                };

                await roleManager.CreateAsync(user);
            }
        }

        private static async Task<User> InitializeAdmin(UserManager<User> userManager)
        {
            string emailAdmin = "admin@flolab.nl";
            User admin = await userManager.FindByEmailAsync(emailAdmin);

            if (admin == null)
            {
                var adminZhana = new User()
                {
                    UserName = $"U{emailAdmin}",
                    Name = "Zhana",
                    Email = emailAdmin,
                    EmailConfirmed = true

                };
                IdentityResult res = await userManager.CreateAsync(adminZhana, "LiveLoveLaugh123!");
                if (res.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminZhana, "Admin");
                }
                return adminZhana;
            }
            return admin;
        }

        private static async Task<User> InitializeUser(UserManager<User> userManager)
        {
            string userEmail = "norbert.g.e.bakker@gmail.com";
            User user = await userManager.FindByEmailAsync(userEmail);

            if (user == null)
            {
                var userNorbert = new User()
                {
                    UserName = $"U{userEmail}",
                    Name = "Norbert",
                    Email = userEmail,
                    EmailConfirmed = true
                };
                IdentityResult res = await userManager.CreateAsync(userNorbert, "Telefoon06!");
                if (res.Succeeded)
                {
                    await userManager.AddToRoleAsync(userNorbert, "User");
                }

                return userNorbert;
            }
            return user;
        }

        private static void InitializeSpace(FloLabContext context)
        {
            if (context.Spaces.Where(x => x.Name.Equals("Sun Room")).Count() == 0)
            {
                Space sunroom = new Space
                {
                    Name = "Sun Room"
                };

                context.Spaces.Add(sunroom);
                context.SaveChanges();
            }
        }

        private static async Task InitializeSubscriptions(FloLabContext context, User user, UserManager<User> userManager)
        {
            //Add different subscriptions
            if (context.Subscriptions.Where(x => x.Name == "Plan A").Count() == 0 &&
                context.Subscriptions.Where(x => x.Name == "Plan B").Count() == 0 &&
                context.Subscriptions.Where(x => x.Name == "Plan C").Count() == 0)
            {
                Subscription OneDaySubscription = new Subscription()
                {
                    Name = "Plan A",
                };

                Subscription ThreeDaySubscription = new Subscription()
                {
                    Name = "Plan B",
                };

                Subscription FiveDaySubscription = new Subscription()
                {
                    Name = "Plan C",
                };

                context.AddRange(OneDaySubscription, ThreeDaySubscription, FiveDaySubscription);
                context.SaveChanges();
            }
            
            //Add subscription to user
            if (user.SubscriptionId == null)
            {
                user.SubscriptionId = context.Subscriptions.Where(x => x.Name == "Plan B").First().Id;
                await userManager.UpdateAsync(user);
            }

            //Add days to user
            if (context.UserDays.Where(x => x.UserId == user.Id).Count() == 0)
            {
                UserDay userDayOneUserOne = new UserDay
                {
                    DayNumber = 1,
                    UserId = user.Id
                };

                UserDay userDayTwoUserOne = new UserDay
                {
                    DayNumber = 3,
                    UserId = user.Id
                };

                UserDay userDayThreeUserOne = new UserDay
                {
                    DayNumber = 5,
                    UserId = user.Id
                };

                context.UserDays.AddRange(userDayOneUserOne, userDayTwoUserOne, userDayThreeUserOne);
                context.SaveChanges();
            }
        } 

        private static void InitializeBookings(FloLabContext context, User user)
        {
            Space space = context.Spaces.Where(x => x.Name.Equals("Sun Room")).First();

            if (context.Bookings.Where(x => x.UserId.Equals(user.Id)).Count() == 0)
            {
                Booking bookingOne = new Booking
                {
                    Start = new DateTime(2019, 7, 29, 12, 0, 0),
                    End = new DateTime(2019, 7, 29, 14, 0, 0),
                    UserId = user.Id,
                    SpaceId = space.Id
                };

                Booking bookingTwo = new Booking
                {
                    Start = new DateTime(2019, 7, 30, 12, 0, 0),
                    End = new DateTime(2019, 7, 30, 13, 0, 0),
                    UserId = user.Id,
                    SpaceId = space.Id
                };

                Booking bookingThree = new Booking
                {
                    Start = new DateTime(2019, 7, 31, 9, 0, 0),
                    End = new DateTime(2019, 7, 31, 12, 0, 0),
                    UserId = user.Id,
                    SpaceId = space.Id
                };

                context.Bookings.AddRange(bookingOne, bookingTwo, bookingThree);
                context.SaveChanges();
            }
        }
        
        private static void InitializeAttendance(FloLabContext context, User user)
        {
            if (context.Attendances.Where(x => x.UserId.Equals(user.Id)).Count() == 0)
            {
                Attendance attendanceOne = new Attendance
                {
                    Date = new DateTime(2019, 7, 29),
                    UserId = user.Id
                };

                Attendance attendanceTwo = new Attendance
                {
                    Date = new DateTime(2019, 7, 30),
                    UserId = user.Id
                };

                Attendance attendanceThree = new Attendance
                {
                    Date = new DateTime(2019, 7, 31),
                    UserId = user.Id
                };

                context.Attendances.AddRange(attendanceOne, attendanceTwo, attendanceThree);
                context.SaveChanges();
            }
        }
    }
}
