using System;
using System.Linq;
using Windows.Phone.UI.Input;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using Siatkostat.ViewModels;

namespace Siatkostat.Statistics
{

    public sealed partial class SelectCriterion
    {
        #region Fields
        public StatusBarProgressIndicator ProgresIndicator = StatusBar.GetForCurrentView().ProgressIndicator;
        #endregion

        #region Constructor
        public SelectCriterion()
        {
            InitializeComponent();
        }
        #endregion

        #region Navigation
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
        }

        void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            e.Handled = true;
            HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
            Frame.Navigate(typeof(MainPage));
        }
        #endregion

        #region Button
        private async void KontynuujButton_Click(object sender, RoutedEventArgs e)
        {
            if (StartDate.Date > EndDate.Date.AddDays(1))
            {
                var errorDialog = new MessageDialog("Początek okresu musi być przed końcem") { Title = "Błąd Daty" };
                await errorDialog.ShowAsync();
                return;
            }

            if (MatchViewModel.Instance.MatchesCollection == null)
            {
                ProgresIndicator.Text = "Trwa przeszukiwanie bazy danych";
                await ProgresIndicator.ShowAsync();
                MatchViewModel.Instance.CollectionLoaded += Instance_CollectionLoaded;
                return;
            }

            FilterMatches();
        }
        #endregion

        #region Filtrowanie meczu
        private async void FilterMatches()
        {
            var filteredMatches =
              MatchViewModel.Instance.MatchesCollection.Where(
                  match => match.MatchDate <= EndDate.Date && match.MatchDate >= StartDate.Date);

            await ProgresIndicator.HideAsync();
            if (!filteredMatches.Any())
            {
                var errorDialog = new MessageDialog("Niestety w wybranym okresie nie rozegrano żadnych spotkań") { Title = "Błąd Daty" };
                await errorDialog.ShowAsync();
                return;
            }

            HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
            Frame.Navigate(typeof(StatisticsWindow), filteredMatches);
        }

        void Instance_CollectionLoaded(object sender)
        {
            FilterMatches();
        }
        #endregion
    }
}
