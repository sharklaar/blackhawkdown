using BhdResponsiveSite.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

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

        public List<GigDetail> GetGigDetails()
        {
            var gigs = new List<GigDetail>();

            _connectionString = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
            var connection = new SqlConnection(_connectionString);
            connection.Open();
            try
            {
                var cmd = new SqlCommand("GetGigDetails", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var gigDetail = new GigDetail();
                    if (!string.IsNullOrEmpty(reader[1].ToString()))
                    {
                        gigDetail.GigDate = (DateTime) reader[1];
                    }
                    if (!string.IsNullOrEmpty(reader[2].ToString()))
                    {
                        gigDetail.Venue = (string) reader[2];
                    }
                    if (!string.IsNullOrEmpty(reader[3].ToString()))
                    {
                        gigDetail.LinkUrl = (string) reader[3];
                    }
                    if (!string.IsNullOrEmpty(reader[4].ToString()))
                    {
                        gigDetail.Location = (string) reader[4];
                    }
                    if (!string.IsNullOrEmpty(reader[5].ToString()))
                    {
                        gigDetail.ExtraDetail = (string) reader[5];
                    }
                    gigs.Add(gigDetail);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new List<GigDetail>();
            }
            finally
            {
                connection.Close();
            }

            return gigs;
        }
    }
}