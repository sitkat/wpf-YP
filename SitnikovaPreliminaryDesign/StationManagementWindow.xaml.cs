using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using SitnikovaPreliminaryDesign.DataSet1TableAdapters;
namespace SitnikovaPreliminaryDesign
{
    public partial class StationManagementWindow : Window
    {
        DataSet1 dataSet = new DataSet1();
        StationTableAdapter stationTableAdapter = new StationTableAdapter();
        ViewStationTableAdapter viewStationTableAdapter = new ViewStationTableAdapter();
        ATSTableAdapter atsTableAdapter = new ATSTableAdapter();

        string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=CityTelephony; Integrated Security = True";
        string sqlExpression = "";

        string str = "";
        public StationManagementWindow()
        {
            InitializeComponent();
            stationTableAdapter.Fill(dataSet.Station);
            viewStationTableAdapter.Fill(dataSet.ViewStation);
            atsTableAdapter.Fill(dataSet.ATS);

            dataGrid.ItemsSource = dataSet.ViewStation.DefaultView;
            dataGrid.SelectedValuePath = "ID_Station";
            dataGrid.CanUserAddRows = false;
            dataGrid.CanUserDeleteRows = false;
            dataGrid.SelectionMode = DataGridSelectionMode.Single;

            cbATS.ItemsSource = dataSet.ATS.DefaultView;
            cbATS.DisplayMemberPath = "Name";
            cbATS.SelectedValuePath = "ID_ATS";
        }

        private void Cleaner()
        {
            tbName.Text = "";
            tbCity.Text = "";
            tbStreet.Text = "";
            tbHouse.Text = "";
            tbCost.Text = "";
            cbATS.SelectedValue = null;

        }
        private void Search()
        {
            sqlExpression = $"SELECT dbo.Station.ID_Station, dbo.Station.Name AS Название, dbo.Station.Cost AS Стоимость, " +
                $"dbo.ATS.Name AS АТС, dbo.Station.City AS Город, dbo.Station.Street AS Улица, dbo.Station.House AS Дом, dbo.Station.ID_ATS FROM dbo.Station INNER JOIN " +
                $"dbo.ATS ON dbo.Station.ID_ATS = dbo.ATS.ID_ATS WHERE(dbo.Station.Name LIKE '%{tbFilter.Text}%')";

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
            bool can = false;
            for (int j = 0; j < dataSet.Tables["Station"].Rows.Count; j++)
            {
                if (tbName.Text == dataSet.Tables["Station"].Rows[j]["Name"].ToString())
                {
                    MessageBox.Show("Станция с таким названием уже существует!");
                    can = false;
                    break;
                }
                else
                {
                    can = true;
                }
            }
            if (string.IsNullOrEmpty(tbName.Text) || string.IsNullOrEmpty(tbCity.Text) || string.IsNullOrEmpty(tbCity.Text) || string.IsNullOrEmpty(tbStreet.Text) || string.IsNullOrEmpty(tbHouse.Text) || string.IsNullOrEmpty(tbCost.Text) ||
                string.IsNullOrEmpty(cbATS.Text))
                MessageBox.Show("Заполните все поля!");
            else if (cbATS.Text == "Городская" && (double.Parse(tbCost.Text) > 1000 || double.Parse(tbCost.Text) < 100))
                MessageBox.Show("Стоимость услуги при городской АТС должна быть от 100 до 1000");
            else if (cbATS.Text == "Ведомственная" && (double.Parse(tbCost.Text) > 5000 || double.Parse(tbCost.Text) < 500))
                MessageBox.Show("Стоимость услуги при ведомственной АТС должна быть от 500 до 5000");
            else if (cbATS.Text == "Учрежденческая" && (double.Parse(tbCost.Text) > 15000 || double.Parse(tbCost.Text) < 1000))
                MessageBox.Show("Стоимость услуги при учрежденческой АТС должна быть от 1000 до 15000");
            else
            {
                if (can == true)
                {
                    stationTableAdapter.InsertQuery(tbName.Text, decimal.Parse(tbCost.Text), int.Parse(cbATS.SelectedValue.ToString()), tbCity.Text, tbStreet.Text, tbHouse.Text);
                    Cleaner();
                    stationTableAdapter.Fill(dataSet.Station);
                    Search();
                    can = false;
                }
            }

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                bool can = false;
                for (int j = 0; j < dataSet.Tables["Station"].Rows.Count; j++)
                {
                    if (tbName.Text == dataSet.Tables["Station"].Rows[j]["Name"].ToString())
                    {
                        MessageBox.Show("Станция с таким названием уже существует!");
                        can = false;
                        break;
                    }
                    else
                    {
                        can = true;
                    }
                }
                if (string.IsNullOrEmpty(tbName.Text) || string.IsNullOrEmpty(tbCity.Text) || string.IsNullOrEmpty(tbCity.Text) || string.IsNullOrEmpty(tbStreet.Text) || string.IsNullOrEmpty(tbHouse.Text) || string.IsNullOrEmpty(tbCost.Text) ||
                    string.IsNullOrEmpty(cbATS.Text))
                    MessageBox.Show("Заполните все поля!");
                else if (cbATS.Text == "Городская" && (double.Parse(tbCost.Text) > 1000 || double.Parse(tbCost.Text) < 100))
                    MessageBox.Show("Стоимость услуги при городской АТС должна быть от 100 до 1000");
                else if (cbATS.Text == "Ведомственная" && (double.Parse(tbCost.Text) > 5000 || double.Parse(tbCost.Text) < 500))
                    MessageBox.Show("Стоимость услуги при ведомственной АТС должна быть от 500 до 5000");
                else if (cbATS.Text == "Учрежденческая" && (double.Parse(tbCost.Text) > 15000 || double.Parse(tbCost.Text) < 1000))
                    MessageBox.Show("Стоимость услуги при учрежденческой АТС должна быть от 1000 до 15000");
                else
                {
                    if (can == true)
                    {
                        stationTableAdapter.UpdateQuery(tbName.Text, decimal.Parse(tbCost.Text), int.Parse(cbATS.SelectedValue.ToString()), tbCity.Text, tbStreet.Text, tbHouse.Text, (int)dataGrid.SelectedValue);
                        stationTableAdapter.Fill(dataSet.Station);
                        Search();
                        Cleaner();
                        can = false;
                    }

                }
            }
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                stationTableAdapter.DeleteQuery((int)dataGrid.SelectedValue);
                Search();
                Cleaner();
            }
        }

        private void dataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            DataRowView selectedRow = (DataRowView)dataGrid.SelectedItem;
            if (selectedRow != null)
            {
                tbName.Text = selectedRow.Row.ItemArray[1].ToString();
                tbCost.Text = selectedRow.Row.ItemArray[2].ToString();
                cbATS.SelectedValue = selectedRow.Row.ItemArray[7].ToString();
                tbCity.Text = selectedRow.Row.ItemArray[4].ToString();
                tbStreet.Text = selectedRow.Row.ItemArray[5].ToString();
                tbHouse.Text = selectedRow.Row.ItemArray[6].ToString();
            }
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dataGrid.Columns[0].Visibility = Visibility.Hidden;
            dataGrid.Columns[7].Visibility = Visibility.Hidden;
        }

        private void tbHouse_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9,]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void tbFilter_TextChanged(object sender, TextChangedEventArgs e) => Search();
    }
}