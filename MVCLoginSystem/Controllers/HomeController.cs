using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// using System.Web.UI.WebControls;
using MVCLoginSystem.Models;

namespace MVCLoginSystem.Controllers
{
    public class HomeController : Controller
    {

        private string connectionString;

        public HomeController()
        {
            connectionString = ConfigurationManager.ConnectionStrings["mvcconnectionstring"].ConnectionString;
        }



        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpGet]
        public ActionResult Login() 
        {
            TempData["SuccessMessage"] = null;
            ViewBag.ErrorMessage = null;
            return View();
        
        }


        [HttpPost]
        public ActionResult Login(Login model)
        {
            if (ModelState.IsValid)
            {
                Login user = GetUser(model.Username, model.Password);
                if (user != null )
                {
                    TempData["SuccessMessage"] = "Successfully logged in!";
                    if (user.Usertype == "Admin")
                    {
                        return RedirectToAction("AdminMain", "Home"); // Redirect to a new page after successful login

                    }
                    else {
                        return RedirectToAction("UserMain", "Home"); // Redirect to a new page after successful login
                    }


                }
                else
                {
                    ViewBag.ErrorMessage = "Invalid username or password.";
                }
            }
            return View();
        }

        private Login GetUser(string username, string password)
        {
            Login user = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM login WHERE Username = @Username AND Password = @Password";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    user = new Login
                    {
                        Username = reader["Username"].ToString(),
                        Password = reader["Password"].ToString(),
                        Usertype = reader["Usertype"].ToString()
                    };
                }
                
                reader.Close();
            }

            return user;
        }
        [HttpGet]
        public ActionResult RecoverPassword() {
            return View();
        }


        [HttpGet]
        public ActionResult AdminMain()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UserMain()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }
        
        
        [HttpPost]
        public ActionResult RecoverPassword(RecoverPassword model)
        {
            if (ModelState.IsValid)
            {
                RecoverPassword user = RecoverUser(model.Username, model.security);
                if (user != null)
                {
                    TempData["SuccessMessage"] = "Success!";
                    return RedirectToAction("ChangePassword", "Home");


                }
                else
                {
                    ViewBag.ErrorMessage = "Failed.";
                }
            }
            return View();
        }

        private RecoverPassword RecoverUser(string username, string security)
        {
            RecoverPassword user = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM login WHERE Username = @Username AND security_quection = @security";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@security", security);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    user = new RecoverPassword
                    {
                        Username = reader["Username"].ToString()
                       
                    };
                }

                reader.Close();
            }

            return user;
        }




    }
}