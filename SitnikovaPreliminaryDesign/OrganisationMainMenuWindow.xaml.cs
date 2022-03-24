using System.Windows;

namespace SitnikovaPreliminaryDesign
{
    public partial class OrganisationMainMenuWindow : Window
    {
        public OrganisationMainMenuWindow()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            AuthorizationWindow authorizationWindow = new AuthorizationWindow();
            authorizationWindow.Show();
            Hide();
        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            PersonalCabinetOrganisationWindow personalCabinetOrganisationWindow = new PersonalCabinetOrganisationWindow();
            personalCabinetOrganisationWindow.Show();
            Hide();
        }

        private void btnOtdel_Click(object sender, RoutedEventArgs e)
        {
            AddDepartmentWindow addDepartmentWindow = new AddDepartmentWindow();
            addDepartmentWindow.Show();
            Hide();
        }

        private void btnContacts_Click(object sender, RoutedEventArgs e)
        {
            ContactsWindow contactsWindow = new ContactsWindow();
            contactsWindow.Show();
            Hide();
        }

        private void btnHistory_Click(object sender, RoutedEventArgs e)
        {
            CallHistoryWindow callHistoryWindow = new CallHistoryWindow();
            callHistoryWindow.Show();
            Hide();
        }
    }
}