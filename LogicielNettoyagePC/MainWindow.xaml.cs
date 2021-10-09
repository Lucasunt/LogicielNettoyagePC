using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using LogicielNettoyagePC.Data;
using LogicielNettoyagePC.Entities;

namespace LogicielNettoyagePC
{
    public partial class MainWindow : Window
    {
        public static int LangIndex { get; set; }
        private string AppVersion => GetType().Assembly.GetName().Version.ToString();

        private readonly DirectoryInfo winTemp;
        private readonly DirectoryInfo appTemp;

        public MainWindow()
        {
            LangIndex = 0;

            InitializeComponent();

            winTemp = new DirectoryInfo(@"c:\windows\temp");
            appTemp = new DirectoryInfo(System.IO.Path.GetTempPath());

            //MessageBox.Show("Dossier cible 1: " + winTemp);
            //MessageBox.Show("Dossier cible 2: " + appTemp);

            GetDate();
            CheckForNews();
        }

        private long DirSize(DirectoryInfo dir)
        {
            return dir.GetFiles().Sum(fi => fi.Length) + dir.GetDirectories().Sum(di => DirSize(di));
        }

        private static void ClearTempData(DirectoryInfo di)
        {
            foreach (FileInfo file in di.GetFiles())
            {
                try
                {
                    file.Delete();
                    Console.WriteLine(file.FullName);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur: " + ex.Message);
                }
            }

            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                try
                {
                    dir.Delete(true);
                    Console.WriteLine(dir.FullName);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur: " + ex.Message);
                }
            }
        }
        private void BtClean_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Nettoyage en cours...");
            btClean.Content = Translations.Get["btCleanInProgress"][LangIndex];

            Clipboard.Clear();
            ClearTempData(winTemp);
            ClearTempData(appTemp);

            btClean.Content = Translations.Get["btCleanDone"][LangIndex];

            lblTitle.Content = Translations.Get["lblTitleDone"][LangIndex];
            _ = SetDirSize();

            InsertHistory("Cleaning done", string.Format("Remaining space to clean: {0} Mb", SetDirSize()));
        }
        private void BtHistory_Click(object sender, RoutedEventArgs e)
        {
            HistoryWindow hw = new();
            hw.Show();
        }

        private void BtUpdates_Click(object sender, RoutedEventArgs e)
        {
            CheckForUpdates();
        }

        private void BtWebsite_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _ = Process.Start(new ProcessStartInfo("https://weather.com/weather/today/l/47.38,8.54")
                {
                    UseShellExecute = true
                }
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur: " + ex.Message);
            }

        }

        private void BtScan_Click(object sender, RoutedEventArgs e)
        {
            AnalyseFolders();
        }

        private long SetDirSize()
        {
            long totalSize = 0;

            try
            {
                totalSize += DirSize(winTemp) / 1048576;
                totalSize += DirSize(appTemp) / 1048576;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur d'analyse: " + ex.Message);
            }

            lblSpace.Content = totalSize + " Mb";
            return totalSize;
        }

        private void AnalyseFolders()
        {
            Console.WriteLine("Début de l'analyse...");

            long totalSize = SetDirSize();

            lblTitle.Content = Translations.Get["lblTitleScanDone"][LangIndex];
            string lastAnalyse = DateTime.Today.ToString("dd/MM/yyyy");
            lblLastScanValue.Content = lastAnalyse;
            SaveDate();

            InsertHistory("Scan done", "Space to clean: " + totalSize + " Mb");
        }

        private static void SaveDate()
        {
            string date = DateTime.Today.ToString("dd/MM/yyyy");
            File.WriteAllText("date.txt", date);
        }

        private void GetDate()
        {
            if (!File.Exists("date.txt"))
            {
                return;
            }

            string dateFichier = File.ReadAllText("date.txt");

            if (dateFichier != string.Empty)
            {
                lblLastScanValue.Content = dateFichier;
            }
        }

        private void CheckForUpdates()
        {
            try
            {
                LogicielNettoyagePCContext? context = new();
                string currentVersion = context.Versions.ToList()[0].CurrentVersion.ToString();

                string msgUpdateInfo = Translations.Get["msgUpdateInfo"][LangIndex];
                string msgUpToDate = Translations.Get["msgUpToDate"][LangIndex];
                string msgNewUpdate = Translations.Get["msgNewUpdate"][LangIndex];

                if (AppVersion == currentVersion)
                {
                    _ = MessageBox.Show(msgUpToDate, msgUpdateInfo, MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    _ = MessageBox.Show(msgNewUpdate, msgUpdateInfo, MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur:" + ex.Message);
            }
        }

        private void CheckForNews()
        {
            try
            {
                LogicielNettoyagePCContext? context = new();
                int count = context.News.Where(n => n.Enabled == true).Count();
                if (count == 0)
                {
                    return;
                }

                string enabledNews = context.News.Where(n => n.Enabled).ToList().LastOrDefault().Message;

                if (enabledNews != string.Empty)
                {
                    rtBanner.Visibility = Visibility.Visible;
                    lblNews.Visibility = Visibility.Visible;
                    lblNews.Content = enabledNews;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur:" + ex.Message);
            }
        }

        private static void InsertHistory(string action, string remark)
        {
            LogicielNettoyagePCContext? context = new();

            History h = new()
            {
                Date = DateTime.Now,
                Action = action,
                Remark = remark
            };
            _ = context.Histories.Add(h);
            _ = context.SaveChanges();
        }

        private void BtLangDE_Click(object sender, RoutedEventArgs e)
        {
            SetLanguage(1);
        }

        private void BtLangFR_Click(object sender, RoutedEventArgs e)
        {
            SetLanguage(0);
        }

        private void SetLanguage(int langIndex) // FR = 0, DE = 1
        {
            LangIndex = langIndex;
            Title = Translations.Get["mWTitle"][LangIndex];
            lblTitle.Content = Translations.Get["lblTitle"][LangIndex];
            lblSpaceToClean.Content = Translations.Get["lblSpaceToClean"][LangIndex];
            lblLastScan.Content = Translations.Get["lblLastScan"][LangIndex];
            btScan.Content = Translations.Get["btScan"][LangIndex];
            btClean.Content = Translations.Get["btClean"][LangIndex];
            btHistory.Content = Translations.Get["btHistory"][LangIndex];
            btUpdates.Content = Translations.Get["btUpdates"][LangIndex];
            btWebsite.Content = Translations.Get["btWebsite"][LangIndex];
        }
    }
}

