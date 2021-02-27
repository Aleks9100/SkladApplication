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
            using (var Db = new SkladModel()) 
            {
                var user = Db.Users.FirstOrDefault(l => l.Login == TB_Log.Text && l.Password == PasswordB.Password);
                if (user is null)
                {
                    MessageBox.Show("Неверный логин или пароль");
                    return;
                } 
                if (user.Status == SkladTable.Status.Admin)
                {
                    (new MenuWindow()).Show();
                    this.Close();
                }
            }
        }
    }
}
