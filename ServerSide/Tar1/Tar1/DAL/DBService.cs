using System;
using System.Data;
using System.Data.SqlClient;
using Tar1.BL;

namespace Tar1.DAL
{
    public class DBService
    {
        public DBService()
        {
        }

        public SqlConnection connect(String conString)
        {

            // read the connection string from the configuration file
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json").Build();
            string cStr = configuration.GetConnectionString("myProjDB");
            SqlConnection con = new SqlConnection(cStr);
            con.Open();
            return con;
        }

        private SqlCommand CreateCommandWithStoredProcedureGeneral(String spName, SqlConnection con, Dictionary<string, object> paramDic)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            if (paramDic != null)
                foreach (KeyValuePair<string, object> param in paramDic)
                {
                    cmd.Parameters.AddWithValue(param.Key, param.Value);

                }


            return cmd;
        }


        //Movies
        public List<Movie> GetAllMovies()
        {
            SqlConnection con;
            SqlCommand cmd;
            List<Movie> movieList = new List<Movie>();
            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateCommandWithStoredProcedureGeneral("spReadMovies", con, null); // create the command

            try
            {

                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dataReader.Read())
                {
                    Movie m = new Movie();
                    m.ID = Convert.ToInt32(dataReader["Id"]);
                    m.Title = dataReader["Title"].ToString();
                    m.Rating = Convert.ToDouble(dataReader["Rating"]);
                    m.Income = Convert.ToInt32(dataReader["Income"]);
                    m.ReleaseYear = Convert.ToInt32(dataReader["ReleaseYear"]);
                    m.Duration = Convert.ToInt32(dataReader["Duration"]);
                    m.Language = Convert.ToString(dataReader["Language"]);
                    m.Description = dataReader["Description"].ToString();
                    m.Genre = Convert.ToString(dataReader["Genre"]);
                    m.PhotoURL = Convert.ToString(dataReader["photoUrl"]);
                    movieList.Add(m);

                }
                return movieList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        public List<Movie> GetMoviesByRating(double rating)
        {
            SqlConnection con;
            SqlCommand cmd;
            List<Movie> movieList = new List<Movie>();
            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@rating", rating);


            cmd = CreateCommandWithStoredProcedureGeneral("spGetMovieByRating", con, paramDic); // create the command

            try
            {

                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dataReader.Read())
                {
                    Movie m = new Movie();
                    m.ID = Convert.ToInt32(dataReader["Id"]);
                    m.Title = dataReader["Title"].ToString();
                    m.Rating = Convert.ToDouble(dataReader["Rating"]);
                    m.Income = Convert.ToInt32(dataReader["Income"]);
                    m.ReleaseYear = Convert.ToInt32(dataReader["ReleaseYear"]);
                    m.Duration = Convert.ToInt32(dataReader["Duration"]);
                    m.Language = Convert.ToString(dataReader["Language"]);
                    m.Description = Convert.ToString(dataReader["Description"]);
                    m.Genre = Convert.ToString(dataReader["Genre"]);
                    m.PhotoURL = Convert.ToString(dataReader["photoUrl"]);
                    movieList.Add(m);

                }
                return movieList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        public List<Movie> GetMoviesByDuration(int Duration)
        {
            SqlConnection con;
            SqlCommand cmd;
            List<Movie> movieList = new List<Movie>();
            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@Duration", Duration);


            cmd = CreateCommandWithStoredProcedureGeneral("spGetMovieByDuration", con, paramDic); // create the command

            try
            {

                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dataReader.Read())
                {
                    Movie m = new Movie();
                    m.ID = Convert.ToInt32(dataReader["Id"]);
                    m.Title = dataReader["Title"].ToString();
                    m.Rating = Convert.ToDouble(dataReader["Rating"]);
                    m.Income = Convert.ToInt32(dataReader["Income"]);
                    m.ReleaseYear = Convert.ToInt32(dataReader["ReleaseYear"]);
                    m.Duration = Convert.ToInt32(dataReader["Duration"]);
                    m.Language = Convert.ToString(dataReader["Language"]);
                    m.Description = Convert.ToString(dataReader["Description"]);
                    m.Genre = Convert.ToString(dataReader["Genre"]);
                    m.PhotoURL = Convert.ToString(dataReader["photoUrl"]);
                    movieList.Add(m);

                }
                return movieList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        public int InsertMovie(Movie m)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@Title", m.Title);
            paramDic.Add("@Rating", m.Rating);
            paramDic.Add("@Income", m.Income);
            paramDic.Add("@ReleaseYear", m.ReleaseYear);
            paramDic.Add("@Duration", m.Duration);
            paramDic.Add("@Language", m.Language);
            paramDic.Add("@Description", m.Description);
            paramDic.Add("@Genre", m.Genre);
            paramDic.Add("@photoUrl", m.PhotoURL);

            cmd = CreateCommandWithStoredProcedureGeneral("spInsertMovie", con, paramDic); // create the command

            try
            {

                int numEff = cmd.ExecuteNonQuery();
                return numEff;

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        //Cast

        public List<Cast> GetAllCast()
        {
            SqlConnection con;
            SqlCommand cmd;
            List<Cast> castList = new List<Cast>();
            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            cmd = CreateCommandWithStoredProcedureGeneral("spReadCast", con, null); // create the command

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dataReader.Read())
                {
                    Cast c = new Cast();
                    c.ID = Convert.ToString(dataReader["Id"]);
                    c.Name = Convert.ToString(dataReader["Name"]);
                    c.Role = Convert.ToString(dataReader["Role"]);
                    c.DateOfBirth = Convert.ToString(dataReader["DateOfBirth"]);
                    c.Country = Convert.ToString(dataReader["Country"]);
                    c.photoURL = Convert.ToString(dataReader["photoUrl"]);
                    castList.Add(c);

                }
                return castList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        public object GetCast4Movie(int MovieId)
        {
            SqlConnection con;
            SqlCommand cmd;
            List<object> castList = new List<object>();
            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@movieId", MovieId);

            cmd = CreateCommandWithStoredProcedureGeneral("SPGetCastForMovie", con, paramDic); // create the command

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dataReader.Read())
                {
                    castList.Add(new
                    {
                        ID = Convert.ToString(dataReader["Id"]),
                        Name = Convert.ToString(dataReader["Name"]),
                        photoURL = Convert.ToString(dataReader["photoUrl"]),
                        ListType = Convert.ToString(dataReader["ListType"])
                    });
                }
                return castList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        public int InsertCast(Cast c)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@Name", c.Name);
            paramDic.Add("@Role", c.Role);
            paramDic.Add("@DateOfBirth", c.DateOfBirth);
            paramDic.Add("@Country", c.Country);
            paramDic.Add("@photoURL", c.photoURL);

            cmd = CreateCommandWithStoredProcedureGeneral("spInsertCast", con, paramDic); // create the command

            try
            {
                int numEff = cmd.ExecuteNonQuery();
                return numEff;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        public int InsertCast2CastInMovie(int castId, int movieId)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>();

            paramDic.Add("@CastId", castId);
            paramDic.Add("@movieId", movieId);



            cmd = CreateCommandWithStoredProcedureGeneral("SPInsertCastInMovie", con, paramDic); // create the command

            try
            {
                int numEff = cmd.ExecuteNonQuery();
                return numEff;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        public int DeleteCastFromMovie(int movieId)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@MovieID", movieId);


            cmd = CreateCommandWithStoredProcedureGeneral("SPClearCast4Movie", con, paramDic); // create the command

            try
            {
                int numEff = cmd.ExecuteNonQuery();
                return numEff;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        //User
        public List<User> GetAllUsers()
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateCommandWithStoredProcedureGeneral("spReadUsers", con, null); // create the command

            try
            {

                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);



                List<User> UserList = new List<User>();

                while (dataReader.Read())
                {
                    User u = new User();
                    u.UserName = dataReader["UserName"].ToString();
                    u.Email = dataReader["Email"].ToString();
                    u.ID = Convert.ToInt32(dataReader["id"].ToString());
                    u.wishListCount = Convert.ToInt32(dataReader["wishlistCount"].ToString());
                    UserList.Add(u);
                }

                return UserList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        public User GetUser(User userToGet)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@UserName", userToGet.UserName); //Send Username
            paramDic.Add("@Password", userToGet.Password);  //Send Password

            cmd = CreateCommandWithStoredProcedureGeneral("SPGetUser", con, paramDic); // create the command

            try
            {

                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);



                User u = new User();
                while (dataReader.Read())
                {
                    u.UserName = dataReader["UserName"].ToString();
                    u.Email = dataReader["Email"].ToString();
                    u.ID = Convert.ToInt32(dataReader["id"].ToString());
                }

                return u;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        public int InsertUser(User newuser)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@UserName", newuser.UserName); //Send Username
            paramDic.Add("@Email", newuser.Email); //Send Email
            paramDic.Add("@Password", newuser.Password);  //Send Password

            cmd = CreateCommandWithStoredProcedureGeneral("spUserRegistration", con, paramDic); // create the command

            try
            {
                int numEff = cmd.ExecuteNonQuery();
                return numEff;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        } // Register User

        public int UserLogin(User userToLogin)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@UserName", userToLogin.UserName); //Send Username
            paramDic.Add("@Password", userToLogin.Password);  //Send Password

            cmd = CreateCommandWithStoredProcedureGeneral("spUserLogin", con, paramDic); // create the command

            try
            {
                SqlParameter returnValue = new SqlParameter();
                returnValue.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(returnValue);

                cmd.ExecuteNonQuery();

                int result = (int)returnValue.Value;
                Console.WriteLine($"Return value: {result}"); // Debug log
                return result;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        } //User Login

        public List<Movie> GetMovie4User(int userId)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@UserId", userId); //Send Username

            List<Movie> wishList = new List<Movie>();

            cmd = CreateCommandWithStoredProcedureGeneral("SPGetMovies4User", con, paramDic); // create the command

            try
            {

                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dataReader.Read())
                {
                    Movie m = new Movie();
                    m.ID = Convert.ToInt32(dataReader["Id"]);
                    m.Title = dataReader["Title"].ToString();
                    m.Rating = Convert.ToDouble(dataReader["Rating"]);
                    m.Income = Convert.ToInt32(dataReader["Income"]);
                    m.ReleaseYear = Convert.ToInt32(dataReader["ReleaseYear"]);
                    m.Duration = Convert.ToInt32(dataReader["Duration"]);
                    m.Language = Convert.ToString(dataReader["Language"]);
                    m.Description = Convert.ToString(dataReader["Description"]);
                    m.Genre = Convert.ToString(dataReader["Genre"]);
                    m.PhotoURL = Convert.ToString(dataReader["photoUrl"]);
                    wishList.Add(m);

                }
                return wishList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        } // Gets A user's WishList

        public int AddMovie2WishList(int UserId, int MovieId)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@UserId", UserId);
            paramDic.Add("@MovieId", MovieId);

            cmd = CreateCommandWithStoredProcedureGeneral("spInsertMovie2WishList", con, paramDic); // create the command

            try
            {
                int numEff = cmd.ExecuteNonQuery();
                return numEff;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }//Adds A movie to a user's WishList

        public int RemoveMovieFromWishList(int UserId, int MovieId) //Removes A movie from a user's WishList
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            Dictionary<string, object> paramDic = new Dictionary<string, object>();
            paramDic.Add("@UserId", UserId);
            paramDic.Add("@MovieId", MovieId);

            cmd = CreateCommandWithStoredProcedureGeneral("SPDeleteMovieFromWishlist", con, paramDic); // create the command

            try
            {
                int numEff = cmd.ExecuteNonQuery();
                return numEff;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

    }

}
















