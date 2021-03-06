using System;
using System.Data;
using Newtonsoft.Json;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using Boomcraft.DAL;
using Boomcraft.METIER;

namespace Boomcraft
{
    /// <summary>
    /// Description résumée de api
    /// </summary>
    //[WebService(Namespace = "http://tempuri.org/")]
    [WebService(Namespace = "Boomcraft")]
    //[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    //[System.ComponentModel.ToolboxItem(false)]
    // Pour autoriser l'appel de ce service Web depuis un script à l'aide d'ASP.NET AJAX, supprimez les marques de commentaire de la ligne suivante. 
    [ScriptService]
    public class api : WebService
    {
        #region VARIABLES
        // ************************************************** VARIABLES ************************************************** //
        //  Instanciatio de la classe d'accès à la base.
        Repository aREP = new Repository();
        //  Instanciation de la classe métier.
        Metier aMetier = new Metier();
        //  Instanciation de la classe Log.
        Log aLog = new Log();
        // **************************************************  ************************************************** //
        #endregion VARIABLES
        #region API BASE
        // ************************************************** SIGN IN ************************************************** //
        [WebMethod]
        public void signin(string username, string password)
        //  Autres jeux => Boomcraft. Renvoie les informations d'un compte à partir d'un couple d'identifiants (NAME - PASSWORD).
        {
            string sResult = string.Empty;
            try
            {
                //  Création d'un objet joueur à l'aide du nom et du mot de passe.
                Joueur aJoueur = new Joueur(username, password);
                //  Obtention de l'objet joueur en format JSON.
                sResult = aJoueur.get_JoueurJSON();
            }
            catch (Exception ex)
            {
                //  Renvoie d'erreur en cas d'échec.
                sResult = "{ 'error': { 'message': 'Une erreur s'est produite lors de la vérification des identifiants du joueur.', 'code': 401 } }";
                aLog.ecrire(sResult + "\rDétails : \r" + ex);
            }
            //  Sérialisation de la réponse en Objet.
            Object oResult = new JavaScriptSerializer().DeserializeObject(sResult);
            //  Sérialisation de la réponse au format JSON.
            var jsonSerializer = new JsonSerializer();
            jsonSerializer.Serialize(Context.Response.Output, oResult);
            //  Formatage du retour en json.
            Context.Response.ContentType = "application/json";
            // Sends all currently buffered output to the client.
            Context.Response.Flush();
            // Gets or sets a value indicating whether to send HTTP content to the client.
            Context.Response.SuppressContent = true;
            // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.
            Context.ApplicationInstance.CompleteRequest();
            Context.Response.End();
        }
        // ************************************************** EXISTING ************************************************** //
        [WebMethod]
        public void existing(string username, string email)
        //  Autres jeux => Boomcraft. Permet de checker l'existence d'un couple d'identifiants (NAME - PASSWORD).
        {
            string sResult = string.Empty;
            try
            {
                //  Récupération du l'existence des données du joueur.
                sResult = aREP.Check_ExistenceJoueur(username, email);
            }
            catch (Exception ex)
            {
                //  Renvoie d'erreur en cas d'échec.
                sResult = "{ 'error': { 'message': 'Une erreur s'est produite lors de la vérification de l'existence du joueur.', 'code': 401 } }";
                aLog.ecrire(sResult + "\rDétails : \r" + ex);
            }
            //  Sérialisation de la réponse en Objet.
            Object oResult = new JavaScriptSerializer().DeserializeObject(sResult);
            //  Sérialisation de la réponse au format JSON.
            var jsonSerializer = new JsonSerializer();
            jsonSerializer.Serialize(Context.Response.Output, oResult);
            //  Formatage du retour en json.
            Context.Response.ContentType = "application/json";
            //  Sends all currently buffered output to the client.
            Context.Response.Flush();
            //  Gets or sets a value indicating whether to send HTTP content to the client.
            Context.Response.SuppressContent = true;
            //  Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.
            Context.ApplicationInstance.CompleteRequest();
            Context.Response.End();
        }
        // **************************************************  ************************************************** //
        #endregion API BASE
        #region API FARMVILLAGE
        // ************************************************** FV ENVOYER DON ************************************************** //
        [WebMethod]
        public void FV_EnvoyerDon(string sUUID, int iQteWood, int iQteFood, int iQteGold, int iQteRock)
        //  FarmVillage => Boomcraft. Permet aux joueurs d'envoyer des ressources suite à une demande.
        {
            string sResult = string.Empty;
            try
            {
                //  Ajoute les ressources envoyées au joueur de Boomcraft.
                string[] sReceptionResult = aREP.FV_EnvoyerDon(sUUID, iQteWood, iQteFood, iQteGold, iQteRock);
                sResult = "{\"result\": \"" + sReceptionResult[0] + "\",\"msg_code\": \"" + sReceptionResult[1] + "\"}";
            }
            catch (Exception ex)
            {
                //  Renvoie d'erreur en cas d'échec.
                sResult = "{ \"error\": { \"message\": \"Une erreur s'est produite lors du don de ressources.\", \"code\": 401 } }";
                aLog.ecrire(sResult + "\rDétails : \r" + ex);
            }
            //  Sérialisation de la réponse en Objet.
            Object oResult = new JavaScriptSerializer().DeserializeObject(sResult);
            //  Sérialisation de la réponse au format JSON.
            var jsonSerializer = new JsonSerializer();
            jsonSerializer.Serialize(Context.Response.Output, oResult);
            //  Formatage du retour en json.
            Context.Response.ContentType = "application/json";
            // Sends all currently buffered output to the client.
            Context.Response.Flush();
            // Gets or sets a value indicating whether to send HTTP content to the client.
            Context.Response.SuppressContent = true;
            // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.
            Context.ApplicationInstance.CompleteRequest();
            Context.Response.End();
        }
        // ************************************************** FV DEMANDER DON ************************************************** //
        [WebMethod]
        public void FV_DemanderTroupe(string sUUID, int iFaction, int iQuantite)
        //  FarmVillage => Boomcraft. Permet aux joueurs d'effectuer une demande de troupes.
        {
            string sResult = string.Empty;
            try
            {
                //  Ajoute les ressources envoyées au joueur de Boomcraft. iResult prend le nombre de lignes créé en base.
                int iResult = aREP.FV_DemanderTroupe(sUUID, iFaction, iQuantite);
                if (iResult > 0)
                {
                    sResult = "{\"result\":\"true\",\"msg_code\": \"La demande de troupes a bien été prise en compte.\"}";
                }
                else
                {
                    sResult = "{\"result\":\"false\",\"msg_code\": \"Ce joueur a déjà effectué une demande de troupes.\"}";
                }
            }
            catch (Exception ex)
            {
                //  Renvoie d'erreur en cas d'échec.
                sResult = "{ 'error': { 'message': 'Une erreur s est produite lors de la demande de troupes.', 'code': 401 } }";
                aLog.ecrire(sResult + "\rDétails : \r" + ex);
            }
            //  Sérialisation de la réponse en Objet.
            Object oResult = new JavaScriptSerializer().DeserializeObject(sResult);
            //  Sérialisation de la réponse au format JSON.
            var jsonSerializer = new JsonSerializer();
            jsonSerializer.Serialize(Context.Response.Output, oResult);
            //  Formatage du retour en json.
            Context.Response.ContentType = "application/json";
            // Sends all currently buffered output to the client.
            Context.Response.Flush();
            // Gets or sets a value indicating whether to send HTTP content to the client.
            Context.Response.SuppressContent = true;
            // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.
            Context.ApplicationInstance.CompleteRequest();
            Context.Response.End();
        }
        // **************************************************  ************************************************** //
        #endregion API FARMVILLAGE
        #region API VEGGIECRUSH
        // ************************************************** POST - VC CONSOMMER BONUS ************************************************** //
        [WebMethod]
        public void VC_ConsommerBonus(string UUID, int qte)
        //  VEGGIECRUSH =>  Boomcraft. Permet à un joueur Veggiecrush de consommer un bonus disponible dans la base boomcraft.
        {
            string sResult = string.Empty;
            try
            {
                //  Récupération du résultat de la demande de consommation de bonus.
                int iResult = aREP.VC_Update_JoueurBonus(UUID, qte);
                string sBooleanResult = "false";
                if (iResult == 1)
                {
                    sBooleanResult = "true";
                }
                sResult = "{ 'success': " + sBooleanResult + " }";
            }
            catch (Exception ex)
            {
                //  Renvoie d'erreur en cas d'échec.
                sResult = "{ 'error': { 'message': 'Une erreur s est produite lors de la vérification des identifiants du joueur.', 'code': 401 } }";
                aLog.ecrire(sResult + "\rDétails : \r" + ex);
            }
            //  Sérialisation de la réponse en Objet.
            Object oResult = new JavaScriptSerializer().DeserializeObject(sResult);
            //  Sérialisation de la réponse au format JSON.
            var jsonSerializer = new JsonSerializer();
            jsonSerializer.Serialize(Context.Response.Output, oResult);
            //  Formatage du retour en json.
            Context.Response.ContentType = "application/json";
            // Sends all currently buffered output to the client.
            Context.Response.Flush();
            // Gets or sets a value indicating whether to send HTTP content to the client.
            Context.Response.SuppressContent = true;
            // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.
            Context.ApplicationInstance.CompleteRequest();
            Context.Response.End();
        }
        // ************************************************** GET - VC CONSOMMER BONUS ************************************************** //
        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public void VC_GetBonus(string UUID)
        //  VEGGIECRUSH =>  Boomcraft. Permet à un joueur Veggiecrush de connaître le nombre de bonus qu'il possède.
        {
            string sResult = string.Empty;
            Repository aREP = new Repository();
            //  Récupération de la quantité de bonus du joueur.
            int iQuantite = aREP.Get_JoueurBonus(UUID);
            sResult = "{ 'qte': " + iQuantite.ToString() + " }";
            //  Sérialisation de la réponse en Objet.
            Object oResult = new JavaScriptSerializer().DeserializeObject(sResult);
            //  Sérialisation de la réponse au format JSON.
            var jsonSerializer = new JsonSerializer();
            jsonSerializer.Serialize(Context.Response.Output, oResult);
            //  Formatage du retour en json.
            Context.Response.ContentType = "application/json";
            // Sends all currently buffered output to the client.
            Context.Response.Flush();
            // Gets or sets a value indicating whether to send HTTP content to the client.
            Context.Response.SuppressContent = true;
            // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.
            Context.ApplicationInstance.CompleteRequest();
            Context.Response.End();
        }
        // **************************************************  ************************************************** //
        #endregion API VEGGIECRUSH
        #region API HOWOB
        // ************************************************** HW LISTE COMBAT ************************************************** //
        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public void HW_ListeCombat()
        //  HOWOB => Boomcraft. Permet à un joueur Howob d'obtenir une liste des combats qu'il peut rejoindre.
        {
            string sResult = string.Empty;
            //  Déclaration d'un objet DataTable pour stocker la liste des combats à venir.
            DataTable dt = aREP.GetAll_Combat().Tables[0];
            //  Conversion de l'objet DataTable en objet Object.
            Object oResult = JsonConvert.SerializeObject(dt, Formatting.None);
            //  Sérialisation de la réponse au format JSON.
            var jsonSerializer = new JsonSerializer();
            jsonSerializer.Serialize(Context.Response.Output, oResult);
            //  Formatage du retour en json.
            Context.Response.ContentType = "application/json";
            // Sends all currently buffered output to the client.
            Context.Response.Flush();
            // Gets or sets a value indicating whether to send HTTP content to the client.
            Context.Response.SuppressContent = true;
            // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.
            Context.ApplicationInstance.CompleteRequest();
            Context.Response.End();
        }
        // ************************************************** HW PARTICIPER COMBAT ************************************************** //
        [WebMethod]
        public void HW_ParticiperCombat(int iIdAttaque, string sUUIDHowob, int iFaction, string sStats)
        //  HOWOB => Boomcraft. Permet à un joueur Howob d'obtenir une liste des combats qu'il peut rejoindre.
        {
            string sResult = string.Empty;
            //  Récupération du résultat de la demande de consommation de bonus.
            int iResult = aREP.Insert_ParticiperCombat(iIdAttaque, sUUIDHowob, iFaction, sStats);
            if (iResult == 1)
            {
                sResult = "{ \"result\": \"true.\","
                    + "\"msg_code\": \"La participation au combat a bien été prise en compte.\"}";
            }
            else
            {
                sResult = "{ \"result\": \"false.\","
                    + "\"msg_code\": \"La participation au combat n'a pas été prise en compte. La place est déjà prise !\"}";
            }
            //  Sérialisation de la réponse en Objet.
            Object oResult = new JavaScriptSerializer().DeserializeObject(sResult);
            //  Sérialisation de la réponse au format JSON.
            var jsonSerializer = new JsonSerializer();
            jsonSerializer.Serialize(Context.Response.Output, oResult);
            //  Formatage du retour en json.
            Context.Response.ContentType = "application/json";
            // Sends all currently buffered output to the client.
            Context.Response.Flush();
            // Gets or sets a value indicating whether to send HTTP content to the client.
            Context.Response.SuppressContent = true;
            // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.
            Context.ApplicationInstance.CompleteRequest();
            Context.Response.End();
        }
        // ************************************************** HW REPORT ************************************************** //
        [WebMethod]
        //  [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string HW_Report(string sUUID, bool bSucces, int iQuantite)
        {
            //  TODO : Comprendre dans quel contexte cette fonction va être appelée. Quels sont les paramètres d'entrée ?
            return "{'uid': 'joueur1', 'success': true, 'ressources': { 'wood': 100, 'food': 100, 'gold': 100, 'rock': 100 } }";
        }
        // **************************************************  ************************************************** //
        #endregion API HOWOB
    }
}
