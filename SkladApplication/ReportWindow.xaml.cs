using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SkladDatabase; 
using System.Windows.Shapes;

namespace SkladApplication
{
    /// <summary>
    /// Логика взаимодействия для ReportWindow.xaml
    /// </summary>
    public partial class ReportWindow : Window
    {
        int id = 0;
        public ReportWindow()
        {
            InitializeComponent();
        }
        public ReportWindow(int id)
        {
            InitializeComponent();
            this.id = id;
        }

        private void Btn_Report_Click(object sender, RoutedEventArgs e)
        {
            using (var Db = new SkladModel()) 
            {
                Db.Report(id);
            }
        }

        private void Btn_Back_Click(object sender, RoutedEventArgs e)
        {
            (new MenuWindow()).Show();
            this.Close();
        }
    }
}
