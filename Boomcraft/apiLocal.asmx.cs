using System;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
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
        #region API OBTENIR JOUEUR
        // ************************************************** OBTENIR JOUEUR ************************************************** //
        [WebMethod]
        public void BC_ObtenirJoueur(string sNomUtilisateur, string sMdp)
        //  Retourne les informations du joueur à l'interface graphique.
        //  Retourne une erreur si les identifiants de connexions ne sont pas dans la base de données Boomcraft.
        //  TODO : Etendre cette API aux joueurs de tous les jeux.
        {
            //  Déclaration d'une variable pour stocker les données d'un utilisateur.
            string sResult = string.Empty;
            //  Création d'un objet joueur à l'aide du nom et du mot de passe.
            Joueur aJoueur = new Joueur(sNomUtilisateur, sMdp);
            //  Récupération des informations du joueur au format JSON.
            sResult = aJoueur.get_JoueurJSONToken();
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
        // ************************************************** BC OBTENIR JOUEUR ************************************************** //
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
        // **************************************************  ************************************************** //
        #endregion API VERIFY EXISTENCE LOGIN
    }
}
