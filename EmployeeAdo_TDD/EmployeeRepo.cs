﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Data;

namespace EmployeePayrol_DB
{
    public class EmployeeRepo
    {
        public static string connectionstring = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog = EmployeePayroll; Integrated Security = True";
        SqlConnection Connection = new SqlConnection(connectionstring);

        /// <summary>
        /// UC 1 Checks the database connection.
        /// </summary>
        public bool CheckDBConnection()
        {
            try
            {
                this.Connection.Open();
                Console.WriteLine("Connection Success");
                this.Connection.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }
        /// <summary>
        ///UC 2 Gets all employee.
        /// </summary>
        /// <exception cref="Exception"></exception>
        public int GetAllEmployee()
        {
            try
            {
                int count = 0;
                EmployeeModel Model = new EmployeeModel();
                using (this.Connection)
                {
                    string query = @"Select * from EmployeePayroll;";
                    SqlCommand CMD = new SqlCommand(query, this.Connection);
                    this.Connection.Open();
                    SqlDataReader reader = CMD.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Model.Id = reader.GetInt32(0);
                            Model.name = reader.GetString(1);
                            Model.basic_pay = reader.GetDecimal(2);
                            Model.start_Date = reader.GetDateTime(3);
                            Model.gender = Convert.ToChar(reader.GetString(4));
                            Model.phoneNumber = reader.GetString(5);
                            Model.department = reader.GetString(6);
                            Model.address = reader.GetString(7);
                            Model.deduction = reader.GetDouble(8);
                            Model.taxable = reader.GetSqlSingle(9);
                            Model.netpay = reader.GetSqlSingle(10);
                            Model.income_tax = reader.GetDouble(11);
                            Console.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}", Model.Id, Model.name, Model.basic_pay, Model.start_Date, Model.gender, Model.phoneNumber, Model.department, Model.address, Model.deduction, Model.taxable, Model.netpay, Model.income_tax);
                            count++;
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Data Found");
                    }
                    reader.Close();
                    this.Connection.Close();
                }
                return count;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
