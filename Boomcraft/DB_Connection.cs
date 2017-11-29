using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Boomcraft
{
    public class DB_Connection
    {
        public DB_Connection()
        {
        }

        private string databaseName = "boomcraft";
        public string DatabaseName
        {
            get { return databaseName; }
            set { databaseName = value; }
        }

        public string Password { get; set; }
        private MySqlConnection connection = null;
        public MySqlConnection Connection
        {
            get { return connection; }
        }

        private static DB_Connection _instance = null;
        public static DB_Connection Instance()
        {
            if (_instance == null)
                _instance = new DB_Connection();
            return _instance;
        }

        public bool IsConnect()
        {
            bool result = true;
            if (Connection == null)
            {
                if (String.IsNullOrEmpty(databaseName))
                    result = false;
                //string connstring = string.Format("Server=192.168.0.1; database={0}; UID=admin; password=admin", databaseName);
                string connstring = string.Format("Server=localhost; database={0}; UID=root; password=toor", databaseName);
                connection = new MySqlConnection(connstring);
                connection.Open();
                result = true;
            }
            return result;
        }
        public void Close()
        {
            connection.Close();
        }
    }
}
