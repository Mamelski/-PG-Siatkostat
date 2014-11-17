using System;
using Windows.Phone.UI.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using Siatkostat.Models;

namespace Siatkostat.Authentication
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SignInPage
    {
        #region Fields
        private Team selectedTeam;

        private readonly ChoseTeamDialog choseTeamDialog = new ChoseTeamDialog();
        #endregion

        #region Constructor
        public SignInPage()
        {   
            InitializeComponent();
        }
        #endregion

        #region Navigation
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
        }
        #endregion

        #region Button Events
        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {

            if (selectedTeam == null)
            {
                var signInErrorDialog = new  MessageDialog("Nie wybrałeś drużyny") {Title = "Błąd logowania"};
                signInErrorDialog.ShowAsync();
                return;
            }

            if (PasswordTextBox.Password.Equals(String.Empty))
            {
                var signInErrorDialog = new MessageDialog("Wpisz hasło") { Title = "Błąd logowania" };
                signInErrorDialog.ShowAsync();
                return;
            }

            if (!PasswordTextBox.Password.Equals(selectedTeam.TeamPassword))
            {
                var signInErrorDialog = new MessageDialog("Błędne hasło") { Title = "Błąd logowania" };
                signInErrorDialog.ShowAsync();
                return;
            }

            App.SelectedTeam = selectedTeam;
            Frame.Navigate(typeof(MainPage));
        }

        private void ChooseTeamButton_Click(object sender, RoutedEventArgs e)
        {
            choseTeamDialog.Closing += choseTeamDialog_Closing;
            choseTeamDialog.ShowAsync();
            
        }
        private void QuestInButton_Click(object sender, RoutedEventArgs e)
        {
            App.SelectedTeam = null;
            Frame.Navigate(typeof(MainPage));
        }

        void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            e.Handled = true;
            HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
            Application.Current.Exit();
        }
        #endregion

        #region Handling choseTeamDialog
        void choseTeamDialog_Closing(Windows.UI.Xaml.Controls.ContentDialog sender, Windows.UI.Xaml.Controls.ContentDialogClosingEventArgs args)
        {
           
            choseTeamDialog.ProgresIndicator.HideAsync();

            if (choseTeamDialog.SelectedTeam == null) 
                return;

            selectedTeam = choseTeamDialog.SelectedTeam;
            TeamNameTextBlock.Text = selectedTeam.TeamName;
        }
        #endregion

    }
}
