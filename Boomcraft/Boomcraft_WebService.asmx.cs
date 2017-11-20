using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
        Test aTest = new Test();
        // **************************************************  ************************************************** //
        [WebMethod]
        public string get_Test(string sNom, int iAge)
        {
            long iIdResult;
            iIdResult = aTest.Insert_Test(sNom, iAge);
            return "Le test a été créé dans la base. Son id est : " + iIdResult + ".";
        }
    }
}
