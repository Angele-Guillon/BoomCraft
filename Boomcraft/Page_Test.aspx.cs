using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Boomcraft
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        // ************************************************** VARIABLES ************************************************** //
        DAL aTest = new DAL();
        // **************************************************  ************************************************** //
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void clic_BtnConnexionBase(object sender, EventArgs e)
        {
            //long iResult = aTest.Insert_Test("Dude", 24);
        }
    }
}