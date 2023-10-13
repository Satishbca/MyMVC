using Microsoft.AspNetCore.Mvc;
using MyMVC.Models;
using System.Data.SqlClient;

namespace MyMVC.Controllers
{
    public class MyStoreController : Controller
    {
        public IActionResult Index()
        {
            List< MyStore >List = new List< MyStore >();
            var con = new SqlConnection("Data Source=ARVIND; Initial Catalog=SatishMVCdb; Integrated Security=True");
            con.Open();
            var command = new SqlCommand("select * from MyStore",con);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int id = Convert.ToInt32(reader["id"]);
                string Name= Convert.ToString(reader["Name"]);
                string Email = Convert.ToString(reader["Email"]);
                int Phone = Convert.ToInt32(reader["Phone"]);
                string Address= Convert.ToString(reader["Address"]);

                List.Add(new MyStore()
                {
                    Id = id,
                    Name=Name,
                    Email=Email,
                    Phone=Phone,
                    Address=Address
                });


            }
            con.Dispose();

            return View(List);
        } 
    }
}
