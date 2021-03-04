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
        List<int> productId = new List<int>();
        List<int> quantityProduct = new List<int>();
        public MenuWindow()
        {
            InitializeComponent();
            UpdateTable();
            UpdateComboBox();
        }
        public MenuWindow(string user)
        {
            using (var Db = new SkladModel())
            {
                InitializeComponent();
                UpdateTable();
                UpdateComboBox();
                if (user == "User")
                {
                    AdminPanelProduct.Visibility = Visibility.Hidden;
                    AdminPanelEmployee.Visibility = Visibility.Hidden;
                    AdminPanelOperatoin.Visibility = Visibility.Hidden;
                    AdminPanelType.Visibility = Visibility.Hidden;
                    AdminPanelUnit.Visibility = Visibility.Hidden;
                    UserPanel.Visibility = Visibility.Hidden;
                }
            }
        }

        private void UpdateTable() 
        {
            using (var Db = new SkladModel()) 
            {
                DGR_Employee.ItemsSource = null;
                DGR_Operation.ItemsSource = null;
                DGR_Users.ItemsSource = null;
                DGR_Unit.ItemsSource = null;
                DGR_Type.ItemsSource = null;
                DGR_Product.ItemsSource = null;

                DGR_Employee.ItemsSource = Db.GetAllEmployee();
                DGR_Operation.ItemsSource = Db.GetAllOperation();
                DGR_Users.ItemsSource = Db.GetAllUser();
                DGR_Unit.ItemsSource = Db.GetAllUnit_Of_Measurement();
                DGR_Type.ItemsSource = Db.GetAllProduct_Type();
                DGR_Product.ItemsSource = Db.GetAllProduct();
            }
        }
        private void UpdateComboBox() 
        {
            using (var Db = new SkladModel()) 
            {
                CB_Empl.ItemsSource = null;
                CB_Product.ItemsSource = null;
                CB_Product_Type.ItemsSource = null;
                CB_Unit_Of_Measurement.ItemsSource = null;
                CB_Oper.Items.Clear();
                CB_Status.Items.Clear();

                CB_Oper.Items.Add("Покупка");
                CB_Oper.Items.Add("Продажа");
                CB_Status.Items.Add("Админ");
                CB_Status.Items.Add("Юзер");

                CB_Empl.ItemsSource = Db.GetAllEmployee();
                CB_Product.ItemsSource = Db.GetAllProduct();
                CB_Product_Type.ItemsSource = Db.GetAllProduct_Type();
                CB_Unit_Of_Measurement.ItemsSource = Db.GetAllUnit_Of_Measurement();
            }
        }

        #region Add
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
                UpdateTable();
                UpdateComboBox();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void AddUnit_Click(object sender, RoutedEventArgs e)
        {
            using (var DB = new SkladModel())
            {
                MessageBox.Show(DB.AddUnit_Of_Measurement(TB_TitleFull.Text, TB_TitleShort.Text));
                UpdateTable();
                UpdateComboBox();
            }
        }
        private void AddType_Click(object sender, RoutedEventArgs e)
        {
            using (var DB = new SkladModel())
            {
                MessageBox.Show(DB.AddProduct_Type(TB_Title_Type.Text));
            }
            UpdateTable();
            UpdateComboBox();
        }
        private void AddEmploee_Click(object sender, RoutedEventArgs e)
        {
            using (var DB = new SkladModel())
            {
                MessageBox.Show(DB.AddEmployee(TB_FirstNameE.Text, TB_LastNameE.Text, TB_MidleNameE.Text));
            }
            UpdateTable();
            UpdateComboBox();
        }
        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            using (var DB = new SkladModel())
            {
                MessageBox.Show(DB.AddProduct(TB_TitleP.Text, Convert.ToDecimal(TB_Price.Text)
                    , Convert.ToInt32(CB_Product_Type.SelectedValue), Convert.ToInt32(CB_Unit_Of_Measurement.SelectedValue)
                    , Convert.ToInt32(TB_QuantityP.Text)));
            }
            UpdateTable();
            UpdateComboBox();
        }
        private void AddOperation_Click(object sender, RoutedEventArgs e)
        {
            using (var DB = new SkladModel())
            {
                if (CB_Oper.SelectedItem.ToString() == "Покупка")
                {
                    MessageBox.Show(DB.AddOperation(SkladTable.OperationStatus.Purchase, TB_Doc.Text, Convert.ToInt32(TB_NumD.Text),
                       quantityProduct, DP_Date_Of_Completion.SelectedDate,
                        Convert.ToInt32(CB_Empl.SelectedValue), productId));
                    quantityProduct.Clear();
                    productId.Clear();
                }
                if (CB_Oper.SelectedItem.ToString() == "Продажа")
                {
                    MessageBox.Show(DB.AddOperation(SkladTable.OperationStatus.Sale, TB_Doc.Text, Convert.ToInt32(TB_NumD.Text),
                        quantityProduct, DP_Date_Of_Completion.SelectedDate,
                        Convert.ToInt32(CB_Empl.SelectedValue),
                       productId));
                    quantityProduct.Clear();
                    productId.Clear();
                }
            }
            UpdateTable();
            UpdateComboBox();
        }
        #endregion

        #region Edit
        private void EditUser_Click(object sender, RoutedEventArgs e)
        {
            using (var DB = new SkladModel())
            {
                if (CB_Status.SelectedItem.ToString() == "Админ")
                {
                    MessageBox.Show(DB.EditUser(Convert.ToInt32(DGR_Users.SelectedValue), TB_Login.Text, TB_Password.Text, SkladTable.Status.Admin));
                }
                else if (CB_Status.SelectedItem.ToString() == "Юзер")
                {
                    MessageBox.Show(DB.EditUser(Convert.ToInt32(DGR_Users.SelectedValue), TB_Login.Text, TB_Password.Text, SkladTable.Status.User));
                }
            }
            UpdateTable();
            UpdateComboBox();
        }
        private void EditUnit_Click(object sender, RoutedEventArgs e)
        {
            using (var DB = new SkladModel())
            {
                MessageBox.Show(DB.EditUnit_Of_Measurement(Convert.ToInt32(DGR_Unit.SelectedValue), TB_TitleFull.Text, TB_TitleShort.Text));
            }
            UpdateTable();
            UpdateComboBox();
        }
        private void EditType_Click(object sender, RoutedEventArgs e)
        {
            using (var DB = new SkladModel())
            {
                MessageBox.Show(DB.EditProduct_Type(Convert.ToInt32(DGR_Type.SelectedValue), TB_Title_Type.Text));
            }
            UpdateTable();
            UpdateComboBox();
        }
        private void EditEmploee_Click(object sender, RoutedEventArgs e)
        {
            using (var DB = new SkladModel())
            {
                MessageBox.Show(DB.EditEmployee(Convert.ToInt32(DGR_Employee.SelectedValue), TB_FirstNameE.Text, TB_LastNameE.Text, TB_MidleNameE.Text));
            }
            UpdateTable();
            UpdateComboBox();
        }
        private void EditProduct_Click(object sender, RoutedEventArgs e)
        {
            using (var DB = new SkladModel())
            {
                MessageBox.Show(DB.EditProduct(Convert.ToInt32(DGR_Product.SelectedValue), TB_TitleP.Text, Convert.ToDecimal(TB_Price.Text)
                    , Convert.ToInt32(CB_Product_Type.SelectedValue), Convert.ToInt32(CB_Unit_Of_Measurement.SelectedValue)
                    , Convert.ToInt32(TB_QuantityP.Text)));
            }
            UpdateTable();
            UpdateComboBox();
        }
        private void EditOperation_Click(object sender, RoutedEventArgs e)
        {
            //using (var DB = new SkladModel())
            //{
            //    MessageBox.Show(DB.EditOperation(Convert.ToInt32(DGR_Operation.SelectedValue), TB_Doc.Text, Convert.ToInt32(TB_NumD.Text),
            //         DP_Date_Of_Completion.SelectedDate,
            //        Convert.ToInt32(CB_Empl.SelectedValue),
            //        quantityProduct));
            //}
            //UpdateTable();
            //UpdateComboBox();
        }
        #endregion

        #region Remove
        private void RemoveUser_Click(object sender, RoutedEventArgs e)
        {
            using (var DB = new SkladModel())
            {
                MessageBox.Show(DB.RemoveUser(Convert.ToInt32(DGR_Users.SelectedValue)));
            }
            UpdateTable();
            UpdateComboBox();
        }
        private void RemoveUnit_Click(object sender, RoutedEventArgs e)
        {
            using (var DB = new SkladModel())
            {
                MessageBox.Show(DB.RemoveUnit_Of_Measurement(Convert.ToInt32(DGR_Unit.SelectedValue)));
            }
            UpdateTable();
            UpdateComboBox();
        }
        private void RemoveType_Click(object sender, RoutedEventArgs e)
        {
            using (var DB = new SkladModel())
            {
                MessageBox.Show(DB.RemoveProduct_Type(Convert.ToInt32(DGR_Type.SelectedValue)));
            }
            UpdateTable();
            UpdateComboBox();
        }
        private void RemoveEmploee_Click(object sender, RoutedEventArgs e)
        {
            using (var DB = new SkladModel())
            {
                MessageBox.Show(DB.RemoveEmployee(Convert.ToInt32(DGR_Employee.SelectedValue)));
            }
            UpdateTable();
            UpdateComboBox();
        }
        private void RemoveProduct_Click(object sender, RoutedEventArgs e)
        {
            using (var DB = new SkladModel())
            {
                MessageBox.Show(DB.RemoveProduct(Convert.ToInt32(DGR_Product.SelectedValue)));
            }
            UpdateTable();
            UpdateComboBox();
        }
        private void RemoveOperation_Click(object sender, RoutedEventArgs e)
        {
            using (var DB = new SkladModel())
            {
                MessageBox.Show(DB.RemoveOperation(Convert.ToInt32(DGR_Operation.SelectedValue)));
            }
            UpdateTable();
            UpdateComboBox();
        }
        #endregion
        private void TB_Quantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CB_Product.SelectedValue != null)
            {
                using (var DB = new SkladModel())
                {
                    decimal count = 0;
                      count = count+ DB.ResultCount(Convert.ToInt32(CB_Product.SelectedValue), 
                        Convert.ToInt32(TB_Quantity.Text));
                    TB_Result.Text = Convert.ToString(count);
                }
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
        private void ReportOPeration_Click(object sender, RoutedEventArgs e)
        {
            (new ReportWindow(Convert.ToInt32(DGR_Operation.SelectedValue))).Show();
            this.Close();           
        }

        private void AddProductInOperation_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new SkladModel())
            {
                if (db.count(Convert.ToInt32(TB_Quantity.Text)))
                {
                    if (db.qua(Convert.ToInt32(TB_Quantity.Text), Convert.ToInt32(CB_Product.SelectedValue)) && CB_Oper.SelectedItem.ToString() == "Продажа")
                    {
                        productId.Add(Convert.ToInt32(CB_Product.SelectedValue));
                        quantityProduct.Add(Convert.ToInt32(TB_Quantity.Text));
                    }
                    else if (CB_Oper.SelectedItem.ToString() == "Покупка") 
                    {
                        productId.Add(Convert.ToInt32(CB_Product.SelectedValue));
                        quantityProduct.Add(Convert.ToInt32(TB_Quantity.Text));
                    }
                    else MessageBox.Show("На складе недостаточно товара.");
                }
                else MessageBox.Show("Количество меньше или равно 0");
            }
            MessageBox.Show("Товар добавлен");
        }
    }
}
