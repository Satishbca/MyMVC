using Microsoft.AspNetCore.Mvc;
using MyMVC.Models;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace MyMVC.Controllers
{
    public class TeacharController : Controller
    {
        public IActionResult Index()
        {
            List<Teachar> Rk = new List<Teachar>();

            var con = new SqlConnection("Data Source=ARVIND; Initial Catalog=SatishMVCdb; Integrated Security=True");
            con.Open();
            var command = new SqlCommand("select * from Teachar", con);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int Id = Convert.ToInt32(reader["ID"]);
                string Name = Convert.ToString(reader["Name"]);
                int Age = Convert.ToInt32(reader["Age"]);
                string Gender = Convert.ToString(reader["Gender"]);
                int AadharNo = Convert.ToInt32(reader["AadharNo"]);
                int phoneNo = Convert.ToInt32(reader["phoneNo"]);
                decimal Selery = Convert.ToDecimal(reader["Selery"]);
                string Address = Convert.ToString(reader["Address"]);

                Rk.Add(new Teachar()
                {
                    Id = Id,
                    Name = Name,
                    Age = Age,
                    Gender = Gender,
                    AadharNo = AadharNo,
                    phoneNo = phoneNo,
                    Selery = Selery,
                    Address = Address,

                });   
            }
            con.Dispose();
            return View(Rk);
        }

        public IActionResult Insert()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Insert(Teachar teachar)
        {
            var con = new SqlConnection("Data Source=ARVIND; Initial Catalog=SatishMVCdb; Integrated Security=True");
            con.Open();
            var command = new SqlCommand("insert into Teachar values ('" + teachar.Name + "','" + teachar.Age + "'" +
                ",'" + teachar.Gender + "','" + teachar.AadharNo + "','" + teachar.phoneNo + "','" + teachar.Selery + "','" + teachar.Address + "') ", con);
            command.ExecuteNonQuery();
            con.Dispose();

            return RedirectToAction(nameof(Index));
        }






    }
}
