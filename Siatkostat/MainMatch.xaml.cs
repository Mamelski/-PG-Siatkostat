using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Siatkostat.Models;
using Siatkostat.ViewModels;

namespace Siatkostat
{
    public sealed partial class MainMatch : Page
    {
        private Match match;
        public MainMatch()
        {
            this.InitializeComponent();

            if (MatchViewModel.Instance.CurrentMatch.Finished())
            {
                Frame.Navigate(typeof (StatisticsWindow));
            }

            Court.SetPlayersOnCourt(PlayersViewModel.Instance.PlayersOnCourt);

            match = MatchViewModel.Instance.CurrentMatch;
           //match.OnSetFinish += UpdateSetsResult;

            FirstTeamNameTextBlock.Text = App.SelectedTeam != null ? App.SelectedTeam.TeamName : "Gość";
            SecondTeamNameTextBlock.Text = MatchViewModel.Instance.CurrentMatch.OponentName;

            SetLog();
        }

        private void SetLog()
        {
            LogListView.ItemsSource = Log.Instance.Messages;

            LogListView.SelectedIndex = LogListView.Items.Count - 1;
            LogListView.ScrollIntoView(LogListView.SelectedItem);
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            UpdateScoreResult();
            UpdateSetsResult();
        }

        private void UpdateSetsResult()
        {
            SetResulTextBlock.Text = String.Format("{0}:{1}", match.TeamSetScore, match.OpponentSetScore);
        }

        private void UpdateScoreResult()
        {
            PointsResultTextBlock.Text = String.Format("{0}:{1}", match.CurrentTeamScore(), match.CurrentOpponentScore());
        }

        private void ActionButton_Click(object sender, RoutedEventArgs e)
        {
      
           Frame.Navigate(typeof(ActionStat));
        }

        private void StatisticsButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(StatisticsWindow));
        }

        private void SubstitutionButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof (CourtPlayersSelect));
        }

    }
}
