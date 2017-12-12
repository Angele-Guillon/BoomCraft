using System;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using Boomcraft.METIER;
using Boomcraft.DAL;

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
                //username = "";
                //password = "984g-èf&_ètvdçvdcçoyb";
                //  Création d'un objet joueur à l'aide du nom et du mot de passe.
                Joueur aJoueur = new Joueur(username, password);
                //  Obtention de l'objet joueur en format JSON.
                sResult = aJoueur.get_JoueurJSON();
            }
            catch (Exception ex)
            {
                //  Renvoie d'erreur en cas d'échec.
                sResult = "{ 'error': { 'message': 'Une erreur s'est produite lors de la vérification des identifiants du joueur.', 'code': 401 } }";
                //sResult = "{ 'error': { 'message': '" + ex + "', 'code': 401 } }";
            }
            //  Sérialisation de la réponse en Objet.
            Object oResult = new JavaScriptSerializer().DeserializeObject(sResult);
            //  Sérialisation de la réponse au format JSON.
            var jsonSerializer = new JsonSerializer();
            jsonSerializer.Serialize(Context.Response.Output, oResult);
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
                //sResult = "{ 'error': { 'message': '" + ex + "', 'code': 401 } }";
            }
            //  Sérialisation de la réponse en Objet.
            Object oResult = new JavaScriptSerializer().DeserializeObject(sResult);
            //  Sérialisation de la réponse au format JSON.
            var jsonSerializer = new JsonSerializer();
            jsonSerializer.Serialize(Context.Response.Output, oResult);
            // Sends all currently buffered output to the client.
            Context.Response.Flush();
            // Gets or sets a value indicating whether to send HTTP content to the client.
            Context.Response.SuppressContent = true;
            // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.
            Context.ApplicationInstance.CompleteRequest();
            Context.Response.End();
        }
        // **************************************************  ************************************************** //
        #endregion API BASE
        #region API VEGGIECRUSH
        // ************************************************** POST - VC CONSOMMER BONUS ************************************************** //
        [WebMethod]
        //[ScriptMethod(HttpMethod = HttpPutAttribute)]
        //public Object VC_ConsommerBonus(string UUID, int qte)
        public void VC_ConsommerBonus(string UUID, int qte)
        //  VEGGIECRUSH =>  Boomcraft. Permet à un joueur Veggiecrush de consommer un bonus disponible dans la base boomcraft.
        {
            string sResult = string.Empty;
            try
            {
                //  Récupération du résultat de la demande de consommation de bonus.
                Boolean bResult = aREP.VC_Update_JoueurBonus(UUID, qte);
                sResult = "{ 'success': " + bResult.ToString().ToLower() + " }";
            }
            catch (Exception ex)
            {
                //  Renvoie d'erreur en cas d'échec.
                sResult = "{ 'error': { 'message': 'Une erreur s'est produite lors de la vérification des identifiants du joueur.', 'code': 401 } }";
                //sResult = "{ 'error': { 'message': '" + ex + "', 'code': 401 } }";
            }
            //  Sérialisation de la réponse en Objet.
            Object oResult = new JavaScriptSerializer().DeserializeObject(sResult);
            //  Sérialisation de la réponse au format JSON.
            var jsonSerializer = new JsonSerializer();
            jsonSerializer.Serialize(Context.Response.Output, oResult);
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
        //  TODO : Transformer ce service web en méthode GET.
        //  VEGGIECRUSH =>  Boomcraft. Permet à un joueur Veggiecrush de connaître le nombre de bonus qu'il possède.
        {
            string sResult = string.Empty;
            Repository aREP = new Repository();
            //  Récupération de la quantité de bonus du joueur.
            int iQuantite = aREP.Get_JoueurBonus(UUID);
            sResult = "{ 'qte': " + iQuantite + " }";
            //  Sérialisation de la réponse en Objet.
            Object oResult = new JavaScriptSerializer().DeserializeObject(sResult);
            //  Sérialisation de la réponse au format JSON.
            var jsonSerializer = new JsonSerializer();
            jsonSerializer.Serialize(Context.Response.Output, oResult);
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
        #region API AUTRES
        // ************************************************** FV ENVOYER DON ************************************************** //
        [WebMethod]
        //  [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string FV_envoyerDon(string sUUID, int iIdRessource, int iQuantite)
        //  FarmVillage => Boomcraft. Permet aux joueurs d'envoyer des ressources suite à une demande.
        {
            //  Ajoute les ressources envoyées au joueur de Boomcraft.
            int iResult = aREP.FV_envoyerDon(sUUID, iIdRessource, iQuantite);
            //int iResult = aDal.FV_envoyerDon("joueur1", 1, 100);
            //return new JavaScriptSerializer().Serialize(sRetour);
            return "{'msg_code': 'Merci !';}";
        }
        // ************************************************** FV ENVOYER DON ************************************************** //
        [WebMethod]
        //  [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string HW_Report(string sUUID, bool bSucces, int iQuantite)
        {
            //  TODO : Comprendre dans quelle contexte cette fonction va être appelée. Quels sont les paramètres d'entrée ?
            return "{'uid': 'joueur1', 'success': true, 'ressources': { 'wood': 100, 'food': 100, 'gold': 100, 'rock': 100 } }";
        }
        // ************************************************** FV ENVOYER DON ************************************************** //
        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public string Test()
        {
            //  TODO : Comprendre dans quelle contexte cette fonction va être appelée. Quels sont les paramètres d'entrée ?
            return "{'uid': 'joueur1', 'success': true, 'ressources': { 'wood': 100, 'food': 100, 'gold': 100, 'rock': 100 } }";
        }
        // ************************************************** API TEST RETURN JSON (SANS d:) ************************************************** //
        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public string api_100d()
        {
            string responseText = "";
            string sResult = "";
            //try
            //{
            //    //Stuff goes here
            //    responseText = "It Worked!";
            //}
            //catch (Exception ex) { responseText = "Opps wehave an error! Exception message:" + ex.Message; }

            //  À tester.
            sResult = "{ 'qte': " + 100 + " }";
            //  Sérialisation du string au format JSON.
            //return new JavaScriptSerializer().DeserializeObject(sResult);
            return sResult;
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string api_100d_Post()
        {
            string responseText = "";
            string sResult = "";
            //try
            //{
            //    //Stuff goes here
            //    responseText = "It Worked!";
            //}
            //catch (Exception ex) { responseText = "Opps wehave an error! Exception message:" + ex.Message; }

            //  À tester.
            sResult = "{ 'qte': " + 100 + " }";
            //  Sérialisation du string au format JSON.
            //return new JavaScriptSerializer().DeserializeObject(sResult);
            //return new JavaScriptSerializer().Serialize(sResult);
            return sResult;

            //return responseText;
        }
        // ************************************************** TEST GET ************************************************** //
        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        //[ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //public string Test_Get()
        public string Test_Get()
        {
            string sResult = "{ 'result': 'Hello World' }";
            //return new JavaScriptSerializer().Serialize(sResult);
            //JavaScriptSerializer ser = new JavaScriptSerializer();
            //ser.Serialize(sResult);
            //Context.Response.Write(ser.ToString());
            return sResult;
        }
        // ************************************************** TEST GET 2 ************************************************** //
        [WebMethod]
        public string Test_Get2()
        {
            return "{ 'aaa': 'aaa' } ";
        }
        // **************************************************  ************************************************** //
        #endregion API AUTRES
    }
}
