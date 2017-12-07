using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Boomcraft.DAL;

namespace Boomcraft
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        // ************************************************** VARIABLES ************************************************** //
        Repository aREP = new Repository();
        // **************************************************  ************************************************** //
        protected void Page_Load(object sender, EventArgs e)
        {
            Function_Test();
        }
        protected void Clic_BtnConnexionBase(object sender, EventArgs e)
        {
            lbl_Test.Text = "Bouton non câblé.";
        }
        protected void Function_Test()
        {
            #region Test GetAll_Joueur
            //DataSet ds = aREP.GetAll_Joueur();
            //DataTable dt = ds.Tables[0];
            //lbl_Test.Text = dt.Rows[0][0].ToString() + dt.Rows[0][1].ToString() + dt.Rows[0][2].ToString() + dt.Rows[0][3].ToString() + dt.Rows[0][4].ToString();
            #endregion Test GetAll_Joueur

            #region Test Update_Joueur
            //DateTime? dt2 = null;
            //int iResult_UpdateJoueur = aREP.Update_Joueur(39, "zzz", "aze", "aze", "aze", DateTime.Now, dt2, dt2, 1);
            #endregion Test Update_Joueur

            #region Test Insert_Joueur
            //DateTime? dt3 = null;
            //int iRetour = aREP.Insert_Joueur("Joueur20", "aaaaa", "mdp", "email", DateTime.Now, dt3, dt3, 1);
            //lbl_Test.Text = iRetour.ToString();
            #endregion Test Insert_Joueur

            #region Test Delete_Joueur
            //int iRetour = aREP.Delete_Joueur(32);
            //lbl_Test.Text = iRetour.ToString();
            #endregion Test Delete_Joueur

            #region Test Check_ExistenceJoueur
            //string sExistenceJoueur = aREP.Check_ExistenceJoueur("Tim", "mdp");
            //lbl_Test.Text = "Le joueur 'Tim' - 'mdp' existe ? ==>" + sExistenceJoueur;
            #endregion Test Check_ExistenceJoueur
        }
    }
}
