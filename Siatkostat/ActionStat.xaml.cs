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

        public ActionStat()
        {
            this.InitializeComponent();

            match = MatchViewModel.Instance.CurrentMatch;

            SetPlayersOnCourt();
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

        private void ActionTypeButton_Click(object sender, RoutedEventArgs e)
        {
            ServeButton.IsChecked = false;
            PrzyjecieButton.IsChecked = false;
            AttackButton.IsChecked = false;
            BlockButton.IsChecked = false;
            AnotherFaultButton.IsChecked = false;

            ToggleButton activeButton = sender as ToggleButton;
            activeButton.IsChecked = true;
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
            match.AddOpponentPoint();
            Court.PointFor(CourtControl.CurrentTeam.Opponent);
            Frame.Navigate(typeof(MainMatch));
        }
        #endregion
        #region No return buttons
        private void OdrzucajacaButton_Click(object sender, RoutedEventArgs e)
        {
            UncheckPlayers();
        }

        private void ResztaServeButton_Click(object sender, RoutedEventArgs e)
        {
            UncheckPlayers();
        }

        private void PerfectSaveButton_Click(object sender, RoutedEventArgs e)
        {
            UncheckPlayers();
        }

        private void BadSaveButton_Click(object sender, RoutedEventArgs e)
        {
            UncheckPlayers();
        }

        private void PositiveSaveButton_Click(object sender, RoutedEventArgs e)
        {
            UncheckPlayers();
        }

        private void OtherAttackButton_Click(object sender, RoutedEventArgs e)
        {
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
