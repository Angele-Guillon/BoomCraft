using System;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;

namespace Boomcraft
{
    public class Boomcraft_Repository
    {
        #region VARIABLES ET CONSTRUCTEUR
        // ************************************************** VARIABLES ************************************************** //
        //  Déclaration d'une chaîne de connexion.
        DB_Connection aDb_Connection = new DB_Connection();
        //  Déclaration de la chaîne de connexion à la base de données locale.
        MySqlConnection sConnexionLocal = null;
        // ************************************************** CONSTRUCTEUR ************************************************** //
        public Boomcraft_Repository()
        {
            sConnexionLocal = new MySqlConnection(ConfigurationManager.ConnectionStrings["sConnexionLocal"].ConnectionString);
            //sConnexionLocal = new MySqlConnection("server = localhost; database = boomcraft; uid = root; pwd = toor");
            //sConnexionLocal = new MySqlConnection("Server = localhost; database = boomcraft; uid = admin; pwd = admin");
        }
        // **************************************************  ************************************************** //
        #endregion VARIABLES ET CONSTRUCTEUR
        #region APPELS DE PROCEDURES
        // ************************************************************************+-************************************************ //
        public int FV_envoyerDon(string sId_Global, int iIdRessource, int iQuantite)
        {
            //  Variable de retour qui indique le nombre de lignes qui ont été affectées par la requête.
            int iResult = 0;
            if (sConnexionLocal.State == ConnectionState.Closed)
            {
                //  Ouverture d'une connexion avec la base.
                sConnexionLocal.Open();
            }
            //  Déclaration d'un objet MySqlCommand pour appeler une procédure stockée.
            MySqlCommand cmd = new MySqlCommand("ps_Update_Ressources", sConnexionLocal);
            cmd.CommandType = CommandType.StoredProcedure;
            //  Transmission des paramètres à la procédure stockée.
            cmd.Parameters.AddWithValue("@sId_Global", sId_Global);
            cmd.Parameters.AddWithValue("@iIdRessource", iIdRessource);
            cmd.Parameters.AddWithValue("@iQuantite", iQuantite);
            //  Exécution de la procédure stockée.
            cmd.ExecuteNonQuery();
            //  Fermeture de la connexion avec la base.
            sConnexionLocal.Close();
            return iResult;
        }
        // ************************************************************************************************************************ //
        public DataSet GetAll_Joueur()
        //  Retourne toutes les informations de tous les joueurs.
        {
            DataSet ds = new DataSet();
            if (sConnexionLocal.State == ConnectionState.Closed)
            {
                //  Ouverture d'une connexion avec la base.
                sConnexionLocal.Open();
            }
            //  Déclaration d'un objet MySqlCommand pour appeler une procédure stockée.
            MySqlCommand cmd = new MySqlCommand("ps_getAll_Account", sConnexionLocal);
            cmd.CommandType = CommandType.StoredProcedure;
            //  Exécution de la procédure stockée.
            cmd.ExecuteNonQuery();
            //  Récupération des données de la procédures dans un adapter.
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.SelectCommand = cmd;
            //  Stockage des données dans un dataset.
            adapter.Fill(ds);
            //  Fermeture de la connexion avec la base.
            sConnexionLocal.Close();
            return ds;
        }
        // ************************************************************************************************************************ //
        public int Insert_Joueur(string sId_Global, string sNom, string sMdp, string sEmail, DateTime dtCreation, DateTime? dtEdition, DateTime? dtSuppression, int iFaction)
        {
            //  Retourne l'id de l'account créé si la requête a fonctionné.
            //  Retourne l'erreur s'il la requête n'a pas fonctionné.
            string sErreur = string.Empty;
            int iIdResult = 0;
            try
            {
                if (sConnexionLocal.State == ConnectionState.Closed)
                {
                    //  Ouverture d'une connexion avec la base.
                    sConnexionLocal.Open();
                }
                //  Déclaration d'un objet MySqlCommand pour appeler une procédure stockée.
                MySqlCommand cmd = new MySqlCommand("ps_Insert_Account", sConnexionLocal);
                cmd.CommandType = CommandType.StoredProcedure;
                //  Déclaration des paramètres d'entrée de la procédure.
                cmd.Parameters.AddWithValue("@sId_Global", sId_Global);
                cmd.Parameters.AddWithValue("@sNom", sNom);
                cmd.Parameters.AddWithValue("@sMdp", sMdp);
                cmd.Parameters.AddWithValue("@sEmail", sEmail);
                cmd.Parameters.AddWithValue("@dtCreation", dtCreation);
                cmd.Parameters.AddWithValue("@dtEdition", dtEdition);
                cmd.Parameters.AddWithValue("@dtSuppression", dtSuppression);
                cmd.Parameters.AddWithValue("@iFaction", iFaction);
                //  Déclaration des paramètres d'entrée de la procédure.
                MySqlParameter out_Id = new MySqlParameter("@out_Id", MySqlDbType.Int16);
                out_Id.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(out_Id);

                //  Exécution de la procédure stockée.
                cmd.ExecuteNonQuery();
                //  Récupération du dernier id créé.
                iIdResult = int.Parse(out_Id.Value.ToString());
                //  Fermeture de la connexion avec la base.
                sConnexionLocal.Close();
            }
            catch (Exception ex)
            {
                //  Erreur dans l'exécution du traitement SQL.
                 sErreur = "\n" + ex.ToString();
            }
            return iIdResult;
        }
        // ************************************************************************************************************************ //
        public int Update_Joueur(int iId, string sId_Global, string sNom, string sMdp, string sEmail, DateTime dtCreation, DateTime? dtEdition, DateTime? dtSuppression, int iFaction)
        //  Met à jour toutes les informations d'un joueur à partir de son id (local).
        {
            //  Variable de retour qui indique le nombre de lignes qui ont été affectées par la requête.
            int iResult = 0;
            if (sConnexionLocal.State == ConnectionState.Closed)
            {
                //  Ouverture d'une connexion avec la base.
                sConnexionLocal.Open();
            }
            //  Déclaration d'un objet MySqlCommand pour appeler une procédure stockée.
            MySqlCommand cmd = new MySqlCommand("ps_Update_Account", sConnexionLocal);
            cmd.CommandType = CommandType.StoredProcedure;
            //  Transmission des paramètres à la procédure stockée.
            cmd.Parameters.AddWithValue("@iId", iId);
            cmd.Parameters.AddWithValue("@sId_Global", sId_Global);
            cmd.Parameters.AddWithValue("@sNom", sNom);
            cmd.Parameters.AddWithValue("@sMdp", sMdp);
            cmd.Parameters.AddWithValue("@sEmail", sEmail);
            cmd.Parameters.AddWithValue("@dtCreation", dtCreation);
            cmd.Parameters.AddWithValue("@dtEdition", dtEdition);
            cmd.Parameters.AddWithValue("@dtSuppression", dtSuppression);
            cmd.Parameters.AddWithValue("@iFaction", iFaction);
            //  Exécution de la procédure stockée.
            iResult = cmd.ExecuteNonQuery();
            //  Fermeture de la connexion avec la base.
            sConnexionLocal.Close();
            return iResult;
        }
        // ************************************************************************************************************************ //
        public int Delete_Joueur(int iId)
        //  Supprime un joueur à partir de son id (local).
        //  Renvoie le nombre de lignes affectées par la requête.
        {
            int iResult = 0;
            if (sConnexionLocal.State == ConnectionState.Closed)
            {
                //  Ouverture d'une connexion avec la base.
                sConnexionLocal.Open();
            }
            //  Déclaration d'un objet MySqlCommand pour appeler une procédure stockée.
            MySqlCommand cmd = new MySqlCommand("ps_Delete_Account", sConnexionLocal);
            cmd.CommandType = CommandType.StoredProcedure;
            //  Transmission des paramètres à la procédure stockée.
            cmd.Parameters.AddWithValue("@iId", iId);
            //  Exécution de la procédure stockée.
            iResult = cmd.ExecuteNonQuery();
            //  Fermeture de la connexion avec la base.
            sConnexionLocal.Close();
            return iResult;
        }
        // ************************************************************************************************************************ //
        public void FV_DoTransaction(string sUUID, int iWood, int iFood, int iGold, int iRock, int iCentralise)
        {
            //  TODO : EST-CE QUE LA STRUCTURE DE LA BASE EST PERTINENTE ??????????????????
            //string sMessageBase = string.Empty;
            //var dbCon = aDb_Connection;
            //dbCon.DatabaseName = "boomcraft";
            //if (dbCon.IsConnect())
            //{
            //    //  Requête à exécuter dans la base.
            //    string sQuery =
            //        "UPDATE boomcraft.userressource "
            //            + "SET qty = qty + " + iQuantite + " WHERE id_User = " + sUUID + " AND id_Ressource = " + iIdRessource + ";";
            //    var cmd = new MySqlCommand(sQuery, dbCon.Connection);
            //    cmd.ExecuteNonQuery();
            //}
            //dbCon.Close();
            //return iResult;
        }
        // ************************************************************************************************************************ //
        #endregion APPELS DE PROCEDURES
        #region COMMUNICATION BASE AVEC CLASSE DB_CONNECTION
        // ************************************************************************************************************************ //
        //public void GetALL_Test()
        //{
        //    var dbCon = aDb_Connection;
        //    dbCon.DatabaseName = "boomcraft";
        //    if (dbCon.IsConnect())
        //    {
        //        //  Requête à exécuter dans la base.
        //        string query = "SELECT * FROM test";
        //        var cmd = new MySqlCommand(query, dbCon.Connection);
        //        var reader = cmd.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            string sId = reader.GetString(0);
        //            string sNom = reader.GetString(1);
        //            string sAge = reader.GetString(2);
        //            Console.WriteLine("\n" + sId + "," + sNom + "," + sAge);
        //        }
        //    }
        //    dbCon.Close();
        //}
        // ************************************************************************************************************************ //
        #endregion COMMUNICATION BASE AVEC CLASSE DB_CONNECTION
    }
}