using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using MyMVC.Models;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Net;

namespace MyMVC.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            List<Student> students = new List<Student>();
            var con = new SqlConnection("Data Source=ARVIND; Initial Catalog=SatishMVCdb;  Integrated Security=True");
            con.Open();
            var command = new SqlCommand("Select * From Student", con);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int Id = Convert.ToInt32(reader["Id"]);
                string Name = Convert.ToString(reader["Name"]);
                int Age = Convert.ToInt32(reader["Age"]);
                string FatherName = Convert.ToString(reader["FatherName"]);
                string MotherName = Convert.ToString(reader["MotherName"]);
                string Address = Convert.ToString(reader["Address"]);
                students.Add(new Student
                {
                    Id = Id,
                    Name = Name,
                    Age = Age,
                    FatherName = FatherName,
                    MotherName = MotherName,
                    Address = Address,
                });
            }
            con.Dispose();
            return View(students);
        }

        public IActionResult Insert()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Insert(Student student)
        {
            var con = new SqlConnection("Data Source=ARVIND; Initial Catalog=SatishMVCdb; Integrated Security=True");
            con.Open();
            var command = new SqlCommand("insert into Student values ('" + student.Name + "','" + student.Age + "'" +
                ",'" + student.FatherName + "','" + student.MotherName + "','" + student.Address + "') ", con);
            command.ExecuteNonQuery();
            con.Dispose();

            return RedirectToAction(nameof(Index));

        }


        [HttpGet]
        public IActionResult Update(int id)
        {
            Student student = new Student();
            var con = new SqlConnection("Data Source=ARVIND; Initial Catalog=SatishMVCdb; Integrated Security=True");
            con.Open();
            var command = new SqlCommand("select * from Student where id=" + id + "", con);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                student.Id = Convert.ToInt32(reader["ID"]);
                student.Name = Convert.ToString(reader["Name"]);
                student.Age = Convert.ToInt32(reader["Age"]);
                student.FatherName = Convert.ToString(reader["FatherName"]);
                student.MotherName = Convert.ToString(reader["MotherName"]);
                student.Address = Convert.ToString(reader["Address"]);
            }


            con.Dispose();

            return View(student);
        }

        [HttpPost]

        public IActionResult Update(Student ssp)
        {
            string ConnectionString = "Data Source=ARVIND; Initial Catalog=SatishMVCdb; Integrated Security=True";
            var con = new SqlConnection(ConnectionString);
            con.Open();

            // string query = "update Student set Name ='" + ssp.@Name + "','" + ssp.@Age + "','" + ssp.@FatherName + "','" + ssp.@MotherName + "','" + ssp.@Address + "'";

            string query = "update Student set Name= '" + ssp.@Name + "',Age='" + ssp.@Age + "'" + ",FatherName='" + ssp.@FatherName + "',MotherName='" + ssp.@MotherName + "',Address='" + ssp.@Address + "' where Id ='"+ ssp.Id +"' ";
            var command = new SqlCommand(query,con);
            command.ExecuteReader();
            con.Dispose();


            return RedirectToAction("Index");

        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var con = new SqlConnection("Data Source=ARVIND; Initial Catalog=SatishMVCdb; Integrated Security=True");
            con.Open();
            var command = new SqlCommand("Delete Student where id="+id+"",con);
            command.ExecuteReader();
            con.Dispose();
            return RedirectToAction("Index");
        }
    }
}
