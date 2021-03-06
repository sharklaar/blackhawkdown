﻿using BhdResponsiveSite.Models;
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

        public DatabaseHelper()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString
                .Replace("{replaceme}", Environment.GetEnvironmentVariable("BHD_SITE"));
        }

        public void WriteEmailToDatabase(EmailOnlyModel emailVm)
        {
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

        public void SoftDeleteContact(EmailOnlyModel emailVm)
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open();
            try
            {
                var cmd = new SqlCommand("RemoveContact", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", emailVm.Email);

                cmd.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }

        public string GetFreeTrackCode()
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open();
            try
            {
                var cmd = new SqlCommand("GetFreeTrackCode", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                return cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                connection.Close();
            }
        }

        public List<string> GetEmailAddresses()
        {
            var emails = new List<string>();

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

            var connection = new SqlConnection(_connectionString);
            try
            {
                connection.Open();
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
                //throw;
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