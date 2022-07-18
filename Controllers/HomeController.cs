using Chess_master_2.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Chess_master_2.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        //[Authorize]
        [HttpPost]
        public ActionResult Index(Online_Chess chess, OnlineChess objchess)
        {
            Online_ChessEntities signdb = new Online_ChessEntities();
            var signobj = signdb.OnlineChesses.Where(a => a.UserName.Equals(objchess.UserName) && a.Password.Equals(objchess.Password)).FirstOrDefault();

            if (signobj == null && chess.Email_ID != null && chess.Country != null)
            {
                UserEntry(chess);

                if (ModelState.IsValid)
                {
                    using (Online_ChessEntities db = new Online_ChessEntities())
                    {
                        var obj = db.OnlineChesses.Where(a => a.UserName.Equals(objchess.UserName) && a.Password.Equals(objchess.Password)).FirstOrDefault();
                        if (obj != null)
                        {
                            Session["UserID"] = obj.UserID.ToString();
                            Session["UserName"] = obj.UserName.ToString();
                        }
                        else
                        {
                            ViewBag.loginfailmassage = "<script>alert('Incorrect UserName or Password !')</script>";
                        }
                    }
                }
            }
            else
            {
                if (ModelState.IsValid)
                {
                    using (Online_ChessEntities db = new Online_ChessEntities())
                    {
                        var obj = db.OnlineChesses.Where(a => a.UserName.Equals(objchess.UserName) && a.Password.Equals(objchess.Password)).FirstOrDefault();
                        if (obj != null)
                        {
                            Session["UserID"] = obj.UserID.ToString();
                            Session["UserName"] = obj.UserName.ToString();
                        }
                        else
                        {
                            //ViewBag.loginfailmassage = "<script>alert('Incorrect UserName or Password !')</script>";
                            TempData["LoginError"] = "Incorrect UserName or Password";
                        }
                    }
                }
            }

            return View();
        }

        public void UserEntry(Online_Chess chess)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ChessDB"].ToString());

            SqlCommand cmd = new SqlCommand("dbo.sp_chess", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserName", chess.UserName);
            cmd.Parameters.AddWithValue("@Password", chess.Password);
            cmd.Parameters.AddWithValue("@Email_ID", chess.Email_ID);
            cmd.Parameters.AddWithValue("@country", chess.Country);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ViewBag.Usersignup = 0;
            ModelState.Clear();
        }

        [HttpPost]
        public ActionResult NoResult(Online_Chess coordinates, Online_Chess objresult)
        {
            if (coordinates.x1 == 11 || coordinates.x1 == 5 || coordinates.x1 == 2 || coordinates.x1 == 3 || coordinates.x1 == 6 || coordinates.x1 == 7 || coordinates.x1 == 1 || coordinates.x1 == 12 || coordinates.x1 == 13)
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ChessDB"].ToString()))
                {
                    connection.Open();
                    if (coordinates.x1 == 11)
                    {
                        var sql = "INSERT INTO ChessResultDetails(UserID,ResultMasterID, Inserteddate) VALUES(@UserID,@ResultMasterID, @Inserteddate)";
                        using (var cmd = new SqlCommand(sql, connection))
                        {
                            if (Session["UserID"] != null)
                            {
                                cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                                cmd.Parameters.AddWithValue("@ResultMasterID", 4);
                                cmd.Parameters.AddWithValue("@Inserteddate", DateTime.Now);

                                cmd.ExecuteNonQuery();
                            }

                        }
                    }
                    else if (coordinates.x1 == 3)
                    {
                        var updateentry = "UPDATE ChessResultDetails SET UserID = @UserID, ResultMasterID = @ResultMasterID ,Inserteddate = @Inserteddate WHERE UserID = @UserID";
                        using (var cmd = new SqlCommand(updateentry, connection))
                        {
                            if (Session["UserID"] != null)
                            {
                                cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                                cmd.Parameters.AddWithValue("@ResultMasterID", 3);
                                cmd.Parameters.AddWithValue("@Inserteddate", DateTime.Now);

                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                    else if (coordinates.x1 == 2 || coordinates.x1 == 5 || coordinates.x1 == 1 || coordinates.x1 == 12)
                    {
                        var updateentry = "UPDATE ChessResultDetails SET UserID = @UserID, ResultMasterID = @ResultMasterID ,Inserteddate = @Inserteddate WHERE UserID = @UserID";
                        using (var cmd = new SqlCommand(updateentry, connection))
                        {
                            if (Session["UserID"] != null)
                            {
                                cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                                cmd.Parameters.AddWithValue("@ResultMasterID", 2);
                                cmd.Parameters.AddWithValue("@Inserteddate", DateTime.Now);

                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                    else if (coordinates.x1 == 7 || coordinates.x1 == 6 || coordinates.x1 == 13)
                    {
                        var updateentry = "UPDATE ChessResultDetails SET UserID = @UserID, ResultMasterID = @ResultMasterID ,Inserteddate = @Inserteddate WHERE UserID = @UserID";
                        using (var cmd = new SqlCommand(updateentry, connection))
                        {
                            if (Session["UserID"] != null)
                            {
                                cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                                cmd.Parameters.AddWithValue("@ResultMasterID", 1);
                                cmd.Parameters.AddWithValue("@Inserteddate", DateTime.Now);

                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }

            return null;
        }
        public ActionResult LogOut()
        {
            Session.Abandon();

            return RedirectToAction("Index");
        }
    }
}