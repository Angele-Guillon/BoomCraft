using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using System.Collections.Generic;

using Boomcraft.DAL;
using Boomcraft.METIER;

namespace Boomcraft
{
    /// <summary>
    /// Description résumée de apiLocal
    /// </summary>
    //[WebService(Namespace = "http://tempuri.org/")]
    [WebService(Namespace = "Boomcraft")]
    //[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    //[System.ComponentModel.ToolboxItem(false)]
    // Pour autoriser l'appel de ce service Web depuis un script à l'aide d'ASP.NET AJAX, supprimez les marques de commentaire de la ligne suivante. 
    [ScriptService]
    public class apiLocal : WebService
    {
        #region VARIABLES
        // ************************************************** VARIABLES ************************************************** //
        //  Instanciatio de la classe d'accès à la base.
        Repository aREP = new Repository();
        //  Instanciation de la classe métier.
        Metier aMetier = new Metier();
        //  Instanciation de la classe Log.
        Log aLog = new Log();
        //  Instanciation de la classe api.
        api aApi = new api();

        // **************************************************  ************************************************** //
        #endregion VARIABLES
        #region API BC JOUEUR
        // ************************************************** BC CREER JOUEUR ************************************************** //
        [WebMethod]
        public void BC_CreerJoueur(string sNomUtilisateur, string sEmail, string sMdp, string sFaction)
        //  Création d'un joueur dans la base.
        {
            string sResult;
            //  Instanciation d'un joueur.
            Joueur aJoueur = new Joueur(sNomUtilisateur, sEmail, sMdp, sFaction);
            if (aJoueur.Get_Id() > 0)
            {
                sResult = "Le joueur a été créé.";
            }
            else
            {
                sResult = "Le joueur n a pas été créé car le nom d utilisateur et/ou l email est indisponible.";
            }
            sResult = "{ 'result' : '" + sResult + "' }";
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
        // ************************************************** OBTENIR JOUEUR ************************************************** //
        [WebMethod]
        public void BC_ObtenirJoueur(string sNomUtilisateur, string sMdp)
        //  Retourne les informations du joueur à l'interface graphique.
        //  Retourne une erreur si les identifiants de connexions ne sont pas dans la base de données Boomcraft.
        //  TODO : Etendre cette API aux joueurs de tous les jeux.
        {
            //  Déclaration d'une variable pour récupérer stocker les informations d'un joueur sous forme de JSON.
            string sResult = string.Empty;
            //  Déclaration d'un objet dans lequel est stocké la réponse à la requête web.
            Object oResult = new Object();
            oResult = null;
            List<WebRequest> liste_WebRequest = new List<WebRequest>();
            //  Déclaration de la variable HttpWebRequest et spécification de l'URL.
            const string sUrlBoomcraft = "http://boomcraft.masi-henallux.be:8080/api.asmx/signin";
            const string sUrlFarmvillage = "http://artshared.fr/andev1/distribue/api/auth/signin/";
            const string sUrlHowob = "https://howob.masi-henallux.be/api/auth/signin";
            const string sUrlVeggieCrush = "https://veggiecrush.masi-henallux.be/rest_server/api/account/signin/";
            liste_WebRequest.Add(WebRequest.Create(sUrlBoomcraft));
            liste_WebRequest.Add(WebRequest.Create(sUrlFarmvillage));
            liste_WebRequest.Add(WebRequest.Create(sUrlHowob));
            liste_WebRequest.Add(WebRequest.Create(sUrlVeggieCrush));
            //  Boucle sur les apis signin des différents groupes.
            foreach (HttpWebRequest request in liste_WebRequest)
            {
                #region  Ensemble de paramètres qui fonctionnent pour communiquer avec les apis C#.
                ////var postData = "username=" + sNomUtilisateur + "&password=" + sMdp;
                ////  Formatage du retour en de la requête.
                //request.ContentType = "application/x-www-form-urlencoded";
                //var postData = "username=" + sNomUtilisateur + "&password=" + sMdp;
                #endregion
                //var jsonData = "{\"username\": \"veggie\",\"password\": \"veggie\"}";
                var jsonData = "{\"username\":\"" + sNomUtilisateur + "\",\"password\":\"" + sMdp + "\"}";
                //jsonData = "{\"username\":\"howob\",\"password\":\"howob\"}";
                var data = Encoding.ASCII.GetBytes(jsonData);
                request.Method = "POST";
                //  Définition la valeur de l'en-tête de la requête.
                request.ContentType = "application/json";
                request.ContentLength = data.Length;
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                //  Déclaration de la variable HttpWebResponse pour récupérer le résultat de la requête.
                HttpWebResponse response;
                try
                {
                    response = (HttpWebResponse)request.GetResponse();
                }
                catch (Exception ex)
                {
                    aLog.ecrire("Erreur lors de la connexion au serveur : " + request.RequestUri + ".\r" + ex);
                    //  Passage à l'itération suivante. On ignore le serveur défectueux.
                    continue;
                }
                //  Déclaration d'un string pour récupérer le résultat de la réponse qui est un JSON.
                string sSigninJson = new StreamReader(response.GetResponseStream()).ReadToEnd();
                response.Close();
                //  Parse la réponse de l'api signin dans un objet permetant d'exploiter un json.
                dynamic d = JObject.Parse(sSigninJson);
                //aLog.ecrire((String)d.user.id);
                //aLog.ecrire((String)d.user.globalId);
                //aLog.ecrire((String)d.user.username);
                //aLog.ecrire((String)d.user.email);
                //aLog.ecrire((String)d.user.faction);
                //aLog.ecrire((String)d.user.dateCreation);
                try
                {
                    //  Instanciation d'un objet Joueur.
                    Joueur aJoueur;
                    switch (request.RequestUri.ToString())
                    {
                        case sUrlBoomcraft:
                            aJoueur = new Joueur((String)d.user.id, (String)d.user.globalId, (String)d.user.username, (String)d.user.email, (String)d.user.faction);
                            //aJoueur = new Joueur("123", "321", "poi", "poi.poi@poi", "light");
                            sResult = aJoueur.get_JoueurJSON();
                            break;
                        case sUrlFarmvillage:
                            aJoueur = new Joueur((String)d.user._id , (String)d.user.globalId, (String)d.user.username, (String)d.user.email, (String)d.user.faction);
                            sResult = aJoueur.get_JoueurJSON();
                            break;
                        case sUrlHowob:
                            //aJoueur = new Joueur((String)d.user._id, (String)d.user.globalId, (String)d.user.username, (String)d.user.email, (String)d.user.faction);
                            aJoueur = new Joueur(1.ToString(), (String)d.user.globalId, (String)d.user.username, (String)d.user.email, (String)d.user.faction);
                            sResult = aJoueur.get_JoueurJSON();
                            break;
                        case sUrlVeggieCrush:
                            aJoueur = new Joueur((String)d.user._id, (String)d.user.globalId, (String)d.user.username, (String)d.user.email, (String)d.user.faction);
                            sResult = aJoueur.get_JoueurJSON();
                            break;
                        default:
                            aLog.ecrire("L'Url du serveur joint est inconnue.");
                            break;
                    }
                    if (sResult != string.Empty)
                    {
                        //  Les informations du joueur ont été récupérées avec succès.
                        oResult = JsonConvert.DeserializeObject<Object>(sResult);
                        break;
                    }
                }
                catch (Exception ex)
                {
                    aLog.ecrire(sResult + "\rDétails : \r" + ex);
                }
            }
            if (oResult == null)
            {
                //  Aucun utilisateur ne correspondant aux identifiants saisis. Définition d'un message d'erreur.
                oResult = JsonConvert.DeserializeObject<Object>(
                "{ \"error\": { \"message\": \"Les identifiants ("
                + sNomUtilisateur + " - " + sMdp + ") n'existent pas.\", \"code\": 401 } }");
            }
            //  Ecrit la réponse finale de l'api BC_ObtenirJoueur.
            aLog.ecrire(JsonConvert.SerializeObject(oResult));
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
        // ************************************************** BC EDITER JOUEUR ************************************************** //
        [WebMethod]
        public void BC_EditerJoueur(int iId, string sNom, string sMdp, string sEmail, string sFaction)
        //  Modification des informations d'un joueur de Boomcraft dans la base.
        {
            //  Déclaration de la réponse de la fonction qui sera transformée en JSON.
            string sResult;
            //  iResult correspond au nombre de lignes modifiées dans la base.
            int iResult = aREP.Update_Joueur(iId, sNom, sMdp, sEmail, sFaction);
            if (iResult > 0)
            {
                sResult = "Les informations du joueurs ont été mises à jour.";
            }
            else
            {
                sResult = "Les informations du joueurs n ont pas été mises à jour.";
            }
            sResult = "{ 'result' : '" + sResult + "' }";
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
        // ************************************************** BC SUPPRIMER JOUEUR ************************************************** //
        [WebMethod]
        public void BC_SupprimerJoueur(int iId)
        //  Suppression des informations d'un joueur de Boomcraft dans la base.
        {
            //  Déclaration de la réponse de la fonction qui sera transformée en JSON.
            string sResult;
            //  iResult correspond au nombre de lignes modifiées dans la base.
            int iResult = aREP.Delete_Joueur(iId);
            if (iResult > 0)
            {
                sResult = "Le joueur a été supprimé.";
            }
            else
            {
                sResult = "Le joueur n a pas été supprimé.";
            }
            sResult = "{ 'result' : '" + sResult + "' }";
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
        // ************************************************** BC NEW ************************************************** //
        [WebMethod]
        public void BC_New(int iId)
        //  Suppression des informations d'un joueur de Boomcraft dans la base.
        {
            //  Déclaration de la réponse de la fonction qui sera transformée en JSON.
            string sResult = String.Empty;
        }
        // **************************************************  ************************************************** //
        #endregion API VERIFY EXISTENCE LOGIN

        #region API BC TEST
        // ************************************************** BC TEST GET ************************************************** //
        [WebMethod]
        public void BC_Test_Get()
        //  Api de test. Doit appeler d'autres apis.
        {
            //  String Json pour le retour de l'api.
            string sResult = string.Empty;
            #region TEST1
            //  Déclaration de la variable HttpWebRequest et spécification de l'URL.
            //WebRequest request = WebRequest.Create("http://boomcraft.masi-henallux.be:8080/api.asmx/HW_ListeCombats");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:50728/api.asmx/HW_ListeCombats");
            //// If required by the server, set the credentials.
            //request.Credentials = CredentialCache.DefaultCredentials;
            //  Déclaration de la variable HttpWebResponse et récupération du résultat de la requête.
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //  Déclaration d'un string pour récupérer le résultat de la réponse qui est un JSON.
            sResult = new StreamReader(response.GetResponseStream()).ReadToEnd();
            response.Close();
            #endregion TEST1
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
        // ************************************************** BC TEST POST ************************************************** //
        [WebMethod]
        public void BC_Test_Post()
        //  Api de test. Doit appeler d'autres apis.
        {
            //  String Json pour le retour de l'api.
            string sResult = string.Empty;
            #region TEST1
            //  Déclaration de la variable HttpWebRequest et spécification de l'URL.
            WebRequest request = WebRequest.Create("http://boomcraft.masi-henallux.be:8080/api.asmx/existing");
            //// If required by the server, set the credentials.
            //request.Credentials = CredentialCache.DefaultCredentials;
            var postData = "username=boomcraft" + "&email=boomcraft@boomcraft.boomcraft";
            var data = Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            //  Formatage du retour en de la requête.
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;
            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            //  Déclaration de la variable HttpWebResponse et récupération du résultat de la requête.
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //  Déclaration d'un string pour récupérer le résultat de la réponse qui est un JSON.
            sResult = new StreamReader(response.GetResponseStream()).ReadToEnd();
            response.Close();
            #endregion TEST1
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
        #endregion API BC TEST
    }
}