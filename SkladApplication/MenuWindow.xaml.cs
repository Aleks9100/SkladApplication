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
using System.Windows.Shapes;
using SkladDatabase;

namespace SkladApplication
{
    /// <summary>
    /// Логика взаимодействия для MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        public MenuWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var Db = new SkladModel())
                {
                    if (CB_Status.SelectedValue.ToString() == "Админ")
                    {
                        MessageBox.Show(Db.AddUser(TB_Login.Text, TB_Password.Text, SkladTable.Status.Admin));
                    }
                    else if (CB_Status.SelectedValue.ToString() == "Юзер")
                    {
                        MessageBox.Show(Db.AddUser(TB_Login.Text, TB_Password.Text, SkladTable.Status.User));
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
