using SitnikovaPreliminaryDesign.DataSet1TableAdapters;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using static SitnikovaPreliminaryDesign.Helper;

namespace SitnikovaPreliminaryDesign
{
    public partial class PersonalCabinetSubscriberWindow : Window
    {
        DataSet1 dataSet = new DataSet1();
        AbonentTableAdapter abonentTableAdapter = new AbonentTableAdapter();
        SexTableAdapter sexTableAdapter = new SexTableAdapter();
        User_StatusTableAdapter user_StatusTableAdapter = new User_StatusTableAdapter();
        StationCost1TableAdapter stationCost1TableAdapter = new StationCost1TableAdapter();
        StationTableAdapter stationTableAdapter = new StationTableAdapter();
        PaymentTableAdapter paymentTableAdapter = new PaymentTableAdapter();
        string ID;
        string ID_Station;
        string ID_Status;


        public PersonalCabinetSubscriberWindow()
        {
            InitializeComponent();

            abonentTableAdapter.Fill(dataSet.Abonent);
            sexTableAdapter.Fill(dataSet.Sex);
            user_StatusTableAdapter.Fill(dataSet.User_Status);
            stationTableAdapter.Fill(dataSet.Station);
            stationCost1TableAdapter.Fill(dataSet.StationCost1);

            paymentTableAdapter.Fill(dataSet.Payment);

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

            string ID_Sex;

            for (int j = 0; j < dataSet.Tables["Abonent"].Rows.Count; j++)
            {
                if (Saver.phoneNumber == dataSet.Tables["Abonent"].Rows[j]["Phone_Number"].ToString())
                {
                    ID = dataSet.Tables["Abonent"].Rows[j]["ID_Abonent"].ToString();
                    tbSecondName.Text = dataSet.Tables["Abonent"].Rows[j]["Surname"].ToString();
                    tbFirstName.Text = dataSet.Tables["Abonent"].Rows[j]["Name"].ToString();
                    tbMiddleName.Text = dataSet.Tables["Abonent"].Rows[j]["LastName"].ToString();
                    dpBirthDate.Text = dataSet.Tables["Abonent"].Rows[j]["Date_Of_Birth"].ToString();
                    tbPersonalAccount.Text = dataSet.Tables["Abonent"].Rows[j]["Personal_Account"].ToString();
                    tbPhoneNumber.Text = dataSet.Tables["Abonent"].Rows[j]["Phone_Number"].ToString();
                    tbIndex.Text = dataSet.Tables["Abonent"].Rows[j]["Index_"].ToString();
                    tbCity.Text = dataSet.Tables["Abonent"].Rows[j]["City"].ToString();
                    tbStreet.Text = dataSet.Tables["Abonent"].Rows[j]["Street"].ToString();
                    tbHouse.Text = dataSet.Tables["Abonent"].Rows[j]["House"].ToString();
                    tbFlat.Text = dataSet.Tables["Abonent"].Rows[j]["Flat"].ToString();
                    pbPassword.Password = dataSet.Tables["Abonent"].Rows[j]["Password"].ToString();
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
            if (cbIntercity.Text == "С межгородом")
                btnIntercity.Content = "Отключить";

            if (cbUserStatuses.Text == "На связи")
                btnStatus.Content = "Отключить";

        }
        private void statusUpdate()
        {
            abonentTableAdapter.Fill(dataSet.Abonent);
            for (int j = 0; j < dataSet.Tables["Abonent"].Rows.Count; j++)
            {
                if (Saver.phoneNumber == dataSet.Tables["Abonent"].Rows[j]["Phone_Number"].ToString())
                {
                    ID_Status = dataSet.Tables["Abonent"].Rows[j]["ID_User_Status"].ToString();
                    for (int i = 0; i < dataSet.Tables["User_Status"].Rows.Count; i++)
                    {
                        if (ID_Status == dataSet.Tables["User_Status"].Rows[i]["ID_User_Status"].ToString())
                            cbUserStatuses.SelectedValue = dataSet.Tables["User_Status"].Rows[i]["ID_User_Status"].ToString();
                    }
                }
            }
        }
        private void interCityUpdate()
        {
            abonentTableAdapter.Fill(dataSet.Abonent);
            for (int j = 0; j < dataSet.Tables["Abonent"].Rows.Count; j++)
            {
                if (Saver.phoneNumber == dataSet.Tables["Abonent"].Rows[j]["Phone_Number"].ToString())
                {
                    tbPersonalAccount.Text = dataSet.Tables["Abonent"].Rows[j]["Personal_Account"].ToString();
                    string inter = dataSet.Tables["Abonent"].Rows[j]["Cross_City"].ToString();
                    if (inter == "True")
                        cbIntercity.SelectedIndex = 0;
                    else
                        cbIntercity.SelectedIndex = 1;
                }

            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            SubscriberMainMenuWindow subscriberMainMenuWindow = new SubscriberMainMenuWindow();
            subscriberMainMenuWindow.Show();
            Hide();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            Regex passwordValidation = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*]){6,20}");
            string ben;
            bool forBen;

            string cross;
            bool forCross;
            if (string.IsNullOrEmpty(tbSecondName.Text) || string.IsNullOrEmpty(tbFirstName.Text) ||
                string.IsNullOrEmpty(tbIndex.Text) || string.IsNullOrEmpty(tbCity.Text) || string.IsNullOrEmpty(tbStreet.Text) || string.IsNullOrEmpty(tbHouse.Text) ||
                string.IsNullOrEmpty(tbFlat.Text) || string.IsNullOrEmpty(cbBenefits.Text) || string.IsNullOrEmpty(cbStations.Text))
                MessageBox.Show("Заполните все поля!");
            else if (!passwordValidation.IsMatch(pbPassword.Password.ToString()))
                MessageBox.Show("Пароль не соответствует требованиям: \nМинимум 6 символов\nМинимум 1 заглавная буква\nМинимум 1 цифра\nМинимум 1 спецсимвол");
            else if (tbIndex.Text.Length != 6)
                MessageBox.Show("Индекс должен состоять из 6 цифр");
            else
            {
                ben = cbBenefits.Text;
                if (ben == "Льготник")
                    forBen = true;
                else forBen = false;

                cross = cbIntercity.Text;
                if (cross == "С межгородом")
                    forCross = true;
                else forCross = false;

                abonentTableAdapter.UpdateQuery(tbSecondName.Text, tbFirstName.Text, tbMiddleName.Text, dpBirthDate.Text, int.Parse(cbGenders.SelectedValue.ToString()), tbPhoneNumber.Text,
                    pbPassword.Password, tbIndex.Text, tbCity.Text, tbStreet.Text, tbHouse.Text, tbFlat.Text,
                    int.Parse(cbStations.SelectedValue.ToString()), forBen, forCross, int.Parse(cbUserStatuses.SelectedValue.ToString()), Convert.ToDecimal(tbPersonalAccount.Text), int.Parse(ID));
            }
        }
        private void btnPay_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbPaySum.Text))
                MessageBox.Show("Введите сумму пополнения");
            else if (Convert.ToDecimal(tbPaySum.Text) <= 10)
                MessageBox.Show("Минимальная сумма пополнения 10");
            else
            {
                paymentTableAdapter.Insert(int.Parse(ID), null, Convert.ToDecimal(tbPaySum.Text));
                abonentTableAdapter.UpdateSum(Convert.ToDecimal(tbPaySum.Text), int.Parse(ID));
                abonentTableAdapter.Fill(dataSet.Abonent);
                tbPaySum.Text = "";

                for (int j = 0; j < dataSet.Tables["Abonent"].Rows.Count; j++)
                {
                    if (Saver.phoneNumber == dataSet.Tables["Abonent"].Rows[j]["Phone_Number"].ToString())
                    {
                        tbPersonalAccount.Text = dataSet.Tables["Abonent"].Rows[j]["Personal_Account"].ToString();
                    }
                }
            }
        }

        private void btnIntercity_Click(object sender, RoutedEventArgs e)
        {
            if (cbIntercity.Text == "С межгородом")
            {
                abonentTableAdapter.UpdateInterCityOff(false, int.Parse(ID));
                btnIntercity.Content = "Подключить";
            }
            else
            {
                if (Convert.ToDecimal(tbPersonalAccount.Text) >= 50)
                {
                    MessageBoxResult result = MessageBox.Show($"Подключить функцию межгород абоненту: {Saver.phoneNumber}?\nЛицевой счет: {tbPersonalAccount.Text}",
                        "Подкючение межгорода", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        abonentTableAdapter.UpdateInterCity(true, int.Parse(ID));
                        btnIntercity.Content = "Отключить";
                    }
                    else if (result == MessageBoxResult.No)
                    {
                        MessageBox.Show("Запрос отклонен"); 
                    }
                    
                }
                else MessageBox.Show("Вы не можете подключить функцию межгород.\nДля подключения данной функции, лицевой счет должен быть не меньше 50!");
            }
            interCityUpdate();
        }

        private void btnStatus_Click(object sender, RoutedEventArgs e)
        {
            if (cbUserStatuses.Text == "На связи")
            {
                abonentTableAdapter.UpdateStatus(2, int.Parse(ID));
                btnStatus.Content = "Включить";
            }
            else
            {
                abonentTableAdapter.UpdateStatus(1, int.Parse(ID));
                btnStatus.Content = "Отключить";
            }
            statusUpdate();

        }

        #region Check
        private void tbSecondName_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^А-я]+");
            e.Handled = regex.IsMatch(e.Text);
        }

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