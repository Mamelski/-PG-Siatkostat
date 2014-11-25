using System;
using System.Linq;
using Windows.Devices.Geolocation;
using Windows.Phone.UI.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Navigation;
using Siatkostat.Models;
using Siatkostat.ViewModels;

namespace Siatkostat
{
    public sealed partial class ActionStat
    {
        private Match match;
        private SetViewModel setViewModel = SetViewModel.Instance;

        public ActionStat()
        {
            this.InitializeComponent();

            match = MatchViewModel.Instance.CurrentMatch;

            SetPlayersOnCourt();
        }

        private Set GetPlayerSet()
        {
            return setViewModel.CurrentMatchSets.Find(
                s => s.PlayerId == Court.SelectedPlayer.player.Id && s.SetNumber == match.CurrentSet);
        }

        private void SetPlayersOnCourt()
        {
            Court.SetPlayersOnCourt(PlayersViewModel.Instance.PlayersOnCourt);
        }

        void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            e.Handled = true;
            HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
            Frame.Navigate(typeof(MainMatch));
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

        private void HideGradeStackPanels()
        {
            ServeStackPanel.Visibility = Visibility.Collapsed;
            PrzyjecieStackPanel.Visibility = Visibility.Collapsed;
            AttackStackPanel.Visibility = Visibility.Collapsed;
            BlockStackPanel.Visibility = Visibility.Collapsed;
            AnotherFaultStackPanel.Visibility = Visibility.Collapsed;
        }

        private void UncheckAllActionTypeButtons()
        {
            ServeButton.IsChecked = false;
            PrzyjecieButton.IsChecked = false;
            AttackButton.IsChecked = false;
            BlockButton.IsChecked = false;
            AnotherFaultButton.IsChecked = false;
        }

        private void HideStackPanelAndUnselectButtons()
        {
            HideGradeStackPanels();
            UncheckAllActionTypeButtons();
        }

        private void UncheckPlayers()
        {
            foreach (PlayerControl player in Court.Players)
                player.Unselect();
        }

        #region LeftPanelButtons
        private void ServeButton_Click(object sender, RoutedEventArgs e)
        {
            UncheckAllActionTypeButtons();
            ServeButton.IsChecked = true;

            HideGradeStackPanels();
            ServeStackPanel.Visibility = Visibility.Visible;

            Log.Instance.Messages.Add(new Random().NextDouble().ToString());
            Court.SelectPlayerOnPosition(CourtControl.Position.Serve);
        }

        private void PrzyjecieButton_Click(object sender, RoutedEventArgs e)
        {
            HideStackPanelAndUnselectButtons();
            UncheckAllActionTypeButtons();
            PrzyjecieButton.IsChecked = true;

            HideGradeStackPanels();
            PrzyjecieStackPanel.Visibility = Visibility.Visible;
        }

        private void AttackButton_Click(object sender, RoutedEventArgs e)
        {
            HideStackPanelAndUnselectButtons();
            UncheckAllActionTypeButtons();
            AttackButton.IsChecked = true;

            HideGradeStackPanels();
            AttackStackPanel.Visibility = Visibility.Visible;
        }

        private void BlockButton_Click(object sender, RoutedEventArgs e)
        {
            HideStackPanelAndUnselectButtons();
            UncheckAllActionTypeButtons();
            BlockButton.IsChecked = true;

            HideGradeStackPanels();
            BlockStackPanel.Visibility = Visibility.Visible;
        }

        private void AnotherFaultButton_Click(object sender, RoutedEventArgs e)
        {
            HideStackPanelAndUnselectButtons();
            UncheckAllActionTypeButtons();
            AnotherFaultButton.IsChecked = true;

            HideGradeStackPanels();
            AnotherFaultStackPanel.Visibility = Visibility.Visible;
        }
        #endregion

        #region RightPanelButtons
        #region ReturnButtons
        private void AnotherTeamPointButton_Click(object sender, RoutedEventArgs e)
        {
            match.AddTeamPoint();
            Court.PointFor(CourtControl.CurrentTeam.Team);
            ExpertSystem.NeedSubstitution(GetPlayerSet());
            Frame.Navigate(typeof (MainMatch));
        }

        private void AnotherOpponentPointButton_Click(object sender, RoutedEventArgs e)
        {
            match.AddOpponentPoint();
            Court.PointFor(CourtControl.CurrentTeam.Opponent);
            Frame.Navigate(typeof(MainMatch));
        }

        private async void PointAttackButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Court.Players.Any(p => p.Selected))
            {
                await new MessageDialog("Wybierz zawodnika!").ShowAsync();
                return;
            }
            GetPlayerSet().SpikeKill++;
            match.AddTeamPoint();
            Court.PointFor(CourtControl.CurrentTeam.Team);
            Frame.Navigate(typeof(MainMatch));
        }

        private async void PointBlockButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Court.Players.Any(p => p.Selected))
            {
                await new MessageDialog("Wybierz zawodnika!").ShowAsync();
                return;
            }
            GetPlayerSet().BlockKill++;
            match.AddTeamPoint();
            Court.PointFor(CourtControl.CurrentTeam.Team);
            Frame.Navigate(typeof(MainMatch));
        }

        private async void SelfFaultButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Court.Players.Any(p => p.Selected))
            {
                await new MessageDialog("Wybierz zawodnika!").ShowAsync();
                return;
            }
            GetPlayerSet().OwnFault++;
            match.AddOpponentPoint();
            Court.PointFor(CourtControl.CurrentTeam.Opponent);
            Frame.Navigate(typeof(MainMatch));
        }

        private async void PointServeButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Court.Players.Any(p => p.Selected))
            {
                await new MessageDialog("Wybierz zawodnika!").ShowAsync();
                return;
            }
            GetPlayerSet().ServeAce++;
            match.AddTeamPoint();
            Court.PointFor(CourtControl.CurrentTeam.Team);
            Frame.Navigate(typeof(MainMatch));
        }

        private async void BrokenServeButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Court.Players.Any(p => p.Selected))
            {
                await new MessageDialog("Wybierz zawodnika!").ShowAsync();
                return;
            }
            GetPlayerSet().ServeFault++;
            match.AddOpponentPoint();
            Court.PointFor(CourtControl.CurrentTeam.Opponent);
            Frame.Navigate(typeof(MainMatch));
        }

        private async void BrokenSaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Court.Players.Any(p => p.Selected))
            {
                await new MessageDialog("Wybierz zawodnika!").ShowAsync();
                return;
            }
            GetPlayerSet().ReceiveFault++;
            match.AddOpponentPoint();
            Court.PointFor(CourtControl.CurrentTeam.Opponent);
            Frame.Navigate(typeof(MainMatch));
        }

        private async void BlockedAttackButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Court.Players.Any(p => p.Selected))
            {
                await new MessageDialog("Wybierz zawodnika!").ShowAsync();
                return;
            }
            GetPlayerSet().SpikeBlocked++;
            match.AddOpponentPoint();
            Court.PointFor(CourtControl.CurrentTeam.Opponent);
            Frame.Navigate(typeof(MainMatch));
        }

        private async void NetFaultBlockButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Court.Players.Any(p => p.Selected))
            {
                await new MessageDialog("Wybierz zawodnika!").ShowAsync();
                return;
            }
            GetPlayerSet().BlockFault++;
            match.AddOpponentPoint();
            Court.PointFor(CourtControl.CurrentTeam.Opponent);
            Frame.Navigate(typeof(MainMatch));
        }

        private async void BrokenAttackButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Court.Players.Any(p => p.Selected))
            {
                await new MessageDialog("Wybierz zawodnika!").ShowAsync();
                return;
            }
            GetPlayerSet().SpikeFault++;
            match.AddOpponentPoint();
            Court.PointFor(CourtControl.CurrentTeam.Opponent);
            Frame.Navigate(typeof(MainMatch));
        }
        #endregion
        #region No return buttons
        private void OdrzucajacaButton_Click(object sender, RoutedEventArgs e)
        {
            GetPlayerSet().ServeHit++;
            UncheckPlayers();
        }

        private void ResztaServeButton_Click(object sender, RoutedEventArgs e)
        {
            GetPlayerSet().ServeOther++;
            UncheckPlayers();
        }

        private void PerfectSaveButton_Click(object sender, RoutedEventArgs e)
        {
            GetPlayerSet().ReceivePerfect++;
            UncheckPlayers();
        }

        private void BadSaveButton_Click(object sender, RoutedEventArgs e)
        {
            GetPlayerSet().ReceiveBad++;
            UncheckPlayers();
        }

        private void PositiveSaveButton_Click(object sender, RoutedEventArgs e)
        {
            GetPlayerSet().ReceiveGood++;
            UncheckPlayers();
        }

        private void OtherAttackButton_Click(object sender, RoutedEventArgs e)
        {
            GetPlayerSet().SpikeOther++;
            UncheckPlayers();
        }
        #endregion

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainMatch));
        }

        
        #endregion
    }
}
