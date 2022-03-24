using SitnikovaPreliminaryDesign.DataSet1TableAdapters;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using static SitnikovaPreliminaryDesign.Helper;

namespace SitnikovaPreliminaryDesign
{
    public partial class PersonalCabinetOrganisationWindow : Window
    {
        DataSet1 dataSet = new DataSet1();
        OrganizationTableAdapter organizationTableAdapter = new OrganizationTableAdapter();
        User_StatusTableAdapter user_StatusTableAdapter = new User_StatusTableAdapter();
        StationCost23TableAdapter stationCost1TableAdapter = new StationCost23TableAdapter();
        StationTableAdapter stationTableAdapter = new StationTableAdapter();
        PaymentTableAdapter paymentTableAdapter = new PaymentTableAdapter();

        string ID;
        string ID_Station;
        string ID_Status;

        public PersonalCabinetOrganisationWindow()
        {
            InitializeComponent();
            organizationTableAdapter.Fill(dataSet.Organization);
            user_StatusTableAdapter.Fill(dataSet.User_Status);
            stationTableAdapter.Fill(dataSet.Station);
            stationCost1TableAdapter.Fill(dataSet.StationCost23);
            paymentTableAdapter.Fill(dataSet.Payment);

            cbUserStatuses.ItemsSource = dataSet.User_Status.DefaultView;
            cbUserStatuses.DisplayMemberPath = "Name";
            cbUserStatuses.SelectedValuePath = "ID_User_Status";

            cbStations.ItemsSource = dataSet.StationCost23.DefaultView;
            cbStations.DisplayMemberPath = "f";
            cbStations.SelectedValuePath = "ID_Station";

            for (int j = 0; j < dataSet.Tables["Organization"].Rows.Count; j++)
            {
                if (Saver.phoneNumber == dataSet.Tables["Organization"].Rows[j]["Phone_Number"].ToString())
                {
                    ID = dataSet.Tables["Organization"].Rows[j]["ID_Organization"].ToString();
                    tbName.Text = dataSet.Tables["Organization"].Rows[j]["Name"].ToString();
                    tbPersonalAccount.Text = dataSet.Tables["Organization"].Rows[j]["Personal_Account"].ToString();
                    tbPhoneNumber.Text = dataSet.Tables["Organization"].Rows[j]["Phone_Number"].ToString();
                    tbIndex.Text = dataSet.Tables["Organization"].Rows[j]["Index_"].ToString();
                    tbCity.Text = dataSet.Tables["Organization"].Rows[j]["City"].ToString();
                    tbStreet.Text = dataSet.Tables["Organization"].Rows[j]["Street"].ToString();
                    tbHouse.Text = dataSet.Tables["Organization"].Rows[j]["House"].ToString();
                    pbPassword.Password = dataSet.Tables["Organization"].Rows[j]["Password"].ToString();
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
            if (cbUserStatuses.Text == "На связи")
                btnStatus.Content = "Отключить";
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            OrganisationMainMenuWindow organisationMainMenuWindow = new OrganisationMainMenuWindow();
            organisationMainMenuWindow.Show();
            Hide();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Regex passwordValidation = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*]){6,20}");
            if (string.IsNullOrEmpty(tbName.Text) ||
            string.IsNullOrEmpty(tbIndex.Text) || string.IsNullOrEmpty(tbCity.Text) || string.IsNullOrEmpty(tbStreet.Text) || string.IsNullOrEmpty(tbHouse.Text) || string.IsNullOrEmpty(cbStations.Text))
                MessageBox.Show("Заполните все поля!");
            else if (!passwordValidation.IsMatch(pbPassword.Password.ToString()))
                MessageBox.Show("Пароль не соответствует требованиям: \nМинимум 6 символов\nМинимум 1 заглавная буква\nМинимум 1 цифра\nМинимум 1 спецсимвол");
            else if (tbIndex.Text.Length != 6)
                MessageBox.Show("Индекс должен состоять из 6 цифр");
            else
            {
                organizationTableAdapter.UpdateQuery(tbName.Text, tbPhoneNumber.Text, pbPassword.Password, tbIndex.Text, tbCity.Text,
                    tbStreet.Text, tbHouse.Text, Convert.ToDecimal(tbPersonalAccount.Text), int.Parse(cbStations.SelectedValue.ToString()), int.Parse(cbUserStatuses.SelectedValue.ToString()), int.Parse(ID));
                organizationTableAdapter.Fill(dataSet.Organization);
            }
        }

        private void statusUpdate()
        {
            organizationTableAdapter.Fill(dataSet.Organization);
            for (int j = 0; j < dataSet.Tables["Organization"].Rows.Count; j++)
            {
                if (Saver.phoneNumber == dataSet.Tables["Organization"].Rows[j]["Phone_Number"].ToString())
                {
                    ID_Status = dataSet.Tables["Organization"].Rows[j]["ID_User_Status"].ToString();
                    for (int i = 0; i < dataSet.Tables["User_Status"].Rows.Count; i++)
                    {
                        if (ID_Status == dataSet.Tables["User_Status"].Rows[i]["ID_User_Status"].ToString())
                            cbUserStatuses.SelectedValue = dataSet.Tables["User_Status"].Rows[i]["ID_User_Status"].ToString();
                    }
                }
            }
        }

        private void btnStatus_Click(object sender, RoutedEventArgs e)
        {
            if (cbUserStatuses.Text == "На связи")
            {
                organizationTableAdapter.UpdateStatus(2, int.Parse(ID));
                btnStatus.Content = "Включить";
            }
            else
            {
                organizationTableAdapter.UpdateStatus(1, int.Parse(ID));
                btnStatus.Content = "Отключить";
            }
            statusUpdate();
        }

        private void btnPay_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbPaySum.Text))
                MessageBox.Show("Введите сумму пополнения");
            else if (Convert.ToDecimal(tbPaySum.Text) <= 10)
                MessageBox.Show("Минимальная сумма пополнения 10");
            else
            {
                paymentTableAdapter.Insert(null, int.Parse(ID), Convert.ToDecimal(tbPaySum.Text));
                organizationTableAdapter.UpdateSum(Convert.ToDecimal(tbPaySum.Text), int.Parse(ID));
                organizationTableAdapter.Fill(dataSet.Organization);
                tbPaySum.Text = "";

                for (int j = 0; j < dataSet.Tables["Organization"].Rows.Count; j++)
                {
                    if (Saver.phoneNumber == dataSet.Tables["Organization"].Rows[j]["Phone_Number"].ToString())
                    {
                        tbPersonalAccount.Text = dataSet.Tables["Organization"].Rows[j]["Personal_Account"].ToString();
                    }
                }
            }
        }

        #region Check
        private void tbIndex_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void tbPaySum_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9,]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        #endregion
    }
}