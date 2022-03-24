using SitnikovaPreliminaryDesign.DataSet1TableAdapters;
using System;
using System.Windows;

namespace SitnikovaPreliminaryDesign
{
    public partial class OperatorMainMenuWindow : Window
    {
        DataSet1 dataSet = new DataSet1();
        AbonentTableAdapter abonentTableAdapter = new AbonentTableAdapter();
        StationTableAdapter stationTableAdapter = new StationTableAdapter();
        OrganizationTableAdapter organizationTableAdapter = new OrganizationTableAdapter();

        string ID = "";
        string ID_Station = "";
        decimal stationCost = 0.0m;
        decimal personalAccount = 0.0m;
        string crossCity = "";
        string benefit = "";

        public OperatorMainMenuWindow()
        {
            InitializeComponent();
            abonentTableAdapter.Fill(dataSet.Abonent);
            stationTableAdapter.Fill(dataSet.Station);
            organizationTableAdapter.Fill(dataSet.Organization);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            AuthorizationWindow authorizationWindow = new AuthorizationWindow();
            authorizationWindow.Show();
            Hide();
        }

        private void btnSubscriptionManager_Click(object sender, RoutedEventArgs e)
        {
            SubscriptionManagementWindow goToManagement = new SubscriptionManagementWindow();
            goToManagement.Show();
            Hide();
        }

        private void btnOrgManager_Click(object sender, RoutedEventArgs e)
        {
            OrganisationManagementWindow goToManagement = new OrganisationManagementWindow();
            goToManagement.Show();
            Hide();
        }

        private void btnStationManager_Click(object sender, RoutedEventArgs e)
        {
            StationManagementWindow goToManagement = new StationManagementWindow();
            goToManagement.Show();
            Hide();
        }

        private void btnAllPayments_Click(object sender, RoutedEventArgs e)
        {
            AllPaymentsWindow goToManagement = new AllPaymentsWindow();
            goToManagement.Show();
            Hide();
        }

        private void takeCostStation()
        {
            for (int i = 0; i < dataSet.Tables["Station"].Rows.Count; i++)
            {
                if (ID_Station == dataSet.Tables["Station"].Rows[i]["ID_Station"].ToString())
                    stationCost = Convert.ToDecimal(dataSet.Tables["Station"].Rows[i]["Cost"]);
            }
        }

        private void btnDoPay_Click(object sender, RoutedEventArgs e)
        {
            for (int j = 0; j < dataSet.Tables["Abonent"].Rows.Count; j++)
            {
                ID = dataSet.Tables["Abonent"].Rows[j]["ID_Abonent"].ToString();
                crossCity = dataSet.Tables["Abonent"].Rows[j]["Cross_City"].ToString();
                benefit = dataSet.Tables["Abonent"].Rows[j]["Benefit"].ToString();
                personalAccount = Convert.ToDecimal(dataSet.Tables["Abonent"].Rows[j]["Personal_Account"]);
                ID_Station = dataSet.Tables["Abonent"].Rows[j]["ID_Station"].ToString();
                takeCostStation();
                if (crossCity == "True" && benefit == "True")
                {
                    if (personalAccount >= (stationCost + 50) / 2)
                        abonentTableAdapter.UpdatePayment((stationCost + 50) / 2, 1, int.Parse(ID));
                    else abonentTableAdapter.UpdatePayment(0, 2, int.Parse(ID));
                }
                else if (crossCity == "True" && benefit == "False")
                {
                    if (personalAccount >= (stationCost + 50) / 1)
                        abonentTableAdapter.UpdatePayment((stationCost + 50) / 1, 1, int.Parse(ID));
                    else abonentTableAdapter.UpdatePayment(0, 2, int.Parse(ID));
                }
                else if (crossCity == "False" && benefit == "True")
                {
                    if (personalAccount >= stationCost / 2)
                        abonentTableAdapter.UpdatePayment(stationCost / 2, 1, int.Parse(ID));
                    else abonentTableAdapter.UpdatePayment(0, 2, int.Parse(ID));
                }
                else if (crossCity == "False" && benefit == "False")
                {
                    if (personalAccount >= stationCost)
                        abonentTableAdapter.UpdatePayment(stationCost, 1, int.Parse(ID));
                    else abonentTableAdapter.UpdatePayment(0, 2, int.Parse(ID));
                }
            }
            for (int j = 0; j < dataSet.Tables["Organization"].Rows.Count; j++)
            {
                ID = dataSet.Tables["Organization"].Rows[j]["ID_Organization"].ToString();
                personalAccount = Convert.ToDecimal(dataSet.Tables["Organization"].Rows[j]["Personal_Account"]);
                ID_Station = dataSet.Tables["Organization"].Rows[j]["ID_Station"].ToString();
                takeCostStation();
                if (personalAccount >= stationCost)
                    organizationTableAdapter.UpdatePaymentOrg(stationCost, 1, int.Parse(ID));
                else organizationTableAdapter.UpdatePaymentOrg(0, 2, int.Parse(ID));
            }
            MessageBox.Show("Произведена абонентская плата");
        }
    }
}