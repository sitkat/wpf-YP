using SitnikovaPreliminaryDesign.DataSet1TableAdapters;
using System.Windows;
using static SitnikovaPreliminaryDesign.Helper;

namespace SitnikovaPreliminaryDesign
{
    public partial class CallHistoryWindow : Window
    {
        DataSet1 dataSet = new DataSet1();
        AbonentTableAdapter abonentTableAdapter = new AbonentTableAdapter();
        OrganizationTableAdapter organizationTableAdapter = new OrganizationTableAdapter();
        History_Calls_OrganizationTableAdapter history_Calls_OrganizationTableAdapter = new History_Calls_OrganizationTableAdapter();
        History_CallsTableAdapter history_CallsTable = new History_CallsTableAdapter();
        OtdelTableAdapter otdelTableAdapter = new OtdelTableAdapter();
        Call_StatusTableAdapter call_StatusTableAdapter = new Call_StatusTableAdapter();
        ViewHistoryOrgTableAdapter viewHistoryOrgTableAdapter = new ViewHistoryOrgTableAdapter();

        string ID = "";
        bool isOrg = false;
        public CallHistoryWindow()
        {
            InitializeComponent();
            abonentTableAdapter.Fill(dataSet.Abonent);
            organizationTableAdapter.Fill(dataSet.Organization);
            history_Calls_OrganizationTableAdapter.Fill(dataSet.History_Calls_Organization);
            history_CallsTable.Fill(dataSet.History_Calls);
            call_StatusTableAdapter.Fill(dataSet.Call_Status);
            for (int j = 0; j < dataSet.Tables["Organization"].Rows.Count; j++)
            {
                if (Saver.phoneNumber == dataSet.Tables["Organization"].Rows[j]["Phone_Number"].ToString())
                {
                    ID = dataSet.Tables["Organization"].Rows[j]["ID_Organization"].ToString();
                    isOrg = true;
                    otdelTableAdapter.Fill(dataSet.Otdel, int.Parse(ID));
                    break;
                }
            }
            for (int j = 0; j < dataSet.Tables["Abonent"].Rows.Count; j++)
            {
                if (Saver.phoneNumber == dataSet.Tables["Abonent"].Rows[j]["Phone_Number"].ToString())
                {
                    ID = dataSet.Tables["Abonent"].Rows[j]["ID_Abonent"].ToString();
                    history_CallsTable.FillById(dataSet.History_Calls, int.Parse(ID));
                    break;
                }
            }
            if (isOrg == true)
            {
                dataGrid.ItemsSource = dataSet.History_Calls_Organization.DefaultView;
                dataGrid.CanUserAddRows = false;
                dataGrid.CanUserDeleteRows = false;
            }
            else
            {
                dataGrid.ItemsSource = dataSet.History_Calls.DefaultView;
                dataGrid.CanUserAddRows = false;
                dataGrid.CanUserDeleteRows = false;
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (isOrg == true)
            {
                OrganisationMainMenuWindow organisationMainMenuWindow = new OrganisationMainMenuWindow();
                organisationMainMenuWindow.Show();
                Hide();
            }
            else
            {
                SubscriberMainMenuWindow subscriberMainMenuWindow = new SubscriberMainMenuWindow();
                subscriberMainMenuWindow.Show();
                Hide();
            }
        }
    }
}