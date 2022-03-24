using SitnikovaPreliminaryDesign.DataSet1TableAdapters;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Office.Interop.Excel;

namespace SitnikovaPreliminaryDesign
{
    public partial class AllPaymentsWindow : System.Windows.Window
    {
        DataSet1 dataSet = new DataSet1();
        PaymentTableAdapter paymentTableAdapter = new PaymentTableAdapter();

        ViewPaymentAbonentTableAdapter viewPaymentAbonentTable = new ViewPaymentAbonentTableAdapter();
        ViewPaymentOrgTableAdapter viewPaymentOrgTableAdapter = new ViewPaymentOrgTableAdapter();
        public AllPaymentsWindow()
        {
            InitializeComponent();
            paymentTableAdapter.Fill(dataSet.Payment);

            viewPaymentAbonentTable.Fill(dataSet.ViewPaymentAbonent);
            viewPaymentOrgTableAdapter.Fill(dataSet.ViewPaymentOrg);

            dataGrid1.ItemsSource = dataSet.ViewPaymentAbonent.DefaultView;
            dataGrid2.ItemsSource = dataSet.ViewPaymentOrg.DefaultView;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            OperatorMainMenuWindow goToOperatorMenu = new OperatorMainMenuWindow();
            goToOperatorMenu.Show();
            Hide();
        }


        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dataGrid1.Columns[0].Visibility = Visibility.Hidden;
            dataGrid2.Columns[0].Visibility = Visibility.Hidden;
        }
    }
}