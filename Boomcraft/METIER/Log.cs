using System;
using System.IO;
using System.Diagnostics;

namespace Boomcraft.METIER
{
    public class Log
    {
        //*****************************************************************************************************************
        // Cette classe permet d'écrire des traces dans un fichier afin d'évaluer le fonctionnement.
        //*****************************************************************************************************************
        //*****************************************************************************************************************
        #region VARIABLES
        // Chemin du fichier contenant les logs de l'application.
        private string sFichierChemin;
        //  Variable pour récupérer l'activation des logs.
        string sLogState;

        //Path.GetTempPath()
        #endregion VARIABLES
        //*****************************************************************************************************************
        #region CONSTRUCTEURS
        public Log()
        {
            //  Récupération de l'état d'activation des logs.
            sLogState = System.Configuration.ConfigurationManager.AppSettings["logState"];
            if (System.Environment.MachineName == "PC-TIM")
            {
                //  Récupération de l'adresse du répertoire du projet sur la machine.
                sFichierChemin = AppDomain.CurrentDomain.BaseDirectory + @"Log\boomcraft_log.txt";
            }
            else
            {
                //  Récupération de l'adresse du répertoire Temp de la machine.
                sFichierChemin += Path.GetTempPath() + "boomcraft_log.txt";
            }
            // Vérification de l'existence du fichier de Log.
            if (!File.Exists(sFichierChemin))
            {
                Debug.WriteLine("KO - Le fichier de logs n'existait pas. Il a été créé.\n Chemin : " + sFichierChemin);
                // Le fichier n'existe pas. On le crée.
                File.Create(sFichierChemin);
            }
        }
        #endregion CONSTRUCTEURS
        //*****************************************************************************************************************
        #region METHODES ET PROPRIETES
        //*****************************************************************************************************************
        public void ecrire(string sMessage)
        {
            if (sLogState == "true")
            {
                // Ecriture dans le fichier de Log lors de l'absence d'un tuple en base.
                File.AppendAllText(sFichierChemin, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\t\t" + sMessage + "\n");
            }
        }
        //*****************************************************************************************************************
        #endregion METHODES ET PROPRIETES
        //*****************************************************************************************************************
    }
}