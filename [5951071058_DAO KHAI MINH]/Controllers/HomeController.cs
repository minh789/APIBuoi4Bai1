using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using _5951071058_DAO_KHAI_MINH_.Models;
using _DAO_KHAI_MINH_5951071058_.Models;
using System.Data;

namespace _5951071058_DAO_KHAI_MINH_.Controllers
{
    public class HomeController : Controller
    {
        db dpop = new db();
        string msg;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            Employee emp = new Employee();
            emp.Flag = "get";
            DataSet ds = dpop.Empget(emp, out msg);
            List<Employee> list = new List<Employee>();
            foreach(DataRow dr in ds.Tables[0].Rows)
            {
                list.Add(new Employee
                {
                    Sr_no=Convert.ToInt32(dr["Sr_no"]),
                    Emp_name=dr["Emp_name"].ToString(),
                    City= dr["City"].ToString(),
                    State = dr["State"].ToString(),
                    Country = dr["Country"].ToString(),
                    Department = dr["Department"].ToString(),
                });
            }
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Create([Bind] Employee emp)
        {
            try
            {
                emp.Flag = "insert";
                dpop.Empdml(emp, out msg);
                TempData["msg"] = msg;
            }
            catch (Exception ex)
            {
                TempData["msg"]= ex.Message;
            }
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            Employee emp = new Employee();
            emp.Sr_no = id;
            emp.Flag = "getid";
            DataSet ds = dpop.Empget(emp, out msg);
            foreach(DataRow dr in ds.Tables[0].Rows)
            {
                emp.Sr_no = Convert.ToInt32(dr["Sr_no"]);
                emp.Emp_name = dr["Emp_name"].ToString();
                emp.City = dr["City"].ToString();
                emp.State = dr["State"].ToString();
                emp.Country = dr["Country"].ToString();
                emp.Department = dr["Department"].ToString();
            }
            return View(emp);
        }

        public IActionResult Delete(int id,[Bind] Employee emp)
        {
            try
            {
                emp.Sr_no = id;
                emp.Flag = "update";
                dpop.Empget(emp, out msg);
                TempData["msg"] = msg;
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return RedirectToAction("Index");
        }
    }
}
