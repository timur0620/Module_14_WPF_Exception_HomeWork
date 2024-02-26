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
using BankLib;

namespace Module_13_WPF_Delegat_HomeWork
{
    /// <summary>
    /// Логика взаимодействия для ConsultantWindow.xaml
    /// </summary>
    public partial class ConsultantWindow : Window
    {
        Consultant consultant = new Consultant();
        List<Consultant> consultants = new List<Consultant>();
        public ConsultantWindow()
        {
            InitializeComponent();
            consultants = consultant.GetAllClient();
            dataGrid.ItemsSource = consultants;          
        }
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            consultants = consultant.SearchClients(txtSearch.Text);
            dataGrid.ItemsSource = consultants;
        }
        private void dataGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            consultant = dataGrid.SelectedItem as Consultant;

            txtId.Text = consultant.Id.ToString();
            txtLastName.Text = consultant.LastName;
            txtName.Text = consultant.Name;
            txtsurname.Text = consultant.Surname;
            txtPhone.Text = consultant.PhoneNumber;
            txtPasport.Text = consultant.SeriesPassportNumber;
        }
        private void btnUpdatePhone_Click(object sender, RoutedEventArgs e)
        {   
            try
            {
                consultant.UpdatePhone(txtId.Text, txtPhone.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error {ex.Message}", this.Title, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
        }
    }
}
