using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Boomcraft
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        // ************************************************** VARIABLES ************************************************** //
        Boomcraft_Repository aREP = new Boomcraft_Repository();
        // **************************************************  ************************************************** //
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void Clic_BtnConnexionBase(object sender, EventArgs e)
        {
            //long iResult = aTest.Insert_Test("Dude", 24);
            //string sRetour = aREP.Insert_Account_Proc("joueur10", "Tommy", "mdp", "email", DateTime.Now, DateTime.Now, DateTime.Now, 1);
            //string sRetour = aREP.Insert_Test("Jean", 42);

            #region Test GetAll_Joueur
            //DataSet ds = aREP.GetAll_Joueur();
            //DataTable dt = ds.Tables[0];
            //lbl_Test.Text = dt.Rows[0][0].ToString() + dt.Rows[0][1].ToString() + dt.Rows[0][2].ToString() + dt.Rows[0][3].ToString() + dt.Rows[0][4].ToString();
            #endregion Test GetAll_Joueur

            #region Test Update_Joueur
            //DateTime? dt2 = null;
            //DataSet ds2 = aREP.Update_Joueur(31, "aze", "aze", "aze", "aze", DateTime.Now, dt2, dt2, 1);
            #endregion Test Update_Joueur

            #region Test Insert_Joueur
            DateTime? dt2 = null;
            string sRetour = aREP.Insert_Joueur("Joueur20", "Biteboy", "mdp", "email", DateTime.Now, dt2, dt2, 1);
            lbl_Test.Text = sRetour;
            #endregion Test Insert_Joueur
        }
    }
}