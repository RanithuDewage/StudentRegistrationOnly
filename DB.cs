using System;
using Microsoft.Data.SqlClient;

namespace StudentRegistrationOnly
{
    internal static class DB
    {
        private static string _connectionString =
            @"Server=localhost\SQLEXPRESS;Database=Student;Trusted_Connection=True;TrustServerCertificate=True;";

        public static void InitializeDatabase()
        {
            string masterConnection =
                @"Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;TrustServerCertificate=True;";

            using (SqlConnection con = new SqlConnection(masterConnection))
            {
                con.Open();

                string createDb = @"
IF DB_ID('Student') IS NULL
    CREATE DATABASE Student;
";
                using (SqlCommand cmd = new SqlCommand(createDb, con))
                {
                    cmd.ExecuteNonQuery();
                }
            }

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();

                string createTable = @"
IF OBJECT_ID('Registration', 'U') IS NULL
BEGIN
    CREATE TABLE Registration
    (
        regNo INT PRIMARY KEY,
        firstName VARCHAR(50),
        lastName VARCHAR(50),
        dateOfBirth DATETIME,
        gender VARCHAR(50),
        address VARCHAR(50),
        email VARCHAR(50),
        mobilePhone INT,
        homePhone INT,
        parentName VARCHAR(50),
        nic VARCHAR(50),
        contactNo INT
    )
END
";
                using (SqlCommand cmd = new SqlCommand(createTable, con))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}