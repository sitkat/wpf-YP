using SitnikovaPreliminaryDesign.DataSet1TableAdapters;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using static SitnikovaPreliminaryDesign.Helper;

namespace SitnikovaPreliminaryDesign
{
    public partial class ContactsWindow : Window
    {
        DataSet1 dataSet = new DataSet1();
        AbonentTableAdapter abonentTableAdapter = new AbonentTableAdapter();
        ViewContactsAbTableAdapter viewContactsAbTableAdapter = new ViewContactsAbTableAdapter();
        OrganizationTableAdapter organizationTableAdapter = new OrganizationTableAdapter();
        ViewContactsOrgTableAdapter viewContactsOrgTable = new ViewContactsOrgTableAdapter();
        History_Calls_OrganizationTableAdapter history_Calls_OrganizationTableAdapter = new History_Calls_OrganizationTableAdapter();
        History_CallsTableAdapter history_CallsTable = new History_CallsTableAdapter();

        string abonentCity = "";
        string crossCity = "";

        string ID = "";
        string status = "";

        bool isOrg = false;
        decimal personalAccount = 0.0m;

        public ContactsWindow()
        {
            InitializeComponent();
            abonentTableAdapter.Fill(dataSet.Abonent);
            organizationTableAdapter.Fill(dataSet.Organization);
            for (int j = 0; j < dataSet.Tables["Organization"].Rows.Count; j++)
            {
                if (Saver.phoneNumber == dataSet.Tables["Organization"].Rows[j]["Phone_Number"].ToString())
                {
                    ID = dataSet.Tables["Organization"].Rows[j]["ID_Organization"].ToString();
                    status = dataSet.Tables["Organization"].Rows[j]["ID_User_Status"].ToString();
                    isOrg = true;
                    break;
                }
            }
            for (int j = 0; j < dataSet.Tables["Abonent"].Rows.Count; j++)
            {
                if (Saver.phoneNumber == dataSet.Tables["Abonent"].Rows[j]["Phone_Number"].ToString())
                {
                    ID = dataSet.Tables["Abonent"].Rows[j]["ID_Abonent"].ToString();
                    abonentCity = dataSet.Tables["Abonent"].Rows[j]["City"].ToString();
                    crossCity = dataSet.Tables["Abonent"].Rows[j]["Cross_City"].ToString();
                    status = dataSet.Tables["Abonent"].Rows[j]["ID_User_Status"].ToString();
                    personalAccount = Convert.ToDecimal(dataSet.Tables["Abonent"].Rows[j]["Personal_Account"]);
                    break;
                }
            }

            viewContactsOrgTable.FillById(dataSet.ViewContactsOrg, int.Parse(ID));
            if (crossCity == "False")
                viewContactsAbTableAdapter.FillByCity(dataSet.ViewContactsAb, abonentCity);
            else
                viewContactsAbTableAdapter.Fill(dataSet.ViewContactsAb);
            if (status == "2")
                btnCall.IsEnabled = false;


            if (isOrg == true)
            {
                dataGrid.ItemsSource = dataSet.ViewContactsOrg.DefaultView;
                dataGrid.SelectedValuePath = "ID_Otdel";
                dataGrid.CanUserAddRows = false;
                dataGrid.CanUserDeleteRows = false;
                dataGrid.SelectionMode = DataGridSelectionMode.Single;
            }
            else
            {
                dataGrid.ItemsSource = dataSet.ViewContactsAb.DefaultView;
                dataGrid.SelectedValuePath = "ID_Abonent";
                dataGrid.CanUserAddRows = false;
                dataGrid.CanUserDeleteRows = false;
                dataGrid.SelectionMode = DataGridSelectionMode.Single;
            }
        }

        private void btnCall_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)dataGrid.SelectedItem;
                string dateNow = DateTime.Now.ToString("dd-MM-yyyy");
                if (string.IsNullOrEmpty(crossCity))
                {
                    MessageBoxResult result = MessageBox.Show($"Вам звонит отдел с номером: {selectedRow.Row.ItemArray[3].ToString()}\nПринять звонок?",
       "Звонок", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        history_Calls_OrganizationTableAdapter.Insert(Convert.ToDateTime(dateNow), (int)dataGrid.SelectedValue, 1);
                    }
                    else if (result == MessageBoxResult.No)
                    {
                        history_Calls_OrganizationTableAdapter.Insert(Convert.ToDateTime(dateNow), (int)dataGrid.SelectedValue, 2);
                    }
                }
                else
                {
                    if (int.Parse(ID) == (int)dataGrid.SelectedValue)
                        MessageBox.Show("Вы не можете позвонить себе");
                    else if (personalAccount < 3)
                        MessageBox.Show($"У вас недостаточно средств для звонка в другие города!\nСредств на счете: {personalAccount}");
                    else
                    {
                        if (abonentCity != selectedRow.Row.ItemArray[4].ToString())
                        {
                            abonentTableAdapter.UpdateCallToInterCity(int.Parse(ID));
                            abonentTableAdapter.Fill(dataSet.Abonent);
                        }

                        MessageBoxResult result = MessageBox.Show($"Вам звонит абонент с номером: {selectedRow.Row.ItemArray[3].ToString()}\nПринять звонок?",
                            "Звонок", MessageBoxButton.YesNo);
                        if (result == MessageBoxResult.Yes)
                        {
                            history_CallsTable.Insert(int.Parse(ID), (int)dataGrid.SelectedValue, Convert.ToDateTime(dateNow), 1);
                        }
                        else if (result == MessageBoxResult.No)
                        {
                            history_CallsTable.Insert(int.Parse(ID), (int)dataGrid.SelectedValue, Convert.ToDateTime(dateNow), 2);
                        }

                    }
                }
            }
            else MessageBox.Show("Выберите кому вы хотите позвонить");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dataGrid.Columns[0].Visibility = Visibility.Hidden;
            dataGrid.Columns[1].Visibility = Visibility.Hidden;
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