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

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var Db = new SkladModel())
                {
                    if (CB_Status.SelectedItem.ToString() == "Админ")
                    {
                        MessageBox.Show(Db.AddUser(TB_Login.Text, TB_Password.Text, SkladTable.Status.Admin));
                    }
                    else if (CB_Status.SelectedItem.ToString() == "Юзер")
                    {
                        MessageBox.Show(Db.AddUser(TB_Login.Text, TB_Password.Text, SkladTable.Status.User));
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void EditUser_Click(object sender, RoutedEventArgs e)
        {
            using (var DB = new SkladModel()) 
            {
                if (CB_Status.SelectedItem.ToString() == "Админ")
                {
                    MessageBox.Show(DB.EditUser(Convert.ToInt32(DGR_Users.SelectedValue),TB_Login.Text, TB_Password.Text, SkladTable.Status.Admin));
                }
                else if (CB_Status.SelectedItem.ToString() == "Юзер")
                {
                    MessageBox.Show(DB.EditUser(Convert.ToInt32(DGR_Users.SelectedValue),TB_Login.Text, TB_Password.Text, SkladTable.Status.User));
                }
            }
        }

        private void RemoveUser_Click(object sender, RoutedEventArgs e)
        {
            using (var DB = new SkladModel())
            {
               MessageBox.Show(DB.RemoveUser(Convert.ToInt32(DGR_Users.SelectedValue)));
            }
        }

        private void AddUnit_Click(object sender, RoutedEventArgs e)
        {
            using (var DB = new SkladModel()) 
            {
                MessageBox.Show(DB.AddUnit_Of_Measurement(TB_TitleFull.Text,TB_TitleShort.Text));
            }
        }

        private void EditUnit_Click(object sender, RoutedEventArgs e)
        {
            using (var DB = new SkladModel())
            {
                MessageBox.Show(DB.EditUnit_Of_Measurement(Convert.ToInt32(DGR_Unit.SelectedValue),TB_TitleFull.Text, TB_TitleShort.Text));
            }
        }

        private void RemoveUnit_Click(object sender, RoutedEventArgs e)
        {
            using (var DB = new SkladModel())
            {
                MessageBox.Show(DB.RemoveUnit_Of_Measurement(Convert.ToInt32(DGR_Unit.SelectedValue)));
            }
        }

        private void AddType_Click(object sender, RoutedEventArgs e)
        {
            using (var DB = new SkladModel())
            {
                MessageBox.Show(DB.AddProduct_Type(TB_Title_Type.Text));
            }
        }

        private void EditType_Click(object sender, RoutedEventArgs e)
        {
            using (var DB = new SkladModel())
            {
                MessageBox.Show(DB.EditProduct_Type(Convert.ToInt32(DGR_Type.SelectedValue),TB_Title_Type.Text));
            }
        }

        private void RemoveType_Click(object sender, RoutedEventArgs e)
        {
            using (var DB = new SkladModel())
            {
                MessageBox.Show(DB.RemoveUnit_Of_Measurement(Convert.ToInt32(DGR_Type.SelectedValue)));
            }
        }

        private void AddEmploee_Click(object sender, RoutedEventArgs e)
        {
            using (var DB = new SkladModel())
            {
                MessageBox.Show(DB.AddEmployee(TB_FirstNameE.Text,TB_LastNameE.Text,TB_MidleNameE.Text));
            }
        }

        private void EditEmploee_Click(object sender, RoutedEventArgs e)
        {
            using (var DB = new SkladModel())
            {
                MessageBox.Show(DB.EditEmployee(Convert.ToInt32(DGR_Employee.SelectedValue), TB_FirstNameE.Text, TB_LastNameE.Text, TB_MidleNameE.Text));
            }
        }

        private void RemoveEmploee_Click(object sender, RoutedEventArgs e)
        {
            using (var DB = new SkladModel())
            {
                MessageBox.Show(DB.RemoveEmployee(Convert.ToInt32(DGR_Employee.SelectedValue)));
            }
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            using (var DB = new SkladModel())
            {
                MessageBox.Show(DB.AddProduct(TB_TitleP.Text,Convert.ToDecimal(TB_Price.Text)
                    ,Convert.ToInt32(CB_Product_Type.SelectedValue),Convert.ToInt32(CB_Unit_Of_Measurement.SelectedValue)
                    ,Convert.ToInt32(TB_QuantityP.Text)));
            }
        }

        private void TB_Quantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(CB_Product.SelectedValue != null) 
            { }
            using (var DB = new SkladModel())
            {
                TB_Result.Text = DB.ResultCount(Convert.ToInt32(CB_Product.SelectedValue), Convert.ToInt32(TB_Quantity.Text)).ToString();
            }
        }

        private void CB_Empl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TB_Quantity.Text != "")
            {
                using (var DB = new SkladModel())
                {
                    TB_Result.Text = DB.ResultCount(Convert.ToInt32(CB_Product.SelectedValue), Convert.ToInt32(TB_Quantity.Text)).ToString();
                }
            }
        }

        private void EditProduct_Click(object sender, RoutedEventArgs e)
        {
            using (var DB = new SkladModel())
            {
                MessageBox.Show(DB.EditProduct(Convert.ToInt32(DGR_Product.SelectedValue),TB_TitleP.Text, Convert.ToDecimal(TB_Price.Text)
                    , Convert.ToInt32(CB_Product_Type.SelectedValue), Convert.ToInt32(CB_Unit_Of_Measurement.SelectedValue)
                    , Convert.ToInt32(TB_QuantityP.Text)));
            }
        }

        private void RemoveProduct_Click(object sender, RoutedEventArgs e)
        {
            using (var DB = new SkladModel())
            {
                MessageBox.Show(DB.RemoveProduct(Convert.ToInt32(DGR_Product.SelectedValue)));
            }
        }

        private void AddOperation_Click(object sender, RoutedEventArgs e)
        {
            using (var DB = new SkladModel())
            {
                if (CB_Oper.SelectedItem.ToString() == "Покупка")
                {
                    MessageBox.Show(DB.AddOperation(SkladTable.OperationStatus.Purchase,TB_Doc.Text,Convert.ToInt32(TB_NumD.Text),
                        Convert.ToInt32(TB_Quantity.Text),DP_Date_Of_Completion.SelectedDate,
                        Convert.ToInt32(CB_Empl.SelectedValue),Convert.ToInt32(CB_Product.SelectedValue)));
                }
                if (CB_Oper.SelectedItem.ToString() == "Продажа")
                {
                    MessageBox.Show(DB.AddOperation(SkladTable.OperationStatus.Sale, TB_Doc.Text, Convert.ToInt32(TB_NumD.Text),
                        Convert.ToInt32(TB_Quantity.Text), DP_Date_Of_Completion.SelectedDate,
                        Convert.ToInt32(CB_Empl.SelectedValue),
                        Convert.ToInt32(CB_Product.SelectedValue)));
                }
            }
        }

        private void EditOperation_Click(object sender, RoutedEventArgs e)
        {
            using (var DB = new SkladModel())
            {
                MessageBox.Show(DB.EditOperation(Convert.ToInt32(DGR_Operation.SelectedValue), TB_Doc.Text, Convert.ToInt32(TB_NumD.Text),
                    Convert.ToInt32(TB_Quantity.Text),DP_Date_Of_Completion.SelectedDate,
                    Convert.ToInt32(CB_Empl.SelectedValue),
                    Convert.ToInt32(CB_Product.SelectedValue)));
            }
        }

        private void RemoveOperation_Click(object sender, RoutedEventArgs e)
        {
            using (var DB = new SkladModel())
            {
                MessageBox.Show(DB.RemoveOperation(Convert.ToInt32(DGR_Operation.SelectedValue)));
            }
        }
    }
}
