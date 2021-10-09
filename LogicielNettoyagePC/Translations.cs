using System.Collections.Generic;

namespace LogicielNettoyagePC
{
    internal class Translations
    {
        public static Dictionary<string, string[]> Get = new()
        {
            //MainWindow
            { "mWTitle", new string[] { "Logiciel de nettoyage PC", "Computerbereinigung Applikation" } },
            { "lblTitle", new string[] { "Analyse du PC nécessaire", "PC-Scan erforderlich" } },
            { "lblTitleScanDone", new string[] { "Analyse effectuée", "Scan durschgeführt" } },
            { "lblTitleDone", new string[] { "Nettoyage terminé", "Bereinigung erledigt" } },
            { "lblSpaceToClean", new string[] { "Espace à nettoyer: ", "Platz zum reinigen: " } },
            { "lblLastScan", new string[] { "Dernière analyse: ", "letzter Scan: " } },
            { "btScan", new string[] { "Analyser", "Scannen" } },
            { "btClean", new string[] { "\n\rNettoyer", "\n\rBereinigen" } },
            { "btCleanInProgress", new string[] { "\n\rNettoyage en cours...", "\n\rBereinigung am Laufen..." } },
            { "btCleanDone", new string[] { "\n\rNettoyage terminé", "\n\rBereinigung erledigt" } },
            { "btHistory", new string[] { "\n\rHistorique", "\n\rVerlauf" } },
            { "btUpdates", new string[] { "\n\rMises à jour", "\n\rUpdates" } },
            { "btWebsite", new string[] { "\n\rSite web", "\n\rWeb Seite" } },
            { "msgUpdateInfo", new string[] { "Mise à jour", "Updates" } },
            { "msgUpToDate", new string[] { "Votre logiciel est à jour!", "Ihre Applikation ist auf dem neusten Stand!" } },
            { "msgNewUpdate", new string[] { "Une nouvelle version du logiciel est disponible.", "Eine neue Version der Software ist verfügbar." } },

            //HistoryWindow
            { "hWTitle", new string[] { "Historique", "Verlauf" } },
            { "msgDeleteInfo", new string[] { "Effacer", "Löschen" } },
            { "msgDelete", new string[] { "Êtes-vous sûre de vouloir effacer l'historique?", "Möchten Sie den Verlauf wirklich löschen?" } },
            { "gvColDate", new string[] { "Date", "Datum" } },
            { "gvColAction", new string[] { "Action", "Handlung" } },
            { "gvColRemark", new string[] { "Remarque", "Bemerkung" } },
            { "btDelete", new string[] { "Effacer", "Löschen" } },
            { "btClose", new string[] { "Fermer", "Schliessen" } }
        };
    }
}
