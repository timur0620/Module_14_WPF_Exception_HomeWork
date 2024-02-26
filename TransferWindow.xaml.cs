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
    /// Логика взаимодействия для TransferWindow.xaml
    /// </summary>
    public partial class TransferWindow : Window
    {   
        StandartAccount account = new StandartAccount();
        public TransferWindow()
        {
            InitializeComponent();
        }

        private void btnRunTrasfer_Click(object sender, RoutedEventArgs e)
        {   try
            {
                account.TransferActive(txtSender.Text, txtBen.Text, txtSum.Text);
                txtDone.Text = "DONE";
            }
            catch (Exception ex)
            {
                txtDone.Text = "Error";
            }

        }
    }
}
