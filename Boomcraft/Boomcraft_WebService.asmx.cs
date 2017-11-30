using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace Boomcraft
{
    /// <summary>
    /// Description résumée de Boomcraft_WebService
    /// </summary>
    //[WebService(Namespace = "http://tempuri.org/")]
    [WebService(Namespace = "Boomcraft")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Pour autoriser l'appel de ce service Web depuis un script à l'aide d'ASP.NET AJAX, supprimez les marques de commentaire de la ligne suivante. 
    [System.Web.Script.Services.ScriptService]
    public class Boomcraft_WebService : System.Web.Services.WebService
    {
        // ************************************************** VARIABLES ************************************************** //
        Boomcraft_Repository aDal = new Boomcraft_Repository();
        // **************************************************  ************************************************** //
        [WebMethod]
        public string get_Test()
        {
            return "Test.\nBlah !";
        }
        // ************************************************** FV ENVOYER DON ************************************************** //
        [WebMethod]
        //  [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //public string FV_envoyerDon(string sUUID, int iIdRessource, int iQuantite)
        public string FV_envoyerDon()
        //  FarmVillage => Boomcraft. Permet aux joueurs d'envoyer des ressources suite à une demande.
        {
            string sRetour = "{'msg_code': 'Merci !';}";
            //  Ajoute les ressources envoyées au joueur de Boomcraft.
            //int iIdResult = aDal.FV_envoyerDon(sUUID, iIdRessource, iQuantite);
            int iIdResult = aDal.FV_envoyerDon("joueur1", 1, 100);
            //return new JavaScriptSerializer().Serialize(sRetour);
            return sRetour;
        }
        // **************************************************  ************************************************** //
    }
}
