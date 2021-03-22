using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using _DAO_KHAI_MINH_5951071058_.Models;

namespace _5951071058_DAO_KHAI_MINH_.Models
{
    public class db
    {
       //connect
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-OQJ7HTJ\SQLEXPRESS;Initial Catalog=DemoCRUD;Integrated Security=True");

        //select
        public DataSet Empget(Employee emp, out string msg)
        {
            DataSet ds = new DataSet();
            msg = "";
            try
            {
                SqlCommand com = new SqlCommand("Sp_Employee", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Sr_no", emp.Sr_no);
                com.Parameters.AddWithValue("@Emp_name", emp.Emp_name);
                com.Parameters.AddWithValue("@City", emp.City);
                com.Parameters.AddWithValue("@STATE", emp.State);
                com.Parameters.AddWithValue("@Country", emp.Country);
                com.Parameters.AddWithValue("@Department", emp.Department);
                com.Parameters.AddWithValue("@flag", emp.Flag);
                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(ds);
                msg = "OK";
                return ds;
            }
            catch(Exception e)
            {
                msg = e.Message;
                return ds;
            }
        }

        //Insert and Upadate

        public string Empdml(Employee emp, out string msg)
        {
            msg = "";
            try
            {
                SqlCommand com = new SqlCommand("Sp_Employee", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Sr_no", emp.Sr_no);
                com.Parameters.AddWithValue("@Emp_name", emp.Emp_name);
                com.Parameters.AddWithValue("@City", emp.City);
                com.Parameters.AddWithValue("@STATE", emp.State);
                com.Parameters.AddWithValue("@Country", emp.Country);
                com.Parameters.AddWithValue("@Department", emp.Department);
                com.Parameters.AddWithValue("@flag", emp.Flag);
                con.Open();
                com.ExecuteNonQuery();
                con.Close();
                msg = "OK";
                return msg;
            }
            catch (Exception e)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                msg = e.Message;
                return msg;
            }
        }
    }


}
