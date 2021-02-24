using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using SkladDatabase;
using System.Windows.Shapes;

namespace SkladApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void BtnAutorization_Click(object sender, RoutedEventArgs e)
        {
            bool key = false;
            using (var Db = new SkladModel()) 
            {
                var user = Db.Users.Where(l => l.Login == TB_Log.Text && l.Password == PasswordB.Password);
                if ( user != null) 
                {
                    foreach (var us in user) 
                    {
                        if (us.Status == SkladTable.Status.Admin) 
                        {
                            key = true;
                            (new MenuWindow()).Show();
                            this.Close();
                        }
                    }
                }
            }
            if (key == false) MessageBox.Show("Неверный логин или пароль");
        }
    }
}
