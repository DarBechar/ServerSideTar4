using System;
using System.Collections;
using System.Collections.Generic;
using Tar1.DAL;

namespace Tar1.BL
{

    public class Movie
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public double Rating { get; set; }
        public double Income { get; set; }
        public int ReleaseYear { get; set; }
        public int Duration { get; set; }
        public string Language { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public string PhotoURL { get; set; }

        public Movie()
        {
        }

        public static int Insert(Movie m)
        {
            DBService dbs = new DBService();

            return dbs.InsertMovie(m);
        }

        public static List<Movie> Read()
        {

            DBService dbs = new DBService();
            return dbs.GetAllMovies();
        }

        public static List<Movie> ReadByRating(double rating)
        {

            DBService dbs = new DBService();
            return dbs.GetMoviesByRating(rating);

        }

        public static List<Movie> ReadByDuration(int duration)
        {
            DBService dbs = new DBService();

            return dbs.GetMoviesByDuration(duration);


        }

        public static int Insertcast2Movie(int castId, int MovieId)
        {
            DBService dbs = new DBService();

            return dbs.InsertCast2CastInMovie(castId, MovieId);
        }

        public static int ClearCast(int MovieId)
        {
            DBService dbs = new DBService();

            return dbs.DeleteCastFromMovie(MovieId);
          
        }
         
    }
}

