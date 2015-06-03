﻿using BhdResponsiveSite.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BhdResponsiveSite.Helpers
{
    public class DatabaseHelper
    {
        private string _connectionString;

        public void WriteEmailToDatabase(EmailOnlyModel emailVm)
        {

            _connectionString = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
            var connection = new SqlConnection(_connectionString);
            connection.Open();
            try
            {
                var cmd = new SqlCommand("AddContact", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", emailVm.Email);
                cmd.Parameters.AddWithValue("@CreatedOn", DateTime.Now);
                cmd.Parameters.AddWithValue("@FirstName", emailVm.FirstName);

                cmd.ExecuteNonQuery();                
            }
            finally
            {
                connection.Close();
            }
        }

        public List<string> GetEmailAddresses()
        {
            var emails = new List<string>();

            _connectionString = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
            var connection = new SqlConnection(_connectionString);
            connection.Open();
            try
            {
                var cmd = new SqlCommand("RetrieveAllEmailAddresses", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var email = reader[0];
                    emails.Add(email.ToString());
                }
            }
            finally
            {
                connection.Close();
            }

            return emails;
        } 
    }
}