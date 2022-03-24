using SitnikovaPreliminaryDesign.DataSet1TableAdapters;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using static SitnikovaPreliminaryDesign.Helper;

namespace SitnikovaPreliminaryDesign
{
    public partial class AddDepartmentWindow : Window
    {
        DataSet1 dataSet = new DataSet1();
        OtdelTableAdapter otdelTableAdapter = new OtdelTableAdapter();
        ViewOtdelTableAdapter viewOtdelTableAdapter = new ViewOtdelTableAdapter();
        OrganizationTableAdapter organizationTableAdapter = new OrganizationTableAdapter();

        string ID_Org;
        public AddDepartmentWindow()
        {
            InitializeComponent();

            organizationTableAdapter.Fill(dataSet.Organization);
            for (int i = 0; i < dataSet.Tables["Organization"].Rows.Count; i++)
            {
                if (Saver.phoneNumber == dataSet.Tables["Organization"].Rows[i]["Phone_Number"].ToString())
                    ID_Org = dataSet.Tables["Organization"].Rows[i]["ID_Organization"].ToString();
            }

            otdelTableAdapter.Fill(dataSet.Otdel, int.Parse(ID_Org));
            viewOtdelTableAdapter.FillById(dataSet.ViewOtdel, int.Parse(ID_Org));
            
            dataGrid.ItemsSource = dataSet.ViewOtdel.DefaultView;
            dataGrid.SelectedValuePath = "ID_Otdel";
            dataGrid.CanUserAddRows = false;
            dataGrid.CanUserDeleteRows = false;
            dataGrid.SelectionMode = DataGridSelectionMode.Single;
        }
        private void Cleaner()
        {
            tbNaming.Text = "";
            tbNumber.Text = "";
        }

        private void tbNumber_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            OrganisationMainMenuWindow organisationMainMenuWindow = new OrganisationMainMenuWindow();
            organisationMainMenuWindow.Show();
            Hide();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbNaming.Text) || string.IsNullOrEmpty(tbNumber.Text))
                MessageBox.Show("Заполните все поля!");
            else if (tbNumber.Text.Length != 4)
                MessageBox.Show("Номер должен состоять из 4 цифр");
            else
            {
                otdelTableAdapter.InsertQuery(tbNaming.Text, tbNumber.Text, int.Parse(ID_Org));
                Cleaner();
                otdelTableAdapter.Fill(dataSet.Otdel, int.Parse(ID_Org));
                viewOtdelTableAdapter.FillById(dataSet.ViewOtdel, int.Parse(ID_Org));
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                if (string.IsNullOrEmpty(tbNaming.Text) || string.IsNullOrEmpty(tbNumber.Text))
                    MessageBox.Show("Заполните все поля!");
                else if (tbNumber.Text.Length != 4)
                    MessageBox.Show("Номер должен состоять из 4 цифр");
                else
                {
                    otdelTableAdapter.UpdateQuery(tbNaming.Text, tbNumber.Text, int.Parse(ID_Org), (int)dataGrid.SelectedValue);
                    Cleaner();
                    otdelTableAdapter.Fill(dataSet.Otdel, int.Parse(ID_Org));
                    viewOtdelTableAdapter.FillById(dataSet.ViewOtdel, int.Parse(ID_Org));
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                otdelTableAdapter.DeleteQuery((int)dataGrid.SelectedValue);
                Cleaner();
                otdelTableAdapter.Fill(dataSet.Otdel, int.Parse(ID_Org));
                viewOtdelTableAdapter.FillById(dataSet.ViewOtdel, int.Parse(ID_Org));
            }
        }

        private void dataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            DataRowView selectedRow = (DataRowView)dataGrid.SelectedItem;
            if (selectedRow != null)
            {
                tbNaming.Text = selectedRow.Row.ItemArray[1].ToString();
                tbNumber.Text = selectedRow.Row.ItemArray[2].ToString();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dataGrid.Columns[0].Visibility = Visibility.Hidden;
            dataGrid.Columns[3].Visibility = Visibility.Hidden;
        }
    }
}