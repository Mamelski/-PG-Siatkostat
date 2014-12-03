using System;
using Windows.Phone.UI.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml.Navigation;
using Siatkostat.EditTeam;
using Siatkostat.Models;
using Siatkostat.ViewModels;

namespace Siatkostat
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewMatch
    {
        public NewMatch()
        {
            InitializeComponent();
        }

        void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            e.Handled = true;
            HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
            Frame.Navigate(typeof(MainPage));
        }
        
        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
        }

        private async void ContinueButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(OpponentNameTextBox.Text))
            {
                await new MessageDialog("Musisz podać nazwę przeciwnej drużyny!").ShowAsync();
                return;
            }
            if (String.IsNullOrEmpty(MatchPlaceTextBox.Text))
            {
                await new MessageDialog("Musisz podać miejsce meczu!").ShowAsync();
                return;
            }

            Match newMatch = new Match
            {
                OponentName = OpponentNameTextBox.Text,
                MatchDate = MatchDateTextBox.Date.Date,
            };

            if (App.SelectedTeam != null)
                newMatch.TeamId = App.SelectedTeam.Id;

            newMatch.CreateTeamStatistics();
            MatchViewModel.Instance.CurrentMatch = newMatch;


            Frame.Navigate(App.SelectedTeam == null ? typeof (EditTeamPage) : typeof (EfficiencyThreshold));
        }
    }
}
