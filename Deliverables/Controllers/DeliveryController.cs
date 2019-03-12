using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Configuration;
using System.Data.SqlClient;
using Deliverables.Models;
/*using newtonsoft.json;*/

namespace Deliverables.Controllers
{
    public class DeliveryController : Controller
    {
        // GET: Delivery
        public ActionResult Index()
        {
            List<DeliveryModel> Delivery = new List<DeliveryModel>();
            string conne = ConfigurationManager.ConnectionStrings["conne"].ConnectionString;
            string query = "SELECT * FROM DeliveryTbl";
            using (SqlConnection conn = new SqlConnection(conne))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                   
                        cmd.Connection = conn;
                        
                          conn.Open();
                    
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            Delivery.Add(new DeliveryModel
                            {
                                IDNumber = Convert.ToString(sdr[0]),
                                FirstName = Convert.ToString(sdr[1]),
                                Surname = Convert.ToString(sdr[2]),
                                //DOB = Convert.ToString(sdr[3]),
                                //Age = Convert.ToInt16(sdr[4]),
                                //Gender = Convert.ToString(sdr[5])
                            });
                        }
                    }
                    conn.Close();
                }
            }

            if (Delivery.Count == 0)
            {
                Delivery.Add(new DeliveryModel());
            }
            return View(Delivery);            
        }
      

        [HttpPost]
        public ActionResult InsertDelivery ( string IDNumber, string FirstName, string Surname, string DOB, int Age, string Gender)
        {
            DeliveryModel x = new DeliveryModel();
            string query = "INSERT INTO [DeliveryDb].[dbo].[DeliveryTbl]([IDNumber],[FirstName],[Surname],[DOB],[Age],[Gender])VALUES(@IDNumber, @FirstName, @Surname, @DOB, @Age, @Gender)";

            string conne = ConfigurationManager.ConnectionStrings["conne"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conne))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@IDNumber", IDNumber);
                    cmd.Parameters.AddWithValue("@FirstName", FirstName);
                    cmd.Parameters.AddWithValue("@Surname", Surname);
                    cmd.Parameters.AddWithValue("@DOB", DOB );
                    cmd.Parameters.AddWithValue("@Age", Age);
                    cmd.Parameters.AddWithValue("@Gender", Gender);

                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteReader();
                    con.Close();
                }
            }

            return View("Index");
        }

        [HttpPost]
        public ActionResult UpdateDelivery(int id)
        {
            DeliveryModel delivery = new DeliveryModel();
            string query = "UPDATE [DeliveryDb].[dbo].[DeliveryTbl] SET IdNo= [IDNumber], Fname=[FirstName], Sname=[Surname],  WHERE id=[IDNumber]";
            string conne = ConfigurationManager.ConnectionStrings["conne"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conne))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@IDNumber", delivery.IDNumber);
                    cmd.Parameters.AddWithValue("@FirstName", delivery.FirstName);
                    cmd.Parameters.AddWithValue("@Surname", delivery.Surname);

                    

                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteReader();
                    con.Close();
                }
            }

            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult DeleteDelivery(int id)
        {
            string query = "DELETE FROM DeliveryTbl WHERE id=[IDNumber]";
            string conne = ConfigurationManager.ConnectionStrings["conne"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conne))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@IDNumber", id);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            return new EmptyResult();
        }
    }
}