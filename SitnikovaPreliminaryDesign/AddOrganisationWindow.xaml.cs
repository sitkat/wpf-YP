using SitnikovaPreliminaryDesign.DataSet1TableAdapters;
using System.Text.RegularExpressions;
using System.Windows;

namespace SitnikovaPreliminaryDesign
{
    public partial class AddOrganisationWindow : Window
    {
        DataSet1 dataSet = new DataSet1();
        User_StatusTableAdapter user_StatusTableAdapter = new User_StatusTableAdapter();
        StationTableAdapter stationTableAdapter = new StationTableAdapter();
        StationCost23TableAdapter stationCost1TableAdapter = new StationCost23TableAdapter();
        OrganizationTableAdapter organizationTableAdapter = new OrganizationTableAdapter();

        public AddOrganisationWindow()
        {
            InitializeComponent();

            organizationTableAdapter.Fill(dataSet.Organization);
            user_StatusTableAdapter.Fill(dataSet.User_Status);
            stationTableAdapter.Fill(dataSet.Station);
            stationCost1TableAdapter.Fill(dataSet.StationCost23);

            cbStations.ItemsSource = dataSet.StationCost23.DefaultView;
            cbStations.DisplayMemberPath = "f";
            cbStations.SelectedValuePath = "ID_Station";
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Regex passwordValidation = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*]){6,20}");
            if (string.IsNullOrEmpty(tbNaming.Text) || string.IsNullOrEmpty(pbPassword.Password.ToString()) ||
                string.IsNullOrEmpty(tbIndex.Text) || string.IsNullOrEmpty(tbCity.Text) || string.IsNullOrEmpty(tbStreet.Text) || string.IsNullOrEmpty(tbHouse.Text) ||
                string.IsNullOrEmpty(tbNumber.Text) || string.IsNullOrEmpty(cbStations.Text))
                MessageBox.Show("Заполните все поля!");
            else if (!passwordValidation.IsMatch(pbPassword.Password.ToString()))
                MessageBox.Show("Пароль не соответствует требованиям: \nМинимум 6 символов\nМинимум 1 заглавная буква\nМинимум 1 цифра\nМинимум 1 спецсимвол");
            else if (tbIndex.Text.Length != 6)
                MessageBox.Show("Индекс должен состоять из 6 цифр");
            else if (tbNumber.Text.Length != 16)
                MessageBox.Show("Введите корректный номер телефона");
            else
            {
                organizationTableAdapter.InsertQuery(tbNaming.Text, tbNumber.Text, pbPassword.Password.ToString(), tbIndex.Text, tbCity.Text,
                    tbStreet.Text, tbHouse.Text, 0.0m, int.Parse(cbStations.SelectedValue.ToString()), 2);

                OrganisationManagementWindow goToMenu = new OrganisationManagementWindow();
                goToMenu.Show();
                Hide();
            }
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            OrganisationManagementWindow goToMenu = new OrganisationManagementWindow();
            goToMenu.Show();
            Hide();
        }

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
        #endregion


    }
}