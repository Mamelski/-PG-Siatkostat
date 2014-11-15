using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Siatkostat.Data.DataProviders;
using Siatkostat.Data.DataModels;

namespace Siatkostat
{
    public enum Grade
    {
        Perfect,
        Positive,
        Bad,
        Broken
    }

    public sealed partial class ActionStat : Page
    {
        private MatchProvider matchProvider = new MatchProvider();
        private SetProvider setProvider = new SetProvider();
        private TeamsProvider teamProvider = new TeamsProvider();
        private PlayersProvider playerProvider = new PlayersProvider();

        public ActionStat()
        {
            this.InitializeComponent();

            // Zagrywka
            PointServeButton.Click += HideStackPanelAndUnselectButtons;
            OdrzucajacaButton.Click += HideStackPanelAndUnselectButtons;
            ResztaServeButton.Click += HideStackPanelAndUnselectButtons;
            BrokenServeButton.Click += HideStackPanelAndUnselectButtons;

            // Przyjecie
            PerfectSaveButton.Click += HideStackPanelAndUnselectButtons;
            PositiveSaveButton.Click += HideStackPanelAndUnselectButtons;
            BadSaveButton.Click += HideStackPanelAndUnselectButtons;
            BrokenSaveButton.Click += HideStackPanelAndUnselectButtons;

            // Atak
            PointAttackButton.Click += HideStackPanelAndUnselectButtons;
            OtherAttackButton.Click += HideStackPanelAndUnselectButtons;
            BlockedAttackButton.Click += HideStackPanelAndUnselectButtons;
            BrokenAttackButton.Click += HideStackPanelAndUnselectButtons;

            // Blok
            PointBlockButton.Click += HideStackPanelAndUnselectButtons;
            NetFaultBlockButton.Click += HideStackPanelAndUnselectButtons;

            // Inny błąd
            SelfFaultButton.Click += HideStackPanelAndUnselectButtons;

            SetPlayersOnCourt();
        }

        private void SetPlayersOnCourt()
        {
            for (int i = 0; i < Court.Players.Count; i++)
            {
                Court.Players[i].player = playerProvider.PlayerMockCollection[i];
            }
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

        private void ServeButton_Click(object sender, RoutedEventArgs e)
        {
            UncheckAllActionTypeButtons();
            ServeButton.IsChecked = true;

            HideGradeStackPanels();
            ServeStackPanel.Visibility = Visibility.Visible;
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

        private void HideStackPanelAndUnselectButtons(object sender, RoutedEventArgs e)
        {
            if (!Court.Players.Any(p => p.Selected))
                new MessageDialog("Wybierz zawodnika!").ShowAsync();
            HideGradeStackPanels();
            UncheckAllActionTypeButtons();
            UncheckPlayers();
        }

        private void UncheckPlayers()
        {
            foreach (PlayerControl player in Court.Players)
                player.Unselect();
        }

        private void PrzyjecieButton_Click(object sender, RoutedEventArgs e)
        {
            UncheckAllActionTypeButtons();
            PrzyjecieButton.IsChecked = true;

            HideGradeStackPanels();
            PrzyjecieStackPanel.Visibility = Visibility.Visible;
        }

        private void AttackButton_Click(object sender, RoutedEventArgs e)
        {
            UncheckAllActionTypeButtons();
            AttackButton.IsChecked = true;

            HideGradeStackPanels();
            AttackStackPanel.Visibility = Visibility.Visible;
        }

        private void BlockButton_Click(object sender, RoutedEventArgs e)
        {
            UncheckAllActionTypeButtons();
            BlockButton.IsChecked = true;

            HideGradeStackPanels();
            BlockStackPanel.Visibility = Visibility.Visible;
        }

        private void AnotherFaultButton_Click(object sender, RoutedEventArgs e)
        {
            UncheckAllActionTypeButtons();
            AnotherFaultButton.IsChecked = true;

            HideGradeStackPanels();
            AnotherFaultStackPanel.Visibility = Visibility.Visible;
        }

        private void AnotherTeamPointButton_Click(object sender, RoutedEventArgs e)
        {
            Court.RotatePlayers();
        }
    }
}
