using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tar1.BL;
using Microsoft.AspNetCore.Mvc;
using Tar1.DAL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tar1.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        // GET: api/values
        [HttpGet]
        public object Get()
        {
            try
            {
                object userList = Tar1.BL.User.Read();
                if (userList == null)
                {
                    return BadRequest("Please insert Valid rating");
                }
                else
                {
                    return Ok(userList);
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // שגיאת שרת
            }

        }

        [HttpGet("wishList/{UserId}")]
        public IActionResult GetMovies4User(int UserId)
        {
            try
            {
                List<Movie> wishList = Tar1.BL.User.GetWishList(UserId); ;
                if (wishList == null)
                {
                    return BadRequest("Please insert Valid rating");
                }
                else
                {
                    return Ok(wishList);
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // שגיאת שרת
            }

        }

        //Adding Movie to WishList 
        [HttpPost]
        [Route("WishList/{UserId}/{MovieId}")]
        public ActionResult Insert2WishList(int UserId, int MovieId)
        {
            try
            {
                int res = Tar1.BL.User.Movie2WishList(UserId, MovieId);

                if (res == 1)
                {
                    return Ok("Succesfully added to wishList");
                }

                else
                {
                    return Unauthorized("Failed, movie was not added to wish list");                 }

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // שגיאת שרת

            }
        }
        [HttpDelete]
        [Route("WishList/{UserId}/{MovieId}")]
        public ActionResult RemoveFromWishList(int UserId, int MovieId)
        {
            try
            {
                int res = Tar1.BL.User.RemoveFromWishList(UserId, MovieId);

                if (res == 1)
                {
                    return Ok(new { message= "Succesfully Removed From wishList"});
                    }

                else
                {
                    return Unauthorized("Failed, movie was not Removed From wishlist");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // שגיאת שרת

            }
        }

        //USER LOGIN 
        // POST: api/User/Login
        [HttpPost]
        [Route("Login")]
        public ActionResult UserLogin([FromBody] User userToLogin)
        {
            if (userToLogin == null)
            {
                return BadRequest("Information missing, please insert valid userName and Paswword " );
            }


            try
            {
                int res = Tar1.BL.User.UserLogin(userToLogin);
                if (res ==1)
                {
                    User u = Tar1.BL.User.Read(userToLogin);
                    return Ok(new
                    {
                        message = "Welcome",
                                    userId=u.ID,
                                    username=u.UserName,
                                    userEmail=u.Email
                                   
                    });

                }

                else
                {
                    return Unauthorized("Login failed. Please check your username and password.");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // שגיאת שרת

            }
        }


        // POST: api/User/Register
        [HttpPost]
        [Route("Register")]

        public ActionResult UserRegistration([FromBody] User NewUser)
        {
            if (NewUser == null)
            {
                return BadRequest( "Information missing, please insert valid userName and Paswword" );
            }
            else
            {
                DBService dbs = new DBService();
                List<User> userList = dbs.GetAllUsers();

                foreach (var user in userList)
                {
                    if (user.UserName==NewUser.UserName)
                    {
                        return Conflict("UserName already exists, Please choose another username");
                    }
                }
            }
            try
            {
                int RegistrationResult = Tar1.BL.User.UserRegistration(NewUser);

                if (RegistrationResult < 1)
                    return Unauthorized("Username or Password are incorrect please try again.");
                else
                {
                    User u = Tar1.BL.User.Read(NewUser);

                    return Ok(new
                    {
                        message = "Registration Seccedded",
                        userId = u.ID,
                        username = u.UserName,
                        userEmail = u.Email
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);

            }
        }
    }

    
}

