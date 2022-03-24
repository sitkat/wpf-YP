using System.Text.RegularExpressions;
using System.Windows;
using SitnikovaPreliminaryDesign.DataSet1TableAdapters;
using static SitnikovaPreliminaryDesign.Helper;

namespace SitnikovaPreliminaryDesign
{
    public partial class AuthorizationWindow : Window
    {
        DataSet1 dataSet = new DataSet1();
        OperatorTableAdapter operatorTableAdapter = new OperatorTableAdapter();
        AbonentTableAdapter abonentTableAdapter = new AbonentTableAdapter();
        OrganizationTableAdapter organizationTableAdapter = new OrganizationTableAdapter();

        Helper helper = new Helper();
        public AuthorizationWindow()
        {
            InitializeComponent();
            operatorTableAdapter.Fill(dataSet.Operator);
            abonentTableAdapter.Fill(dataSet.Abonent);
            organizationTableAdapter.Fill(dataSet.Organization);
        }


        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbNumber.Text) && !string.IsNullOrEmpty(pbPassword.Password.ToString()))
            {
                for (int j = 0; j < dataSet.Tables["Operator"].Rows.Count; j++)
                {
                    if (tbNumber.Text.Contains(dataSet.Tables["Operator"].Rows[j]["Phone_Number"].ToString()) && pbPassword.Password.Contains(dataSet.Tables["Operator"].Rows[j]["Password"].ToString()))
                    {
                        OperatorMainMenuWindow operatorMenu = new OperatorMainMenuWindow();
                        operatorMenu.Show();
                        Hide();
                        break;
                    }
                }

                for (int i = 0; i < dataSet.Tables["Abonent"].Rows.Count; i++)
                {
                    if (tbNumber.Text.Contains(dataSet.Tables["Abonent"].Rows[i]["Phone_Number"].ToString()) && pbPassword.Password.Contains(dataSet.Tables["Abonent"].Rows[i]["Password"].ToString()))
                    {
                        SubscriberMainMenuWindow subscriperrMenu = new SubscriberMainMenuWindow();
                        subscriperrMenu.Show();
                        Hide();
                        break;
                    }
                }

                for (int w = 0; w < dataSet.Tables["Organization"].Rows.Count; w++)
                {
                    if (tbNumber.Text.Contains(dataSet.Tables["Organization"].Rows[w]["Phone_Number"].ToString()) && pbPassword.Password.Contains(dataSet.Tables["Organization"].Rows[w]["Password"].ToString()))
                    {
                        OrganisationMainMenuWindow organisationMenu = new OrganisationMainMenuWindow();
                        organisationMenu.Show();
                        Hide();
                        break;
                    }
                }
                Saver.phoneNumber = tbNumber.Text;
            }
            else MessageBox.Show("Заполните все поля!");
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
    }
}