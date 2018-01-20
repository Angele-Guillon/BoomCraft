using Boomcraft.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Boomcraft
{
  public class Combat
  {
    #region VARIABLES ET CONSTRUCTEURS
    // ************************************************** VARIABLES ************************************************** //
    //  Id unique pour les combats qui se déroulent entre joueurs de Boomcraft.
    private int iIdCombat;
    //  Id du joueur qui attaque.
    private int iIdAttaquant;
    //  Id du joueur qui déffend.
    private int iIdDefenseur;
    //	Temps en minutes avant le déroulement du combat.
    private int iDureeAvantCombat;
    //  Id du joueur qui a remporté le combat.
    private int iIdVainqueur;
    // ************************************************** CONSTRUCTEUR COMBAT ************************************************** //

    public Combat(int iIdCombat, int iIdAttaquant, int iIdDefenseur, int iDureeAvantCombat, int iIdVainqueur)
    //  Constructeur utilisé lors de la connexion du joueur.
    {
      //  Instanciation d'un objet permettant l'accès à la base de données.
      Repository aREP = new Repository();
      //  Récupération des données du joueur en fonction des ses identifiants de connexion.
      DataTable dt = aREP.Get_Combat(iIdCombat, iIdAttaquant, iIdDefenseur, iDureeAvantCombat, iIdVainqueur).Tables[0];

      //  TODO : Gérer le cas où les identifiants saisis sont erronés.
      this.iIdCombat =          int.Parse(dt.Rows[0][0].ToString());
      this.iIdAttaquant =       int.Parse(dt.Rows[0][1].ToString());
      this.iIdDefenseur =       int.Parse(dt.Rows[0][2].ToString());
      this.iDureeAvantCombat =  int.Parse(dt.Rows[0][3].ToString());
      this.iIdVainqueur =       int.Parse(dt.Rows[0][4].ToString());

    }
    // ************************************************** CONSTRUCTEUR ??? ************************************************** //
    #endregion VARIABLES ET CONSTRUCTEURS
    #region METHODES
    public string CombatJSON()
    {
      string sCombatJSON = string.Empty;
      //Structure du fichiers JSON

      sCombatJSON = "{ " +
                        "idCombat :" + iIdCombat +
                        ", idAttaquant : " + iIdAttaquant +
                        ", idDefenseur :" + iIdDefenseur +
                        ", idDureAvantCombat : " + iDureeAvantCombat +
                        ",idVainqueur : " + iIdVainqueur +

                     "}";
      return sCombatJSON;
    }
  
    // ************************************************** METHODES ************************************************** //
    // **************************************************  ************************************************** //
    #endregion METHODES
  }
}
