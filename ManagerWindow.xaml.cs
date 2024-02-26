using Module_13_WPF_Delegat_HomeWork.Model;
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
using System.Windows.Shapes;

namespace Module_13_WPF_Delegat_HomeWork
{
    /// <summary>
    /// Логика взаимодействия для ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {   
        Manager manager = new Manager();
        StandartAccount account = new StandartAccount();

        public static event Func<string, string, string> LogingEvent;
        public ManagerWindow()
        {
            InitializeComponent();
            dataGrid.ItemsSource = manager.GetAllClient();
            LogingEvent += account.LogingFunction;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            manager.SearchClients(txtSearch.Text);
        }
        private void dataGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            manager = dataGrid.SelectedItem as Manager;
        }
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {  
            try
            {
                manager.UpdateAllData(txtId.Text, txtLastName.Text, txtName.Text, txtSurname.Text, txtPhone.Text, txtPasport.Text);
                string log = LogingEvent?.Invoke("Manager", "Update Data Client");
                MessageBox.Show(log, this.Title, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch  (Exception ex)
            {
                MessageBox.Show($"Error {ex.Message}", this.Title, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {   
            try
            {
                manager.CreateNewClient(txtLastName.Text, txtName.Text, txtSurname.Text, txtPhone.Text, txtPasport.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error {ex.Message}", this.Title, MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }
            dataGrid.ItemsSource = manager.GetAllClient();

            string log = LogingEvent?.Invoke("Manager", "Create Client");

            MessageBox.Show(log, this.Title, MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void dataGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {   
            try
            {
                dataGridAcc.ItemsSource = account.GetAllAccountsClientId(txtId.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error {ex.Message}", this.Title, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
        private void btnCreateAccount_Click(object sender, RoutedEventArgs e)
        {   
            try
            {   
                account.CreateAccount(txtId.Text, txtTotalSumAcc.Text, "standart");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error {ex.Message}", this.Title, MessageBoxButton.OK, MessageBoxImage.Error);
                return ;
            }
            
            dataGridAcc.ItemsSource = account.GetAllAccountsClientId(txtId.Text);

            string log = LogingEvent?.Invoke("Manager", "Create account");
            MessageBox.Show(log, this.Title, MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void btnTransfer_Click(object sender, RoutedEventArgs e)
        {
            TransferWindow transferWindow = new TransferWindow();
            transferWindow.Show();
        }
        private void btnDeleteAccount_Click(object sender, RoutedEventArgs e)
        {  
            try
            {
                account.DeleteAccount(txtIdAcc.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error {ex.Message}", this.Title, MessageBoxButton.OK, MessageBoxImage.Error);
                return ;
            }
            
            dataGridAcc.ItemsSource = account.GetAllAccounts("standart");

            string log = LogingEvent?.Invoke("Manager", "Delete Account");
            MessageBox.Show(log, this.Title, MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
