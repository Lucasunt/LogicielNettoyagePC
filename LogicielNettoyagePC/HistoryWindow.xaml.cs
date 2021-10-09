using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using LogicielNettoyagePC.Data;
using LogicielNettoyagePC.Entities;

namespace LogicielNettoyagePC
{
    public partial class HistoryWindow : Window
    {
        public HistoryWindow()
        {
            InitializeComponent();
            LoadHistory();
            SetLanguage();
        }

        private void LoadHistory()
        {
            try
            {
                LogicielNettoyagePCContext? context = new();
                IOrderedEnumerable<History>? history = context.Histories.ToList().OrderByDescending(h => h.Date);
                lvHistory.ItemsSource = history;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur:" + ex.Message);
            }
        }

        private void DeleteHistory()
        {
            try
            {
                LogicielNettoyagePCContext? context = new();
                IEnumerable<History> list = context.Histories.ToList();
                context.Histories.RemoveRange(list);
                _ = context.SaveChanges();

                lvHistory.ItemsSource = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur:" + ex.Message);
            }
        }

        private void BtClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtDelete_Click(object sender, RoutedEventArgs e)
        {
            string msgDelete = Translations.Get["msgDelete"][MainWindow.LangIndex];
            string msgDeleteInfo = Translations.Get["msgDeleteInfo"][MainWindow.LangIndex];

            MessageBoxResult result = MessageBox.Show(msgDelete, msgDeleteInfo, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                DeleteHistory();
            }
        }

        private void SetLanguage()
        {
            Title = Translations.Get["hWTitle"][MainWindow.LangIndex];
            lblTitle.Content = Translations.Get["hWTitle"][MainWindow.LangIndex];
            gvColDate.Header = Translations.Get["gvColDate"][MainWindow.LangIndex];
            gvColAction.Header = Translations.Get["gvColAction"][MainWindow.LangIndex];
            gvColRemark.Header = Translations.Get["gvColRemark"][MainWindow.LangIndex];
            btDelete.Content = Translations.Get["btDelete"][MainWindow.LangIndex];
            btClose.Content = Translations.Get["btClose"][MainWindow.LangIndex];
        }
    }
}
