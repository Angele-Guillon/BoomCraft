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
    [System.Web.Script.Services.ScriptService]
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
        // ************************************************** aaa ************************************************** //

        public void obtenirJoueur(string sNomUtilisateur, string sMdp)
        //  Retourne les informations du joueur à l'interface graphique.
        //  TODO : Cette fonction est ectuellement une fonction de test qui permet d'effectuer un bouchon au niveau de la conexion de l'interface.
        {
            //  Déclaration d'une variable pour stocker les données d'un utilisateur.
            string sResult = string.Empty;
            //  Création d'un objet joueur à l'aide du nom et du mot de passe.
            Joueur aJoueur = new Joueur("boomcraft", "boomcraft");
            //  Récupération des informations du joueur au format JSON.
            sResult = aJoueur.get_JoueurJSON();
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
