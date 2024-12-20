using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using Tar1.DAL;

namespace Tar1.BL
{
    public class User
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

     
        public User()
        {
        }

        public static List<User> Read()
        {
            DBService dbs = new DBService();
            return dbs.GetAllUsers();

        }
        public static User Read(User u)
        {
            DBService dbs = new DBService();
            return dbs.GetUser(u);

        }

        public static int UserLogin(User u)
        {
            DBService dbService = new DBService();
            return dbService.UserLogin(u);
        }

        public static int UserRegistration(User u)
        {
            DBService dBService = new DBService();
            return dBService.InsertUser(u);

        }

        public static List<Movie> GetWishList(int id)
        {
            DBService dBService = new DBService();
            return dBService.GetMovie4User(id);

        }

        public static int Movie2WishList(int UserId,int MovieId)
        {
            DBService dbs = new DBService();
            return dbs.AddMovie2WishList(UserId, MovieId);
        }

        public static int RemoveFromWishList(int UserId, int MovieId)
        {
            DBService dbs = new DBService();
            return dbs.RemoveMovieFromWishList(UserId, MovieId);
        }

    }
}

