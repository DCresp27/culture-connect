using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MySql.Data.MySqlClient;
using Super_Jew_2._0.Backend;
using Super_Jew_2._0.Backend.Services;
using Super_Jew_2._0.Data;

namespace Super_Jew_2._0
{
    public class Program
    {
        private static void AllAvailableShuls()
        {
            Console.WriteLine("Test All Available Shuls");
            List<Shul> allShulls = ShulService.GetAllAvailableShuls();
            foreach (var shul in allShulls)
            {
                Console.WriteLine(shul.ShulName + " " + shul.Location);
            }
        }
        
        private static void GetUserShuls(string username, string password)
        {
            Console.WriteLine("Getting followed Shuls for user");
            User user1 = ShulService.GetFollowedShulsForUser(username, password);
            Console.WriteLine(user1.UserID);
            Console.WriteLine("Shuls For Username:" + user1.Username);
            
            List<Shul> user1Shuls = user1.FollowedShuls;
            foreach (var shul in user1Shuls)
            {
                Console.WriteLine(shul.ShulName + " " + shul.Location + " " + "Shacharis Time: " + shul.ShachrisTime);
            }
        }

        private static void addShulToUserProfile(string username, string password, int shulId)
        {
            User user1 = ShulService.GetFollowedShulsForUser("john_doe", "password123");
            List<Shul> user1Shuls = user1.FollowedShuls;
            
            
            Console.WriteLine("Attempting to Add Shul from User!");
            bool addShulToUserProfile = ShulService.AddShulToUserProfile(user1.UserID, shulId);
            Console.WriteLine("Added Shul To User Profile: " + addShulToUserProfile);
            
            //update user one
            user1 = ShulService.GetFollowedShulsForUser("john_doe", "password123");
            Console.WriteLine(user1.UserID);
            Console.WriteLine("Shuls For Username:" + user1.Username);
            
            user1Shuls = user1.FollowedShuls;
            foreach (var shul in user1Shuls)
            {
                Console.WriteLine(shul.ShulName + " " + shul.Location + " " + "Shacharis Time: " + shul.ShachrisTime);
            }
        }
        
        private static void removeShulFromUserProfile(string username, string password, int shulId)
        {
            User user1 = ShulService.GetFollowedShulsForUser(username, password);
            List<Shul> user1Shuls = user1.FollowedShuls;
            
            
            Console.WriteLine("Attempting to Remove Shul from User!");
            bool addShulToUserProfile = ShulService.RemoveShulFromUserProfile(user1.UserID, shulId);
            Console.WriteLine("Removed Shul To User Profile: " + addShulToUserProfile);
            
            //update user one
            user1 = ShulService.GetFollowedShulsForUser("john_doe", "password123");
            Console.WriteLine(user1.UserID);
            Console.WriteLine("Shuls For Username:" + user1.Username);
            
            user1Shuls = user1.FollowedShuls;
            foreach (var shul in user1Shuls)
            {
                Console.WriteLine(shul.ShulName + " " + shul.Location + " " + "Shacharis Time: " + shul.ShachrisTime);
            }
        }
        
        public static void Main(string[] args)
        {
            /*
            AllAvailableShuls();
            GetUserShuls("john_doe", "password123");
            addShulToUserProfile("john_doe", "password123", 1);
            removeShulFromUserProfile("john_doe", "password123", 1);
            */
            
            //Blazor Code
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddSingleton<WeatherForecastService>();
            builder.Services.AddTransient<Class>();
           
            builder.Services.AddTransient<ShulService>(); 


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}