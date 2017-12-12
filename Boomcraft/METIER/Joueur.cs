using System;
using System.Data;
using Boomcraft.DAL;

namespace Boomcraft.METIER
{
    public class Joueur
    {
        #region VARIABLES ET CONSTRUCTEURS
        // ************************************************** VARIABLES ************************************************** //
        //  Id unique pour les joueurs de Bomcraft.
        private int iId;
        //  Id unique pour l'ensemble des jeux.
        private string sUUID;
        private string sNom;
        private string sMdp;
        private string sEmail;
        private DateTime? dtCreation;
        private DateTime? dtEdition;
        private DateTime? dtSupression;
        private string sFaction;
        private Boolean bErreur;
        // ************************************************** CONSTRUCTEUR CONNEXION JOUEUR ************************************************** //
        public Joueur(string sNom, string sMdp)
        //  Constructeur utilisé lors de la connexion du joueur.
        {
            //  Instanciation d'un objet permettant l'accès à la base de données.
            Repository aREP = new Repository();
            //  Récupération des données du joueur en fonction des ses identifiants de connexion.
            DataTable dt = aREP.Get_JoueurByLogin(sNom, sMdp).Tables[0];
            //  Initialisation du champ erreur à true.
            bErreur = true;
            try
            {
                DateTime dtDateNull;
                //  TODO : Gérer le cas où les identifiants saisis sont erronés.
                this.iId = int.Parse(dt.Rows[0][0].ToString());
                this.sUUID = dt.Rows[0][1].ToString();
                this.sNom = dt.Rows[0][2].ToString();
                this.sMdp = dt.Rows[0][3].ToString();
                this.sEmail = dt.Rows[0][4].ToString();
                this.dtCreation = DateTime.Parse(dt.Rows[0][5].ToString());
                if (DateTime.TryParse(dt.Rows[0][6].ToString(), out dtDateNull))
                {
                    this.dtEdition = dtDateNull;
                }
                if (DateTime.TryParse(dt.Rows[0][7].ToString(), out dtDateNull))
                {
                    this.dtSupression = dtDateNull;
                }
                this.sFaction = dt.Rows[0][8].ToString();
                bErreur = false;
            }
            catch
            {
                //  Le joueur n'a pas pu êre créé car la combinaison USERNAME - PASSWORD est fausse.
                bErreur = true;
            }
        }
        // ************************************************** CONSTRUCTEUR CREATION JOUEUR ************************************************** //
        public Joueur(string sNom, string sEmail, string sMdp, string sFaction)
        //  Constructeur utilisé lors de la création du compte du joueur.
        {
            //  Instanciation d'un objet permettant l'accès à la base de données.
            Repository aREP = new Repository();
            //  Récupération de la date actuelle.
            DateTime dtActuelle = DateTime.Now;
            //  Instanciation d'un DateTime null.
            DateTime? dtNull = null;
            //  Récupération des données du joueur en fonction des ses identifiants de connexion.
            int iIdResult = aREP.Create_Joueur(sNom, sEmail, sMdp, dtActuelle, sFaction);
            //  Teste le retour de la fonction
            if (iIdResult > 0)
            {
                //  L'id est strictement supérieur à 0 donc le joueur  bien été créé en base.
                this.iId = iIdResult;
                //  Génération d'un id aléatoire unique.
                this.sUUID = Guid.NewGuid().ToString();
                this.sNom = sNom;
                this.sEmail = sEmail;
                this.sMdp = sMdp;
                this.dtCreation = dtActuelle;
                this.dtEdition = dtNull;
                this.dtSupression = dtNull;
                this.sFaction = sFaction;
            }
            else
            {
                //  TODO : Gérer le cas où la création de compte ne fonctionne pas. (Exemple : email déjà existant).
            }
        }
        #endregion VARIABLES ET CONSTRUCTEURS
        #region METHODES
        // ************************************************** METHODES ************************************************** //
        public string get_JoueurJSON()
        {
            string sJoueurJSON = string.Empty;
            if (!bErreur)
            {
                //  La combinaison (USERNAME - PASSWORD) est correct. Le joueur a bien été créé et ses informations ont été récupérées en base.
                sJoueurJSON = "{ 'user': { 'id': " + iId + ", 'globalId': '" + sUUID + "', 'username': '" + sNom + "', 'email': '" + sEmail
                    + "', 'faction': '" + sFaction + "', 'dateCreation': '" + dtCreation.ToString() + "', 'dateEdition': '" + dtEdition.ToString()
                    + "', 'dateSuppression': '" + dtSupression.ToString() + "'} }";
                return sJoueurJSON;
            }
            else
            {
                return sJoueurJSON = "{ 'error': { 'message': 'Combinaison (USERNAME - PASSWORD) erronée !', 'code': '401' } }";
            }
        }
        // **************************************************  ************************************************** //
        #endregion METHODES
    }
}
