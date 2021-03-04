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
        int idOperation = 0;
        public ReportWindow()
        {
            InitializeComponent();
        }
        public ReportWindow(int id)
        {
            InitializeComponent();
            idOperation = id;          
            using (var Db = new SkladModel()) 
            {
                DGR_Product.ItemsSource = null;
                DGR_Product.ItemsSource = Db.GetOperationProduct(idOperation);    
            }
        }

        private void Btn_Report_Click(object sender, RoutedEventArgs e)
        {
            using (var Db = new SkladModel()) 
            {
                Db.Report(idOperation);
            }
        }

        private void Btn_Back_Click(object sender, RoutedEventArgs e)
        {
            (new MenuWindow()).Show();
            this.Close();
        }
    }
}
