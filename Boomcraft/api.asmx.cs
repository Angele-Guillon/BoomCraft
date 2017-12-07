using System.Web.Script.Serialization;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using Boomcraft.METIER;
using Boomcraft.DAL;
using System.Data;
using System.Web.Mvc;
using System;

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
    [System.Web.Script.Services.ScriptService]
    public class api : System.Web.Services.WebService
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
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public Object signin(string username, string password)
        //  Autres jeux => Boomcraft. Renvoie les informations d'un compte à partir d'un couple d'identifiants (NAME - PASSWORD).
        {
            //  Création d'un objet joueur à l'aide du nom et du mot de passe.
            Joueur aJoueur = new Joueur(username, password);
            //  Obtention de l'objet joueur en format JSON.
            string sJoueurJSON = aJoueur.get_JoueurJSON();
            //  Sérialisation du string au format JSON.
            return new JavaScriptSerializer().DeserializeObject(sJoueurJSON);
        }
        // ************************************************** EXISTING ************************************************** //
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public Object existing(string username, string email)
        //  Autres jeux => Boomcraft. Permet de checker l'existence d'un couple d'identifiants (NAME - PASSWORD).
        {
            //  Récupération du l'existence des données du joueur.
            string sExistenceJoueur = aREP.Check_ExistenceJoueur(username, email);
            //  Sérialisation du string au format JSON.
            return new JavaScriptSerializer().DeserializeObject(sExistenceJoueur);
        }
        // **************************************************  ************************************************** //
        #endregion API BASE
        #region API VEGGIECRUSH
        // ************************************************** VC BONUS ************************************************** //
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public Object VC_bonus(string UUID, int qte)
        //  Autres jeux => Boomcraft. Permet de checker l'existence d'un couple d'identifiants (NAME - PASSWORD).
        {
            //  TODO : Renvoyer true si le bonus demandé a bien été consommé dans la base de données.
            string sReuslt = string.Empty;
            //  Sérialisation du string au format JSON.
            return new JavaScriptSerializer().DeserializeObject(sReuslt);
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
        // **************************************************  ************************************************** //
        #endregion API AUTRES
    }
}
