using System;
using Windows.Phone.UI.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using Siatkostat.Models;
using Siatkostat.ViewModels;

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

        private readonly AddTeamDialog addTeamDialog = new AddTeamDialog();
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
        private async void SignInButton_Click(object sender, RoutedEventArgs e)
        {

            if (selectedTeam == null)
            {
                var signInErrorDialog = new  MessageDialog("Nie wybrałeś drużyny") {Title = "Błąd logowania"};
                await signInErrorDialog.ShowAsync();
                return;
            }

            if (PasswordTextBox.Password.Equals(String.Empty))
            {
                var signInErrorDialog = new MessageDialog("Wpisz hasło") { Title = "Błąd logowania" };
                await signInErrorDialog.ShowAsync();
                return;
            }

            if (!PasswordTextBox.Password.Equals(selectedTeam.TeamPassword))
            {
                var signInErrorDialog = new MessageDialog("Błędne hasło") { Title = "Błąd logowania" };
                await signInErrorDialog.ShowAsync();
                return;
            }

            App.SelectedTeam = selectedTeam;
            Frame.Navigate(typeof(MainPage));
            HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
        }

        private async void ChooseTeamButton_Click(object sender, RoutedEventArgs e)
        {
            choseTeamDialog.Closing += choseTeamDialog_Closing;
            await choseTeamDialog.ShowAsync();
            
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

        private async void AddTeamButton_Click(object sender, RoutedEventArgs e)
        {
            addTeamDialog.Closing += addTeamDialog_Closing;
            await addTeamDialog.ShowAsync();
        }

      
        #endregion

        #region Handling Dialogs
        async void choseTeamDialog_Closing(Windows.UI.Xaml.Controls.ContentDialog sender, Windows.UI.Xaml.Controls.ContentDialogClosingEventArgs args)
        {
           
            await choseTeamDialog.ProgresIndicator.HideAsync();

            if (choseTeamDialog.SelectedTeam == null) 
                return;

            selectedTeam = choseTeamDialog.SelectedTeam;
            TeamNameTextBlock.Text = selectedTeam.TeamName;
        }

        void addTeamDialog_Closing(Windows.UI.Xaml.Controls.ContentDialog sender, Windows.UI.Xaml.Controls.ContentDialogClosingEventArgs args)
        {
            if (addTeamDialog.NewTeam == null)
                return;

            TeamsViewModel.Instance.InsertTeam(addTeamDialog.NewTeam);

            selectedTeam = addTeamDialog.NewTeam;
            TeamNameTextBlock.Text = selectedTeam.TeamName;
        }
        #endregion

       
    }
}
