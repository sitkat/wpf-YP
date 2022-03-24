using System;
using System.Text.RegularExpressions;
using System.Windows;
using SitnikovaPreliminaryDesign.DataSet1TableAdapters;

namespace SitnikovaPreliminaryDesign
{
    public partial class AddSubscriberWindow : Window
    {
        DataSet1 dataSet = new DataSet1();
        AbonentTableAdapter abonentTableAdapter = new AbonentTableAdapter();
        SexTableAdapter sexTableAdapter = new SexTableAdapter();
        User_StatusTableAdapter user_StatusTableAdapter = new User_StatusTableAdapter();
        StationTableAdapter stationTableAdapter = new StationTableAdapter();
        StationCost1TableAdapter stationCost1TableAdapter = new StationCost1TableAdapter();
        public AddSubscriberWindow()
        {
            InitializeComponent();
            abonentTableAdapter.Fill(dataSet.Abonent);
            sexTableAdapter.Fill(dataSet.Sex);
            user_StatusTableAdapter.Fill(dataSet.User_Status);
            stationTableAdapter.Fill(dataSet.Station);
            stationCost1TableAdapter.Fill(dataSet.StationCost1);

            cbGenders.ItemsSource = dataSet.Sex.DefaultView;
            cbGenders.DisplayMemberPath = "Name";
            cbGenders.SelectedValuePath = "ID_Sex";

            cbStations.ItemsSource = dataSet.StationCost1.DefaultView;
            cbStations.DisplayMemberPath = "f";
            cbStations.SelectedValuePath = "ID_Station";

            cbBenefits.Items.Add("Льготник");
            cbBenefits.Items.Add("Без льготы");
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            SubscriberMainMenuWindow goToMenu = new SubscriberMainMenuWindow();
            goToMenu.Show();
            Hide();
        }

        private void btnAddAbonent_Click(object sender, RoutedEventArgs e)
        {
            string ben;
            bool forBen;
            Regex passwordValidation = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*]){6,20}");
            DateTime dateOfBirth = Convert.ToDateTime(dpBirthdate.Text);
            DateTime dateNow = DateTime.Now;
            TimeSpan dateCheck = dateNow - dateOfBirth;
            DateTime age = DateTime.MinValue + dateCheck;
            int Years = age.Year - 1;
               if (string.IsNullOrEmpty(tbSecondName.Text) || string.IsNullOrEmpty(tbFirstName.Text) || string.IsNullOrEmpty(pbPassword.Password.ToString()) ||
                string.IsNullOrEmpty(tbIndex.Text) || string.IsNullOrEmpty(tbCity.Text) || string.IsNullOrEmpty(tbStreet.Text) || string.IsNullOrEmpty(tbHouse.Text) ||
                string.IsNullOrEmpty(tbFlat.Text) || string.IsNullOrEmpty(dpBirthdate.Text) ||
                string.IsNullOrEmpty(tbNumber.Text) || string.IsNullOrEmpty(cbGenders.Text)|| string.IsNullOrEmpty(cbBenefits.Text) || string.IsNullOrEmpty(cbStations.Text))
                MessageBox.Show("Заполните все поля!");
            else if (!passwordValidation.IsMatch(pbPassword.Password.ToString()))
                MessageBox.Show("Пароль не соответствует требованиям: \nМинимум 6 символов\nМинимум 1 заглавная буква\nМинимум 1 цифра\nМинимум 1 спецсимвол");
            else if (tbIndex.Text.Length != 6)
                MessageBox.Show("Индекс должен состоять из 6 цифр");
            else if (Years <= 14 || Years >= 120)
                MessageBox.Show("Вы не проходите по возрасту");
            else if (tbNumber.Text.Length != 16)
                MessageBox.Show("Введите корректный номер телефона");
            else
            {
                ben = cbBenefits.Text;
                if (ben == "Льготник")
                    forBen = true;
                else forBen = false;
                abonentTableAdapter.InsertQuery(tbSecondName.Text, tbFirstName.Text, tbMiddleName.Text, dpBirthdate.Text, int.Parse(cbGenders.SelectedValue.ToString()), tbNumber.Text, pbPassword.Password.ToString(), tbIndex.Text, tbCity.Text,
                    tbStreet.Text,tbHouse.Text,tbFlat.Text,int.Parse(cbStations.SelectedValue.ToString()),forBen,false,2,0.0m);

                SubscriptionManagementWindow Manager = new SubscriptionManagementWindow();
                Manager.Show();
                Hide();
            }
        }
        //TODO
        private void btnCheck_Click(object sender, RoutedEventArgs e)
        {

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

        private void dpBirthdate_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
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