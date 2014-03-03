using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace AventyrligaKontakter.Model.DAL
{
    public abstract class DALBase
    {
        private static string _connectionString;

        static DALBase()
        {
            _connectionString = WebConfigurationManager.ConnectionStrings["AdventurousConnectionString"].ConnectionString;
        }

        protected SqlConnection CS()
        {
            return new SqlConnection(_connectionString);
        }
    }
}