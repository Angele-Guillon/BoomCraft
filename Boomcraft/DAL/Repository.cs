using System;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;

using Boomcraft.METIER;

namespace Boomcraft.DAL
{
    public class Repository
    {
        #region VARIABLES ET CONSTRUCTEUR
        // ************************************************** VARIABLES ************************************************** //
        //  Déclaration de la chaîne de connexion à la base de données locale.
        private MySqlConnection sConnexionLocal = null;
        //  Instanciation de la classe Log pour pouvoir générer des logs lors de l'exécution du code.
        Log aLog = new Log();
        // ************************************************** CONSTRUCTEUR ************************************************** //
        public Repository()
        {
            sConnexionLocal = new MySqlConnection(ConfigurationManager.ConnectionStrings["sConnexionLocal"].ConnectionString);
            //sConnexionLocal = new MySqlConnection("server = localhost; database = boomcraft; uid = root; pwd = toor");
            //sConnexionLocal = new MySqlConnection("Server = localhost; database = boomcraft; uid = admin; pwd = admin");
        }
        // **************************************************  ************************************************** //
        #endregion VARIABLES ET CONSTRUCTEUR
        #region JOUEUR
        // ************************************************** CHECK EXISTENCE JOUEUR ************************************************** //
        public string Check_ExistenceJoueur(string sNom, string sEmail)
        {
            //  Renvoie un string qui indique si le nom et l'email existent déjà.
            string sResult = string.Empty;
            if (sConnexionLocal.State == ConnectionState.Closed)
            {
                //  Ouverture d'une connexion avec la base.
                sConnexionLocal.Open();
            }
            //  Déclaration d'un objet MySqlCommand pour appeler une procédure stockée.
            MySqlCommand cmd = new MySqlCommand("ps_Existence_Account", sConnexionLocal);
            cmd.CommandType = CommandType.StoredProcedure;
            //  Déclaration des paramètres d'entrée de la procédure.
            cmd.Parameters.AddWithValue("@sNom", sNom);
            cmd.Parameters.AddWithValue("@sEmail", sEmail);
            //  Déclaration des paramètres d'entrée de la procédure.
            MySqlParameter out_sExistenceNom = new MySqlParameter("@out_sExistenceNom", MySqlDbType.VarChar);
            out_sExistenceNom.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(out_sExistenceNom);
            MySqlParameter out_sExistenceEmail = new MySqlParameter("@out_sExistenceEmail", MySqlDbType.VarChar);
            out_sExistenceEmail.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(out_sExistenceEmail);
            //  Exécution de la procédure stockée.
            cmd.ExecuteNonQuery();
            //  Construction d'un string de format JSON avec les retours de la procédure.
            sResult = "{ 'username': " + out_sExistenceNom.Value.ToString().ToLower() + ", 'email': "
                + out_sExistenceEmail.Value.ToString().ToLower() + " }";
            //  Fermeture de la connexion avec la base.
            sConnexionLocal.Close();
            return sResult;
        }
        // ************************************************** GET JOUEUR BY LOGIN ************************************************** //
        public DataSet Get_JoueurByLogin(string sNom, string sMdp)
        //  Renvoie les informations d'un joueur correspondant au couple (USERNAME - PASSWORD).
        {
            DataSet ds = new DataSet();
            if (sConnexionLocal.State == ConnectionState.Closed)
            {
                //  Ouverture d'une connexion avec la base.
                sConnexionLocal.Open();
            }
            //  Déclaration d'un objet MySqlCommand pour appeler une procédure stockée.
            MySqlCommand cmd = new MySqlCommand("ps_Get_JoueurByLogin", sConnexionLocal);
            cmd.CommandType = CommandType.StoredProcedure;
            //  Déclaration des paramètres d'entrée de la procédure.
            cmd.Parameters.AddWithValue("@sNom", sNom);
            cmd.Parameters.AddWithValue("@sMdp", sMdp);
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
        // ************************************************** GET ALL JOUEUR ************************************************** //
        public DataSet GetAll_Joueur()
        //  Renvoie la liste des joueurs présents dans la base.
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
        // ************************************************** INSERT JOUEUR ************************************************** //
        public int Insert_Joueur(string sId_Global, string sNom, string sEmail, string sMdp, DateTime dtCreation, DateTime? dtEdition, DateTime? dtSuppression, int iFaction)
        {
            //  Retourne l'id de l'account créé si la requête a fonctionné.
            //  Retourne l'erreur s'il la requête n'a pas fonctionné.
            int iIdResult = 0;
            //  variable pour stocker l'erreur en cas d'échec de la procédure.
            string sErreur = string.Empty;
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
                MySqlParameter out_Id = new MySqlParameter("@out_Id", MySqlDbType.Int32);
                out_Id.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(out_Id);
                //  Exécution de la procédure stockée.
                cmd.ExecuteNonQuery();
                //  Récupération du dernier id créé.
                iIdResult = Int32.Parse(out_Id.Value.ToString());
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
        // ************************************************** UPDATE JOUEUR ************************************************** //
        public int Update_Joueur(int iId, string sNom, string sMdp, string sEmail, string sFaction)
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
            cmd.Parameters.AddWithValue("@sNom", sNom);
            cmd.Parameters.AddWithValue("@sMdp", sMdp);
            cmd.Parameters.AddWithValue("@sEmail", sEmail);
            cmd.Parameters.AddWithValue("@sFaction", sFaction);
            //  Exécution de la procédure stockée.
            iResult = cmd.ExecuteNonQuery();
            //  Fermeture de la connexion avec la base.
            sConnexionLocal.Close();
            return iResult;
        }
        // ************************************************** DELETE JOUEUR ************************************************** //
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
        #endregion JOUEUR
        #region BOOMCRAFT FACTION
        // ************************************************** GET ALL FACTION ************************************************** //
        public DataSet GetAll_Faction()
        //  Renvoie la liste des factions présentes dans la base.
        {
            DataSet ds = new DataSet();
            if (sConnexionLocal.State == ConnectionState.Closed)
            {
                //  Ouverture d'une connexion avec la base.
                sConnexionLocal.Open();
            }
            //  Déclaration d'un objet MySqlCommand pour appeler une procédure stockée.
            MySqlCommand cmd = new MySqlCommand("ps_GetAll_Faction", sConnexionLocal);
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
        #endregion BOOMCRAFT FACTION
        #region BOOMCRAFT RESSOURCE
        // ************************************************** INSERT RESSOURCE JOUEUR ************************************************** //
        public void Insert_RessourceJoueur(int iIdJoueur)
        {
            //  Insert des ressources à la quantité nulle dans la table userressource.
            if (sConnexionLocal.State == ConnectionState.Closed)
            {
                //  Ouverture d'une connexion avec la base.
                sConnexionLocal.Open();
            }
            //  Déclaration d'un objet MySqlCommand pour appeler une procédure stockée.
            MySqlCommand cmd = new MySqlCommand("ps_Insert_AccountRessource", sConnexionLocal);
            cmd.CommandType = CommandType.StoredProcedure;
            //  Déclaration des paramètres d'entrée de la procédure.
            cmd.Parameters.AddWithValue("@iIdJoueur", iIdJoueur);
            //  Exécution de la procédure stockée.
            cmd.ExecuteNonQuery();
            //  Fermeture de la connexion avec la base.
            sConnexionLocal.Close();
        }
        // ************************************************** GET ALL RESSOURCE ************************************************** //
        public DataSet GetAll_Ressource()
        //  Renvoie la liste des ressources présentes dans la base.
        {
            DataSet ds = new DataSet();
            if (sConnexionLocal.State == ConnectionState.Closed)
            {
                //  Ouverture d'une connexion avec la base.
                sConnexionLocal.Open();
            }
            //  Déclaration d'un objet MySqlCommand pour appeler une procédure stockée.
            MySqlCommand cmd = new MySqlCommand("ps_GetAll_Ressource", sConnexionLocal);
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
        // ************************************************** GET ALL RESSOURCE JOUEUR ************************************************** //
        public DataSet GetAll_RessourceJoueur(int iIdJoueur)
        //  Renvoie la liste des ressources présentes dans la base.
        {
            DataSet ds = new DataSet();
            if (sConnexionLocal.State == ConnectionState.Closed)
            {
                //  Ouverture d'une connexion avec la base.
                sConnexionLocal.Open();
            }
            //  Déclaration d'un objet MySqlCommand pour appeler une procédure stockée.
            MySqlCommand cmd = new MySqlCommand("ps_GetAll_RessourceByAccount", sConnexionLocal);
            cmd.CommandType = CommandType.StoredProcedure;
            //  Déclaration des paramètres d'entrée de la procédure.
            cmd.Parameters.AddWithValue("@iIdJoueur", iIdJoueur);
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
        #endregion BOOMCRAFT RESSOURCE
        #region BOOMCRAFT BATIMENT
        // ************************************************** GET ALL BATIMENT JOUEUR ************************************************** //
        public DataSet GetAll_BatimentJoueur(int iIdJoueur)
        //  Renvoie la liste des bâtiments que possède un joueur.
        {
            DataSet ds = new DataSet();
            if (sConnexionLocal.State == ConnectionState.Closed)
            {
                //  Ouverture d'une connexion avec la base.
                sConnexionLocal.Open();
            }
            //  Déclaration d'un objet MySqlCommand pour appeler une procédure stockée.
            MySqlCommand cmd = new MySqlCommand("ps_GetAll_BuildingByAccount", sConnexionLocal);
            cmd.CommandType = CommandType.StoredProcedure;
            //  Déclaration des paramètres d'entrée de la procédure.
            cmd.Parameters.AddWithValue("@iIdJoueur", iIdJoueur);
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
        #endregion BOOMCRAFT BATIMENT
        #region BOOMCRAFT DEMANDE TROUPE
        // ************************************************** GET ALL DEMANDE TROUPE ************************************************** //
        public DataSet GetAll_DemandeTroupe()
        //  Renvoie la liste des bâtiments que possède un joueur.
        {
            DataSet ds = new DataSet();
            if (sConnexionLocal.State == ConnectionState.Closed)
            {
                //  Ouverture d'une connexion avec la base.
                sConnexionLocal.Open();
            }
            //  Déclaration d'un objet MySqlCommand pour appeler une procédure stockée.
            MySqlCommand cmd = new MySqlCommand("ps_GetAll_TroopRequest", sConnexionLocal);
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
        // ************************************************** DELETE DEMANDE TROUPE ************************************************** //
        public void Delete_DemandeTroupe(string sUUID)
        //  Supprime une demande de troupes.
        {
            int iResult = 0;
            if (sConnexionLocal.State == ConnectionState.Closed)
            {
                //  Ouverture d'une connexion avec la base.
                sConnexionLocal.Open();
            }
            //  Déclaration d'un objet MySqlCommand pour appeler une procédure stockée.
            MySqlCommand cmd = new MySqlCommand("ps_Delete_TroopRequest", sConnexionLocal);
            cmd.CommandType = CommandType.StoredProcedure;
            //  Transmission des paramètres à la procédure stockée.
            cmd.Parameters.AddWithValue("@sUUID", sUUID);
            //  Exécution de la procédure stockée.
            iResult = cmd.ExecuteNonQuery();
            //  Fermeture de la connexion avec la base.
            sConnexionLocal.Close();
        }
        // ************************************************************************************************************************ //
        #endregion BOOMCRAFT DEMANDE TROUPE
        #region COMBAT
        //***************************************************** INSERT COMBAT *********************************************************//
        public int Insert_Combat(int iIdCombat, int iIdAttaquant, int iIdDefenseur, int iDureeAvantCombat, int iIdVainqueur)
        //  Renvoie la liste des combat présents dans la base.
        {
            //  Retourne l'id de l'account créé si la requête a fonctionné.
            //  Retourne l'erreur s'il la requête n'a pas fonctionné.
            int iIdResult = 0;
            //  variable pour stocker l'erreur en cas d'échec de la procédure.
            string sErreur = string.Empty;
            try
            {
                if (sConnexionLocal.State == ConnectionState.Closed)
                {
                    //  Ouverture d'une connexion avec la base.
                    sConnexionLocal.Open();
                }
                //  Déclaration d'un objet MySqlCommand pour appeler une procédure stockée.
                MySqlCommand cmd = new MySqlCommand("ps_Insert_Combat", sConnexionLocal);
                cmd.CommandType = CommandType.StoredProcedure;
                //  Déclaration des paramètres d'entrée de la procédure.
                cmd.Parameters.AddWithValue("@iIdCombat", iIdCombat);
                cmd.Parameters.AddWithValue("@iIdAttaquant", iIdAttaquant);
                cmd.Parameters.AddWithValue("@iIdDefenseur", iIdDefenseur);
                cmd.Parameters.AddWithValue("@iDureeAvantCombat", iDureeAvantCombat);
                cmd.Parameters.AddWithValue("@iIdVainqueur", iIdVainqueur);
                //  Exécution de la procédure stockée.
                cmd.ExecuteNonQuery();
                //  Récupération du dernier id créé.

                MySqlParameter out_Id = new MySqlParameter("@out_Id", MySqlDbType.Int32);
                out_Id.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(out_Id);
                //  Exécution de la procédure stockée.
                cmd.ExecuteNonQuery();
                //  Récupération du dernier id créé.
                iIdResult = Int32.Parse(out_Id.Value.ToString());
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
        //****************************************************** GET COMBAT *********************************************************//
        public DataSet Get_Combat()
        //  Renvoie la liste des joueurs présents dans la base.
        {
            DataSet ds = new DataSet();
            if (sConnexionLocal.State == ConnectionState.Closed)
            {
                //  Ouverture d'une connexion avec la base.
                sConnexionLocal.Open();
            }
            //  Déclaration d'un objet MySqlCommand pour appeler une procédure stockée.
            MySqlCommand cmd = new MySqlCommand("ps_Get_Combat", sConnexionLocal);
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
        //****************************************************** GET ALL COMBAT *********************************************************//
        public DataSet GetAll_Combat()
        //  Renvoie la liste des combat présents dans la base.
        {
            DataSet ds = new DataSet();
            if (sConnexionLocal.State == ConnectionState.Closed)
            {
                //  Ouverture d'une connexion avec la base.
                sConnexionLocal.Open();
            }
            //  Déclaration d'un objet MySqlCommand pour appeler une procédure stockée.
            MySqlCommand cmd = new MySqlCommand("ps_GetAll_Combat", sConnexionLocal);
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
        //****************************************************** GET ALL JOUEUR PAS COMBAT *********************************************************//
        public DataSet GetAll_JoueurPasCombat()
        //  Renvoie la liste des combat présents dans la base.
        {
            DataSet ds = new DataSet();
            if (sConnexionLocal.State == ConnectionState.Closed)
            {
                //  Ouverture d'une connexion avec la base.
                sConnexionLocal.Open();
            }
            //  Déclaration d'un objet MySqlCommand pour appeler une procédure stockée.
            MySqlCommand cmd = new MySqlCommand("ps_GetAll_AccountNotInCombat", sConnexionLocal);
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
        //***************************************************** INSERT COMBAT *********************************************************//
        public int Insert_ParticiperCombat(int iIdAttaque, string sUUIDHowob, int iFaction, string sStats)
        //  Insert un attaquant ou un defenseur Howob dans un combat.
        {
            //  Retourne l'id du combat modifié.
            int iIdResult = 0;
            if (sConnexionLocal.State == ConnectionState.Closed)
            {
                //  Ouverture d'une connexion avec la base.
                sConnexionLocal.Open();
            }
            //  Déclaration d'un objet MySqlCommand pour appeler une procédure stockée.
            MySqlCommand cmd = new MySqlCommand("ps_Insert_ParticiperCombat", sConnexionLocal);
            cmd.CommandType = CommandType.StoredProcedure;
            //  Déclaration des paramètres d'entrée de la procédure.
            cmd.Parameters.AddWithValue("@iIdAttaque", iIdAttaque);
            cmd.Parameters.AddWithValue("@sUUIDHowob", sUUIDHowob);
            cmd.Parameters.AddWithValue("@iFaction", iFaction);
            cmd.Parameters.AddWithValue("@sStats", sStats);
            //  Récupération de l'id du combat modifié.
            MySqlParameter out_IdCombat = new MySqlParameter("@out_IdCombat", MySqlDbType.Int32);
            out_IdCombat.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(out_IdCombat);
            //  Exécution de la procédure stockée.
            cmd.ExecuteNonQuery();
            //  Récupération du dernier id créé.
            iIdResult = Int32.Parse(out_IdCombat.Value.ToString());
            //  Fermeture de la connexion avec la base.
            sConnexionLocal.Close();
            return iIdResult;

        }
        // ************************************************************************************************************************ //
        #endregion COMBAT
        #region FARMVILLAGE
        // ************************************************** FV ENVOYER DON ************************************************** //
        public string[] FV_EnvoyerDon(string sUUID, int iQteWood, int iQteFood, int iQteGold, int iQteRock)
        //  Permet à un joueur de Farmvillage de faire un don de rssources à un joueur de Boomcraft.
        {
            //  Variable de retour qui indique le nombre de lignes qui ont été affectées par la requête.
            string[] sResult = new string[2];
            if (sConnexionLocal.State == ConnectionState.Closed)
            {
                //  Ouverture d'une connexion avec la base.
                sConnexionLocal.Open();
            }
            //  Déclaration d'un objet MySqlCommand pour appeler une procédure stockée.
            MySqlCommand cmd = new MySqlCommand("ps_Update_Ressources", sConnexionLocal);
            cmd.CommandType = CommandType.StoredProcedure;
            //  Transmission des paramètres à la procédure stockée.
            cmd.Parameters.AddWithValue("@sUUID", sUUID);
            cmd.Parameters.AddWithValue("@iQteWood", iQteWood);
            cmd.Parameters.AddWithValue("@iQteFood", iQteFood);
            cmd.Parameters.AddWithValue("@iQteGold", iQteGold);
            cmd.Parameters.AddWithValue("@iQteRock", iQteRock);
            //  Déclaration des paramètres de sortie de la procédure.
            MySqlParameter out_sReceptionResult = new MySqlParameter("@out_sReceptionResult", MySqlDbType.VarChar);
            out_sReceptionResult.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(out_sReceptionResult);
            MySqlParameter out_sReceptionMessage = new MySqlParameter("@out_sReceptionMessage", MySqlDbType.VarChar);
            out_sReceptionMessage.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(out_sReceptionMessage);
            //  Exécution de la procédure stockée.
            cmd.ExecuteNonQuery();
            //  Récupération du dernier id créé.
            sResult[0] = out_sReceptionResult.Value.ToString();
            sResult[1] = out_sReceptionMessage.Value.ToString();
            //  Fermeture de la connexion avec la base.
            sConnexionLocal.Close();
            return sResult;
        }
        // ************************************************** FV DEMANDER TROUPE ************************************************** //
        public int FV_DemanderTroupe(string sUUID, int iFaction, int iQuantite)
        //  Permet à un joueur de Farmvillage de faire une demande de troupes à un joueur de Boomcraft.
        {
            //  Variable de retour qui indique le nombre de lignes qui ont été affectées par la requête.
            int iIdResult = 0;
            if (sConnexionLocal.State == ConnectionState.Closed)
            {
                //  Ouverture d'une connexion avec la base.
                sConnexionLocal.Open();
            }
            //  Déclaration d'un objet MySqlCommand pour appeler une procédure stockée.
            MySqlCommand cmd = new MySqlCommand("ps_Insert_TroopRequest", sConnexionLocal);
            cmd.CommandType = CommandType.StoredProcedure;
            //  Transmission des paramètres à la procédure stockée.
            cmd.Parameters.AddWithValue("@sUUID", sUUID);
            cmd.Parameters.AddWithValue("@iFaction", iFaction);
            cmd.Parameters.AddWithValue("@iQuantite", iQuantite);
            //  Déclaration des paramètres de sortie de la procédure.
            MySqlParameter out_iIdDemandeTroupe = new MySqlParameter("@out_iIdDemandeTroupe", MySqlDbType.Int32);
            out_iIdDemandeTroupe.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(out_iIdDemandeTroupe);
            //  Exécution de la procédure stockée.
            cmd.ExecuteNonQuery();
            //  Récupération du dernier id créé.
            iIdResult = Int32.Parse(out_iIdDemandeTroupe.Value.ToString());
            //  Fermeture de la connexion avec la base.
            sConnexionLocal.Close();
            return iIdResult;
        }
        // ************************************************************************************************************************ //
        #endregion FARMVILLAGE
        #region HOWOB
        #endregion HOWOB
        #region VEGGIECRUSH
        // ************************************************** VC UPDATE JOUEUR BONUS ************************************************** //
        public int VC_Update_JoueurBonus(string sUUID, int iQuantite)
        //  Renvoie un booléen qui indique si le bonus a été consommé ou non.
        {
            int iResult = 0;
            if (sConnexionLocal.State == ConnectionState.Closed)
            {
                //  Ouverture d'une connexion avec la base.
                sConnexionLocal.Open();
            }
            //  Déclaration d'un objet MySqlCommand pour appeler une procédure stockée.
            MySqlCommand cmd = new MySqlCommand("ps_Update_AccountBonus", sConnexionLocal);
            cmd.CommandType = CommandType.StoredProcedure;
            //  Déclaration des paramètres d'entrée de la procédure.
            cmd.Parameters.AddWithValue("@sUUID", sUUID);
            cmd.Parameters.AddWithValue("@iQuantite", iQuantite);
            //  Déclaration des paramètres d'entrée de la procédure.
            MySqlParameter out_iConsommationBonus = new MySqlParameter("@out_iConsommationBonus", MySqlDbType.Int32);
            out_iConsommationBonus.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(out_iConsommationBonus);
            //  Exécution de la procédure stockée.
            cmd.ExecuteNonQuery();
            //  Récupération de la consommation.
            iResult = Int32.Parse(out_iConsommationBonus.Value.ToString());
            //  Fermeture de la connexion avec la base.
            sConnexionLocal.Close();
            return iResult;
        }
        // ************************************************** GET JOUEUR BONUS ************************************************** //
        public int Get_JoueurBonus(string sUUID)
        //  Renvoie la quantité de bonus possédée par un joueur.
        {
            int iQuantite = 0;
            if (sConnexionLocal.State == ConnectionState.Closed)
            {
                //  Ouverture d'une connexion avec la base.
                sConnexionLocal.Open();
            }
            //  Déclaration d'un objet MySqlCommand pour appeler une procédure stockée.
            MySqlCommand cmd = new MySqlCommand("ps_Get_AccountBonus", sConnexionLocal);
            cmd.CommandType = CommandType.StoredProcedure;
            //  Déclaration des paramètres d'entrée de la procédure.
            cmd.Parameters.AddWithValue("@sUUID", sUUID);
            //  Déclaration des paramètres de sortie de la procédure.
            MySqlParameter out_iConsommationBonus = new MySqlParameter("@out_iConsommationBonus", MySqlDbType.Int32);
            out_iConsommationBonus.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(out_iConsommationBonus);
            //  Exécution de la procédure stockée.
            cmd.ExecuteNonQuery();
            //  Récupération de la quantité de bonus que le joueur possède.
            iQuantite = Int32.Parse(out_iConsommationBonus.Value.ToString());
            //  Fermeture de la connexion avec la base.
            sConnexionLocal.Close();
            return iQuantite;
        }
        // **************************************************  ************************************************** //
        #endregion VEGGIECRUSH
    }
}
