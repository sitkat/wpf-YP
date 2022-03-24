using System.Windows;

namespace SitnikovaPreliminaryDesign
{
    public partial class SubscriberMainMenuWindow : Window
    {
        public SubscriberMainMenuWindow()
        {
            InitializeComponent();
        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            PersonalCabinetSubscriberWindow personalCabinetSubscriberWindow = new PersonalCabinetSubscriberWindow();
            personalCabinetSubscriberWindow.Show();
            Hide();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            AuthorizationWindow authorizationWindow = new AuthorizationWindow();
            authorizationWindow.Show();
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