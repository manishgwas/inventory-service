using System;
using System.Data.SqlClient;

namespace Inventory_Management_Service
{
    public class SqlConnectivityChecker
    {
        public static bool CanConnect(string connectionString, out string error)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    error = null;
                    return true;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
    }
}
