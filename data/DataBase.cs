using System;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace simpleCRUD.data
{
    public class DataBase
    {
        public static MySqlConnection DataBaseConnector()
        {
            string connectionString = ConfigurationManager.AppSettings.Get("connectionString");
            MySqlConnection connection = new MySqlConnection(connectionString);
            
            return connection;
        }
    }
}