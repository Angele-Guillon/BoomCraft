using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.Runtime.InteropServices;


namespace Boomcraft
{
    public class Test
    {
        // ************************************************** VARIABLES ************************************************** //
        DB_Connection aDb_Connection = new DB_Connection();
        // **************************************************  ************************************************** //
        #region aaaaaaaaaaaa
        // ************************************************************************************************************************ //
        public void GetALL_Test()
        {
            var dbCon = aDb_Connection;
            dbCon.DatabaseName = "boomcraft";
            if (dbCon.IsConnect())
            {
                //  Requête à exécuter dans la base.
                string query = "SELECT * FROM test";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string sId = reader.GetString(0);
                    string sNom = reader.GetString(1);
                    string sAge = reader.GetString(2);
                    Console.WriteLine("\n" + sId + "," + sNom + "," + sAge);
                }
            }
        }
        // ************************************************************************************************************************ //
        public long Insert_Test(string sNom, int iAge)
        {
            long iResult = -1;
            var dbCon = aDb_Connection;
            dbCon.DatabaseName = "boomcraft";
            if (dbCon.IsConnect())
            {
                //  Requête à exécuter dans la base.
                //string sQuery = "CALL boomcraft.ps_Insert_Test('" +sNom + "', " +iAge + ");";

                string sQuery = "INSERT INTO boomcraft.test(nom, age) VALUES('" + sNom + "', " + iAge + ")";
                var cmd = new MySqlCommand(sQuery, dbCon.Connection);
                cmd.ExecuteNonQuery();
                //  Fonctionne avec un INSERT en dur dans le code.
                //long id = cmd.LastInsertedId;
                iResult = cmd.LastInsertedId;
            }
            return iResult;
        }
        // ************************************************************************************************************************ //
        #endregion aaaaaaaaaaaa
    }
}