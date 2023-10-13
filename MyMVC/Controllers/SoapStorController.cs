using Microsoft.AspNetCore.Mvc;
using MyMVC.Models;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography.X509Certificates;

namespace MyMVC.Controllers
{
    public class SoapStorController : Controller
    {
        public IActionResult Index()
        {
            List<SoapStor> list = new List<SoapStor>();
            var con = new SqlConnection("Data Source=ARVIND; Initial Catalog=SatishMVCdb; Integrated Security=True");
            con.Open();
            var command = new SqlCommand("Select * From SoapStor", con);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int id = Convert.ToInt32(reader["ID"]);
                string Name = Convert.ToString(reader["SoapName"]);
                int Quntity = Convert.ToInt32(reader["Quntity"]);
                int Price = Convert.ToInt32(reader["Price"]);

                list.Add(new SoapStor
                {
                    Id = id,
                    SoapName = Name,
                    Quntity = Quntity, 
                    Price = Price
                });
            }

            return View(list);
        }


        public IActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Insert(SoapStor soapStor)
        {
            var con = new SqlConnection("Data Source=ARVIND; Initial Catalog=SatishMVCdb; Integrated Security=True");
            con.Open();
            var command = new SqlCommand("insert into SoapStor values ('" + soapStor.SoapName + "','" + soapStor.Quntity + "','" + soapStor.Price + "')", con);
            command.ExecuteReader();
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult Edit(int Id)
        {
            SoapStor stor = new SoapStor();
            var con = new SqlConnection("Data Source=ARVIND; Initial Catalog=SatishMVCdb; Integrated Security=True");
            con.Open();
            var command = new SqlCommand("SELECT * FROM SoapStor WHERE Id = " + Id + "", con);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                stor.Id = Convert.ToInt32(reader["ID"]);
                stor.SoapName = Convert.ToString(reader["SoapName"]);
                stor.Quntity = Convert.ToInt32(reader["Quntity"]);
                stor.Price = Convert.ToInt32(reader["Price"]);
            }
            con.Close();
            return View(stor);
        }
             
            
        
        [HttpPost]
        public IActionResult Edit(SoapStor ess)
        {
            string connectionString = "Data Source=ARVIND; Initial Catalog=SatishMVCdb; Integrated Security=True";
            var con = new SqlConnection(connectionString);
            con.Open();
            string query = "update SoapStor set SoapName = '" + ess.@SoapName + "', Quntity = '" + ess.@Quntity + "', Price = '" + ess.@Price + "' where Id ='" + ess.Id + "'";
            var command = new SqlCommand(query, con);
            command.ExecuteReader();
            con.Close(); 
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            var con = new SqlConnection("Data Source=ARVIND; Initial Catalog=SatishMVCdb; Integrated Security=True");
            con.Open();
            var command = new SqlCommand("delete SoapStor where Id=" + Id + "", con);
             command.ExecuteReader();
          
            return RedirectToAction("Index");


        }
    }
    }

