using System;
using System.Linq;
using Windows.Media.Capture;
using Windows.Phone.UI.Input;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using Siatkostat.Models;
using Siatkostat.ViewModels;

namespace Siatkostat.Statistics
{

    public sealed partial class ChoosePlayerForStats
    {
        #region Fields
        public StatusBarProgressIndicator ProgresIndicator = StatusBar.GetForCurrentView().ProgressIndicator;
        #endregion

        #region Constructor
        public ChoosePlayerForStats()
        {
            InitializeComponent();
            Loaded += ChosePlayer_Loaded;

            if (PlayersViewModel.Instance.PlayersCollection == null)
            {
                PlayersViewModel.Instance.CollectionLoaded += PlayersViewModel_CollectionLoaded;
            }
            else
            {
                PlayersViewModel_CollectionLoaded(this);
            }
            if (App.SelectedTeam != null)
            {
                TeamNameTextBlock.Text = App.SelectedTeam.TeamName;
            }
        }
        #endregion

        async void PlayersViewModel_CollectionLoaded(object sender)
        {
            PlayersListBox.ItemsSource = PlayersViewModel.Instance.PlayersCollection;
            await ProgresIndicator.HideAsync();
        }

        async void ChosePlayer_Loaded(object sender, RoutedEventArgs e)
        {
            if (PlayersListBox.ItemsSource != null)
                return;

            ProgresIndicator.Text = "Trwa łączenie z bazą danych";
            await ProgresIndicator.ShowAsync();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
        }

        async void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            e.Handled = true;
            HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
            await ProgresIndicator.HideAsync();
            Frame.Navigate(typeof(MainPage));
        }

        private void DodajButton_Click(object sender, RoutedEventArgs e)
        {
            if (PlayersListBox.SelectedItem == null)
            {
                return;
            }
             HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
            var Id = ((Player) PlayersListBox.SelectedItem).Id.ToString();
            Frame.Navigate(typeof (StatsWindow),Id);
        }
    }
}
