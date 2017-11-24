using System;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace Boomcraft
{
    public class DAL
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
            dbCon.Close();
        }
        // ************************************************************************************************************************ //
        public int FV_envoyerDon(string sUUID, int iIdRessource, int iQuantite)
        {
            int iResult = -1;
            string sMessageBase = string.Empty;
            var dbCon = aDb_Connection;
            dbCon.DatabaseName = "boomcraft";
            if (dbCon.IsConnect())
            {
                //  Requête à exécuter dans la base.
                string sQuery =
                    "UPDATE boomcraft.userressource "
                    + "SET qty = qty + " + iQuantite + " WHERE id_User = " + sUUID + " AND id_Ressource = " + iIdRessource + ";";
                var cmd = new MySqlCommand(sQuery, dbCon.Connection);
                cmd.ExecuteNonQuery();
            }
            dbCon.Close();
            return iResult;
        }
        // ************************************************************************************************************************ //
        #endregion aaaaaaaaaaaa
    }
}