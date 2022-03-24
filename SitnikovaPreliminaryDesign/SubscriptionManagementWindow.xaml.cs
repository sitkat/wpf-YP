using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using SitnikovaPreliminaryDesign.DataSet1TableAdapters;

namespace SitnikovaPreliminaryDesign
{
    public partial class SubscriptionManagementWindow : Window
    {
        DataSet1 dataSet = new DataSet1();
        AbonentTableAdapter abonentTableAdapter = new AbonentTableAdapter();
        AbonentsTableAdapter abonentsTableAdapter = new AbonentsTableAdapter();
        SexTableAdapter sexTableAdapter = new SexTableAdapter();
        User_StatusTableAdapter user_StatusTableAdapter = new User_StatusTableAdapter();
        StationCost1TableAdapter stationCost1TableAdapter = new StationCost1TableAdapter();
        StationTableAdapter stationTableAdapter = new StationTableAdapter();

        PaymentTableAdapter paymentTableAdapter = new PaymentTableAdapter();

        string ID_Sex;
        string ID_Status;
        string ID_Station;
        string password;

        string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=CityTelephony; Integrated Security = True";
        string sqlExpression = "";

        string str = "";
        public SubscriptionManagementWindow()
        {
            InitializeComponent();

            abonentTableAdapter.Fill(dataSet.Abonent);
            abonentsTableAdapter.Fill(dataSet.Abonents);
            sexTableAdapter.Fill(dataSet.Sex);
            user_StatusTableAdapter.Fill(dataSet.User_Status);
            stationTableAdapter.Fill(dataSet.Station);
            stationCost1TableAdapter.Fill(dataSet.StationCost1);

            paymentTableAdapter.Fill(dataSet.Payment);

            dataGrid.ItemsSource = dataSet.Abonents.DefaultView;
            dataGrid.SelectedValuePath = "ID_Abonent";
            dataGrid.CanUserAddRows = false;
            dataGrid.CanUserDeleteRows = false;
            dataGrid.SelectionMode = DataGridSelectionMode.Single;

            cbGenders.ItemsSource = dataSet.Sex.DefaultView;
            cbGenders.DisplayMemberPath = "Name";
            cbGenders.SelectedValuePath = "ID_Sex";

            cbBenefits.Items.Add("Льготник");
            cbBenefits.Items.Add("Без льготы");

            cbIntercity.Items.Add("С межгородом");
            cbIntercity.Items.Add("Без межгорода");

            cbUserStatuses.ItemsSource = dataSet.User_Status.DefaultView;
            cbUserStatuses.DisplayMemberPath = "Name";
            cbUserStatuses.SelectedValuePath = "ID_User_Status";

            cbStations.ItemsSource = dataSet.StationCost1.DefaultView;
            cbStations.DisplayMemberPath = "f";
            cbStations.SelectedValuePath = "ID_Station";

            cbFilter.ItemsSource = dataSet.User_Status.DefaultView;
            cbFilter.DisplayMemberPath = "Name";
            cbFilter.SelectedValuePath = "ID_User_Status";

        }
        private void Cleaner()
        {
            tbSecondName.Text = "";
            tbFirstName.Text = "";
            tbMiddleName.Text = "";
            dpBirthDate.Text = "";
            tbPersonalAccount.Text = "";
            tbNumber.Text = "";
            tbIndex.Text = "";
            tbCity.Text = "";
            tbStreet.Text = "";
            tbHouse.Text = "";
            tbFlat.Text = "";
            tbPaySum.Text = "";
            cbGenders.SelectedValue = null;
            cbBenefits.SelectedValue = null;
            cbStations.SelectedValue = null;
            cbUserStatuses.SelectedValue = null;
            cbIntercity.SelectedValue = null;
        }
        private void Filter()
        {
            if (cbFilter.SelectedValue == null)
                str = "";
            else if (int.Parse(cbFilter.SelectedValue.ToString()) == 1)
                str = "На связи";
            else if (int.Parse(cbFilter.SelectedValue.ToString()) == 2)
                str = "Заблокирован";
            sqlExpression = $"SELECT dbo.Abonent.ID_Abonent, dbo.Abonent.Surname + ' ' + dbo.Abonent.Name + ' ' + dbo.Abonent.LastName AS ФИО, dbo.Abonent.Phone_Number AS [Номер телефона], " +
                $"'Индекс:' + dbo.Abonent.Index_ + ', г.' + dbo.Abonent.City + ', ул.' + dbo.Abonent.Street + ' д.' + dbo.Abonent.House + ' кв.' + dbo.Abonent.Flat + '.' AS Адрес, dbo.Abonent.Personal_Account AS Счёт, " +
                $"dbo.Abonent.Benefit AS Льгота, dbo.Abonent.Cross_City AS Межгород, dbo.User_Status.Name AS Статус FROM dbo.Abonent INNER JOIN dbo.Station ON dbo.Abonent.ID_Station = dbo.Station.ID_Station INNER JOIN " +
                $"dbo.User_Status ON dbo.Abonent.ID_User_Status = dbo.User_Status.ID_User_Status WHERE(dbo.User_Status.Name like '%{str}%')";

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

        private void dataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            DataRowView selectedRow = (DataRowView)dataGrid.SelectedItem;
            if (selectedRow != null)
            {
                for (int j = 0; j < dataSet.Tables["Abonent"].Rows.Count; j++)
                {
                    if (dataGrid.SelectedValue.ToString() == dataSet.Tables["Abonent"].Rows[j]["ID_Abonent"].ToString())
                    {
                        tbSecondName.Text = dataSet.Tables["Abonent"].Rows[j]["Surname"].ToString();
                        tbFirstName.Text = dataSet.Tables["Abonent"].Rows[j]["Name"].ToString();
                        tbMiddleName.Text = dataSet.Tables["Abonent"].Rows[j]["LastName"].ToString();
                        dpBirthDate.Text = dataSet.Tables["Abonent"].Rows[j]["Date_Of_Birth"].ToString();
                        tbPersonalAccount.Text = selectedRow.Row.ItemArray[4].ToString();
                        tbNumber.Text = dataSet.Tables["Abonent"].Rows[j]["Phone_Number"].ToString();
                        tbIndex.Text = dataSet.Tables["Abonent"].Rows[j]["Index_"].ToString();
                        tbCity.Text = dataSet.Tables["Abonent"].Rows[j]["City"].ToString();
                        tbStreet.Text = dataSet.Tables["Abonent"].Rows[j]["Street"].ToString();
                        tbHouse.Text = dataSet.Tables["Abonent"].Rows[j]["House"].ToString();
                        tbFlat.Text = dataSet.Tables["Abonent"].Rows[j]["Flat"].ToString();
                        ID_Sex = dataSet.Tables["Abonent"].Rows[j]["ID_Sex"].ToString();
                        ID_Status = dataSet.Tables["Abonent"].Rows[j]["ID_User_Status"].ToString();
                        ID_Station = dataSet.Tables["Abonent"].Rows[j]["ID_Station"].ToString();
                        for (int i = 0; i < dataSet.Tables["Sex"].Rows.Count; i++)
                        {
                            if (ID_Sex == dataSet.Tables["Sex"].Rows[i]["ID_Sex"].ToString())
                                cbGenders.SelectedValue = dataSet.Tables["Sex"].Rows[i]["ID_Sex"].ToString();
                        }
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
                        string ben = dataSet.Tables["Abonent"].Rows[j]["Benefit"].ToString();
                        if (ben == "True")
                            cbBenefits.SelectedIndex = 0;
                        else
                            cbBenefits.SelectedIndex = 1;
                        string inter = dataSet.Tables["Abonent"].Rows[j]["Cross_City"].ToString();
                        if (inter == "True")
                            cbIntercity.SelectedIndex = 0;
                        else
                            cbIntercity.SelectedIndex = 1;
                    }
                }
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddSubscriberWindow addSubscriberWindow = new AddSubscriberWindow();
            addSubscriberWindow.Show();
            Hide();
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                DateTime dateOfBirth = Convert.ToDateTime(dpBirthDate.Text);
                DateTime dateNow = DateTime.Now;
                TimeSpan dateCheck = dateNow - dateOfBirth;
                DateTime age = DateTime.MinValue + dateCheck;
                int Years = age.Year - 1;

                string ben;
                bool forBen;

                string cross;
                bool forCross;
                if (string.IsNullOrEmpty(tbSecondName.Text) || string.IsNullOrEmpty(tbFirstName.Text) ||
                    string.IsNullOrEmpty(tbIndex.Text) || string.IsNullOrEmpty(tbCity.Text) || string.IsNullOrEmpty(tbStreet.Text) || string.IsNullOrEmpty(tbHouse.Text) ||
                    string.IsNullOrEmpty(tbFlat.Text) || string.IsNullOrEmpty(dpBirthDate.Text) ||
                    string.IsNullOrEmpty(tbNumber.Text) || string.IsNullOrEmpty(cbGenders.Text) || string.IsNullOrEmpty(cbBenefits.Text) || string.IsNullOrEmpty(cbStations.Text))
                    MessageBox.Show("Заполните все поля!");
                else if (tbIndex.Text.Length != 6)
                    MessageBox.Show("Индекс должен состоять из 6 цифр");
                else if (Years <= 14 || Years >= 120)
                    MessageBox.Show("Вы не проходите по возрасту");
                else if (tbNumber.Text.Length != 16)
                    MessageBox.Show("Введите корректный номер телефона");
                else
                {
                    for (int i = 0; i < dataSet.Tables["Abonent"].Rows.Count; i++)
                    {
                        if (dataGrid.SelectedValue.ToString() == dataSet.Tables["Abonent"].Rows[i]["ID_Abonent"].ToString())
                            password = dataSet.Tables["Abonent"].Rows[i]["Password"].ToString();
                    }
                    ben = cbBenefits.Text;
                    if (ben == "Льготник")
                        forBen = true;
                    else forBen = false;

                    cross = cbIntercity.Text;
                    if (cross == "С межгородом")
                        forCross = true;
                    else forCross = false;

                    abonentTableAdapter.UpdateQuery(tbSecondName.Text, tbFirstName.Text, tbMiddleName.Text, dpBirthDate.Text, int.Parse(cbGenders.SelectedValue.ToString()), tbNumber.Text,
                        password, tbIndex.Text, tbCity.Text, tbStreet.Text, tbHouse.Text, tbFlat.Text,
                        int.Parse(cbStations.SelectedValue.ToString()), forBen, forCross, int.Parse(cbUserStatuses.SelectedValue.ToString()), Convert.ToDecimal(tbPersonalAccount.Text), (int)dataGrid.SelectedValue);
                    abonentTableAdapter.Fill(dataSet.Abonent);
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
                    abonentTableAdapter.DeleteQuery((int)dataGrid.SelectedValue);
                    Filter();
                    Cleaner();
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
                    paymentTableAdapter.Insert((int)dataGrid.SelectedValue, null, Convert.ToDecimal(tbPaySum.Text));
                    abonentTableAdapter.UpdateSum(Convert.ToDecimal(tbPaySum.Text), (int)dataGrid.SelectedValue);
                    abonentsTableAdapter.Fill(dataSet.Abonents);
                    Cleaner();
                }

            }
        }

        private void cbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e) => Filter();
        private void btnClear_Click(object sender, RoutedEventArgs e) => cbFilter.SelectedValue = null;
        private void Window_Loaded(object sender, RoutedEventArgs e) => dataGrid.Columns[0].Visibility = Visibility.Hidden;

        #region Check
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

        private void tbSecondName_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^А-я]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void tbPersonalAccount_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9,]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void dpBirthDate_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }


        #endregion
    }
}
