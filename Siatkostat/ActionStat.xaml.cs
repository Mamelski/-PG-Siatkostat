using System;
using System.Linq;
using Windows.Phone.UI.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using Siatkostat.Models;
using Siatkostat.ViewModels;

namespace Siatkostat
{
    public enum ActionType
    {
        Spike,
        Block,
        Rebound,
        Serve
    };
    
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
                s => (s.PlayerId == Court.SelectedPlayer.player.Id && s.SetNumber == match.CurrentSet));
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

        private void LogMessage()
        {
            int ile = 5;
            if (ExpertSystem.NeedSubstitution(GetPlayerSet()))
            {
                Player player = Court.SelectedPlayer.player;
                if (player == null) return;
                Log.Instance.Messages.Add(String.Format("Sugerowana zmiana zawodnika nr {0} - {1} {2}", player.Number, player.FirstName, player.LastName));
            }

            int blockRate = (int)(ExpertSystem.BlockRate(match.GetCurrentSet()) * 100);
            int spikeRate = (int)(ExpertSystem.SpikeRate(match.GetCurrentSet()) * 100);
            int receiveRate = (int)(ExpertSystem.ReceiveRate(match.GetCurrentSet()) * 100);
            int serveRate = (int)(ExpertSystem.ServeRate(match.GetCurrentSet()) * 100);

            if (match.GetCurrentSet().TotalBlocks() > ile && blockRate < match.BlockThreshold)
            {
                Log.Instance.Messages.Add(String.Format("Skuteczność bloku spadła poniżej {0}% - ({1}%)", match.BlockThreshold, blockRate));
            }
            if (match.GetCurrentSet().TotalSpikes() > ile && spikeRate < match.AttackThreshold)
            {
                Log.Instance.Messages.Add(String.Format("Skuteczność ataku spadła poniżej {0}% - ({1}%)", match.AttackThreshold, spikeRate));
            }
            if (match.GetCurrentSet().TotalReceives() > ile && receiveRate < match.ReboundThreshold)
            {
                Log.Instance.Messages.Add(String.Format("Skuteczność przyjęcia spadła poniżej {0}% - ({1}%)", match.ReboundThreshold, receiveRate));
            }
            if (match.GetCurrentSet().TotalServes() > ile && serveRate < match.ServeThreshold)
            {
                Log.Instance.Messages.Add(String.Format("Skuteczność zagrywki spadła poniżej {0}% - ({1}%)", match.ServeThreshold, serveRate));
            }
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
            UncheckPlayers();
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
            match.GetCurrentSet().SpikeKill++;
            match.AddTeamPoint();
            Court.PointFor(CourtControl.CurrentTeam.Team);
            LogMessage();
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
            match.GetCurrentSet().BlockKill++;
            match.AddTeamPoint();
            Court.PointFor(CourtControl.CurrentTeam.Team);
            LogMessage();
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
            match.GetCurrentSet().OwnFault++;
            match.AddOpponentPoint();
            Court.PointFor(CourtControl.CurrentTeam.Opponent);
            LogMessage();
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
            match.GetCurrentSet().ServeAce++;
            match.AddTeamPoint();
            Court.PointFor(CourtControl.CurrentTeam.Team);
            LogMessage();
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
            match.GetCurrentSet().ServeFault++;
            match.AddOpponentPoint();
            Court.PointFor(CourtControl.CurrentTeam.Opponent);
            LogMessage();
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
            match.GetCurrentSet().ReceiveFault++;
            match.AddOpponentPoint();
            Court.PointFor(CourtControl.CurrentTeam.Opponent);
            LogMessage();
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
            match.GetCurrentSet().SpikeBlocked++;
            match.AddOpponentPoint();
            Court.PointFor(CourtControl.CurrentTeam.Opponent);
            LogMessage();
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
            match.GetCurrentSet().BlockFault++;
            match.AddOpponentPoint();
            Court.PointFor(CourtControl.CurrentTeam.Opponent);
            LogMessage();
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
            match.GetCurrentSet().SpikeFault++;
            match.AddOpponentPoint();
            Court.PointFor(CourtControl.CurrentTeam.Opponent);
            LogMessage();
            Frame.Navigate(typeof(MainMatch));
        }
        #endregion
        #region No return buttons
        private async void OdrzucajacaButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Court.Players.Any(p => p.Selected))
            {
                await new MessageDialog("Wybierz zawodnika!").ShowAsync();
                return;
            }
            GetPlayerSet().ServeHit++;
            match.GetCurrentSet().ServeHit++;
            LogMessage();
            UncheckPlayers();
        }

        private async void ResztaServeButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Court.Players.Any(p => p.Selected))
            {
                await new MessageDialog("Wybierz zawodnika!").ShowAsync();
                return;
            }
            GetPlayerSet().ServeOther++;
            match.GetCurrentSet().ServeOther++;
            LogMessage();
            UncheckPlayers();
        }

        private async void PerfectSaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Court.Players.Any(p => p.Selected))
            {
                await new MessageDialog("Wybierz zawodnika!").ShowAsync();
                return;
            }
            GetPlayerSet().ReceivePerfect++;
            match.GetCurrentSet().ReceivePerfect++;
            LogMessage();
            UncheckPlayers();
        }

        private async void BadSaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Court.Players.Any(p => p.Selected))
            {
                await new MessageDialog("Wybierz zawodnika!").ShowAsync();
                return;
            }
            GetPlayerSet().ReceiveBad++;
            match.GetCurrentSet().ReceiveBad++;
            LogMessage();
            UncheckPlayers();
        }

        private async void PositiveSaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Court.Players.Any(p => p.Selected))
            {
                await new MessageDialog("Wybierz zawodnika!").ShowAsync();
                return;
            }
            GetPlayerSet().ReceiveGood++;
            match.GetCurrentSet().ReceiveGood++;
            LogMessage();
            UncheckPlayers();
        }

        private async void OtherAttackButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Court.Players.Any(p => p.Selected))
            {
                await new MessageDialog("Wybierz zawodnika!").ShowAsync();
                return;
            }
            GetPlayerSet().SpikeOther++;
            match.GetCurrentSet().SpikeOther++;
            LogMessage();
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
