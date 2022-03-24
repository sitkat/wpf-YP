using SitnikovaPreliminaryDesign.DataSet1TableAdapters;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace SitnikovaPreliminaryDesign
{
    public partial class OrganisationManagementWindow : Window
    {
        DataSet1 dataSet = new DataSet1();
        User_StatusTableAdapter user_StatusTableAdapter = new User_StatusTableAdapter();
        StationTableAdapter stationTableAdapter = new StationTableAdapter();
        StationCost23TableAdapter stationCost1TableAdapter = new StationCost23TableAdapter();
        OrganizationTableAdapter organizationTableAdapter = new OrganizationTableAdapter();
        OrganizationsTableAdapter organizationsTableAdapter = new OrganizationsTableAdapter();

        PaymentTableAdapter paymentTableAdapter = new PaymentTableAdapter();

        string ID_Status;
        string ID_Station;
        string password;

        string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=CityTelephony; Integrated Security = True";
        string sqlExpression = "";

        string str = "";
        public OrganisationManagementWindow()
        {
            InitializeComponent();

            organizationTableAdapter.Fill(dataSet.Organization);
            organizationsTableAdapter.Fill(dataSet.Organizations);
            user_StatusTableAdapter.Fill(dataSet.User_Status);
            stationTableAdapter.Fill(dataSet.Station);
            stationCost1TableAdapter.Fill(dataSet.StationCost23);

            paymentTableAdapter.Fill(dataSet.Payment);

            cbStations.ItemsSource = dataSet.StationCost23.DefaultView;
            cbStations.DisplayMemberPath = "f";
            cbStations.SelectedValuePath = "ID_Station";

            cbUserStatuses.ItemsSource = dataSet.User_Status.DefaultView;
            cbUserStatuses.DisplayMemberPath = "Name";
            cbUserStatuses.SelectedValuePath = "ID_User_Status";

            dataGrid.ItemsSource = dataSet.Organizations.DefaultView;
            dataGrid.SelectedValuePath = "ID_Organization";
            dataGrid.CanUserAddRows = false;
            dataGrid.CanUserDeleteRows = false;
            dataGrid.SelectionMode = DataGridSelectionMode.Single;

            cbFilter.ItemsSource = dataSet.User_Status.DefaultView;
            cbFilter.DisplayMemberPath = "Name";
            cbFilter.SelectedValuePath = "ID_User_Status";
        }

        private void Cleaner()
        {
            tbNaming.Text = "";
            tbPersonalAccount.Text = "";
            tbNumber.Text = "";
            tbIndex.Text = "";
            tbCity.Text = "";
            tbStreet.Text = "";
            tbHouse.Text = "";
            tbPaySum.Text = "";
            cbStations.SelectedValue = null;
            cbUserStatuses.SelectedValue = null;
            tbPaySum.Text = "";
        }

        private void Filter()
        {
            if (cbFilter.SelectedValue == null)
                str = "";
            else if (int.Parse(cbFilter.SelectedValue.ToString()) == 1)
                str = "На связи";
            else if (int.Parse(cbFilter.SelectedValue.ToString()) == 2)
                str = "Заблокирован";
            sqlExpression = $"SELECT dbo.Organization.ID_Organization, dbo.Organization.Name AS Название, dbo.Organization.Phone_Number AS [Номер телефона], " +
                $"dbo.Organization.Personal_Account AS Счёт, dbo.User_Status.Name AS Статус, dbo.Station.Name + ' ' + CAST(dbo.Station.Cost AS varchar) + 'р. в м.' AS Станция FROM dbo.Organization INNER JOIN " +
                $"dbo.User_Status ON dbo.Organization.ID_User_Status = dbo.User_Status.ID_User_Status INNER JOIN dbo.Station ON dbo.Organization.ID_Station = dbo.Station.ID_Station WHERE(dbo.User_Status.Name like '%{str}%')";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataAdapter da = new SqlDataAdapter(sqlExpression, connection);

                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGrid.ItemsSource = dt.DefaultView;
                dataGrid.Columns[0].Visibility = Visibility.Hidden;
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            OperatorMainMenuWindow goToOperatorMenu = new OperatorMainMenuWindow();
            goToOperatorMenu.Show();
            Hide();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddOrganisationWindow goToMenu = new AddOrganisationWindow();
            goToMenu.Show();
            Hide();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                if (string.IsNullOrEmpty(tbNaming.Text) ||
            string.IsNullOrEmpty(tbIndex.Text) || string.IsNullOrEmpty(tbCity.Text) || string.IsNullOrEmpty(tbStreet.Text) || string.IsNullOrEmpty(tbHouse.Text) ||
            string.IsNullOrEmpty(tbNumber.Text) || string.IsNullOrEmpty(cbStations.Text))
                    MessageBox.Show("Заполните все поля!");
                else if (tbIndex.Text.Length != 6)
                    MessageBox.Show("Индекс должен состоять из 6 цифр");
                else if (tbNumber.Text.Length != 16)
                    MessageBox.Show("Введите корректный номер телефона");
                else
                {
                    for (int i = 0; i < dataSet.Tables["Organization"].Rows.Count; i++)
                    {
                        if (dataGrid.SelectedValue.ToString() == dataSet.Tables["Organization"].Rows[i]["ID_Organization"].ToString())
                            password = dataSet.Tables["Organization"].Rows[i]["Password"].ToString();
                    }
                    organizationTableAdapter.UpdateQuery(tbNaming.Text, tbNumber.Text, password, tbIndex.Text, tbCity.Text,
                        tbStreet.Text, tbHouse.Text, Convert.ToDecimal(tbPersonalAccount.Text), int.Parse(cbStations.SelectedValue.ToString()), int.Parse(cbUserStatuses.SelectedValue.ToString()), (int)dataGrid.SelectedValue);
                    organizationTableAdapter.Fill(dataSet.Organization);
                    Filter();
                    Cleaner();
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                if (Convert.ToDecimal(tbPersonalAccount.Text) >= -10 && Convert.ToDecimal(tbPersonalAccount.Text) <= 10)
                {
                    MessageBoxResult result = MessageBox.Show("У данной организации есть отделы, все равно хотите удалить?",
                       "Удаление организации", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        organizationTableAdapter.DeleteQuery((int)dataGrid.SelectedValue);
                        organizationTableAdapter.Fill(dataSet.Organization);
                        Filter();
                        Cleaner();
                    }
                    else if (result == MessageBoxResult.No)
                    {
                        MessageBox.Show("Организация не удалена");
                    }

                }
                else
                    MessageBox.Show("Разрешено удалять только тех пользователей, лицевой счет которых больше -10 и меньше 10.");
            }
        }

        private void btnPay_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                if (string.IsNullOrEmpty(tbPaySum.Text))
                    MessageBox.Show("Введите сумму пополнения");
                else if (Convert.ToDecimal(tbPaySum.Text) <= 10)
                    MessageBox.Show("Минимальная сумма пополнения 10");
                else
                {
                    paymentTableAdapter.Insert(null, (int)dataGrid.SelectedValue, Convert.ToDecimal(tbPaySum.Text));
                    organizationTableAdapter.UpdateSum(Convert.ToDecimal(tbPaySum.Text), (int)dataGrid.SelectedValue);
                    organizationTableAdapter.Fill(dataSet.Organization);
                    organizationsTableAdapter.Fill(dataSet.Organizations);
                    Cleaner();
                }

            }
        }

        private void dataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            DataRowView selectedRow = (DataRowView)dataGrid.SelectedItem;
            if (selectedRow != null)
            {
                for (int j = 0; j < dataSet.Tables["Organization"].Rows.Count; j++)
                {
                    if (dataGrid.SelectedValue.ToString() == dataSet.Tables["Organization"].Rows[j]["ID_Organization"].ToString())
                    {
                        tbNaming.Text = dataSet.Tables["Organization"].Rows[j]["Name"].ToString();
                        tbPersonalAccount.Text = selectedRow.Row.ItemArray[3].ToString();
                        tbNumber.Text = dataSet.Tables["Organization"].Rows[j]["Phone_Number"].ToString();
                        tbIndex.Text = dataSet.Tables["Organization"].Rows[j]["Index_"].ToString();
                        tbCity.Text = dataSet.Tables["Organization"].Rows[j]["City"].ToString();
                        tbStreet.Text = dataSet.Tables["Organization"].Rows[j]["Street"].ToString();
                        tbHouse.Text = dataSet.Tables["Organization"].Rows[j]["House"].ToString();
                        ID_Status = dataSet.Tables["Organization"].Rows[j]["ID_User_Status"].ToString();
                        ID_Station = dataSet.Tables["Organization"].Rows[j]["ID_Station"].ToString();
                        for (int i = 0; i < dataSet.Tables["User_Status"].Rows.Count; i++)
                        {
                            if (ID_Status == dataSet.Tables["User_Status"].Rows[i]["ID_User_Status"].ToString())
                                cbUserStatuses.SelectedValue = dataSet.Tables["User_Status"].Rows[i]["ID_User_Status"].ToString();
                        }
                        for (int i = 0; i < dataSet.Tables["Station"].Rows.Count; i++)
                        {
                            if (ID_Station == dataSet.Tables["Station"].Rows[i]["ID_Station"].ToString())
                                cbStations.SelectedValue = dataSet.Tables["Station"].Rows[i]["ID_Station"].ToString();
                        }
                    }
                }
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e) => dataGrid.Columns[0].Visibility = Visibility.Hidden;
        private void cbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e) => Filter();
        private void btnClear_Click(object sender, RoutedEventArgs e) => cbFilter.SelectedValue = null;

        #region Check
        private void tbPersonalAccount_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9,]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void tbNumber_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void tbNumber_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (tbNumber.Text.Length == 1)
            {
                tbNumber.Text = "+" + tbNumber.Text;
                tbNumber.SelectionStart = tbNumber.Text.Length;
            }
            if (tbNumber.Text.Length == 2)
            {
                tbNumber.Text = tbNumber.Text + "(";
                tbNumber.SelectionStart = tbNumber.Text.Length;
            }
            if (tbNumber.Text.Length == 6)
            {
                tbNumber.Text = tbNumber.Text + ")";
                tbNumber.SelectionStart = tbNumber.Text.Length;
            }
            if (tbNumber.Text.Length == 10 || tbNumber.Text.Length == 13)
            {
                tbNumber.Text = tbNumber.Text + "-";
                tbNumber.SelectionStart = tbNumber.Text.Length;
            }
        }

        #endregion


    }
}